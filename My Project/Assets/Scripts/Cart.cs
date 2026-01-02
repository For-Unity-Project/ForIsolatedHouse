using UnityEngine;
using Unity.Netcode;

[RequireComponent(typeof(Rigidbody))]
public class NetworkPhysicsCart : NetworkBehaviour
{
    [Header("References")]
    public NetworkPhysicalLever lever;

    [Header("Forces")]
    public float constantLeftPull = 6f;     // unbalanced force
    public float maxPlayerForce = 12f;       // lever force

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        // 🔥 SERVER AUTHORITY ONLY
        if (!IsServer) return;

        ApplyForces();
    }

    private void ApplyForces()
    {
        if (rb == null || lever == null) return;

        float leverInput = lever.GetLeverNormalized();

        if (float.IsNaN(leverInput) || float.IsInfinity(leverInput))
            return;

        // Base unbalanced force (left)
        float baseForce = -constantLeftPull;

        // Player-added force (right)
        float playerForce = leverInput * maxPlayerForce;

        float finalForce = baseForce + playerForce;

        Vector3 forceVector = Vector3.right * finalForce;

        // 🔥 FINAL SAFETY CHECK
        if (float.IsNaN(forceVector.x)) return;

        rb.AddForce(forceVector, ForceMode.Force);
    }
}
