using UnityEngine;
using Unity.Netcode;

public class NetworkPhysicsBlock : NetworkBehaviour
{
    [Header("Cart")]
    public Rigidbody rb;

    [Header("Force Machines")]
    public PhysicalLever leftLever;   // machine pulling LEFT
    public PhysicalLever rightLever;  // machine pushing RIGHT

    [Header("Force Strength")]
    public float leftForceMultiplier = 30f;
    public float rightForceMultiplier = 30f;

    private void Awake()
    {
        if (!rb)
            rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (!IsServer) return;

        Vector3 totalForce = Vector3.zero;

        // LEFT machine
        if (leftLever)
        {
            float leftValue = leftLever.GetNormalizedValue(); // -1 to 1
            totalForce += -Vector3.forward * Mathf.Max(0f, leftValue) * leftForceMultiplier;
        }

        // RIGHT machine
        if (rightLever)
        {
            float rightValue = rightLever.GetNormalizedValue(); // -1 to 1
            totalForce += Vector3.forward * Mathf.Max(0f, rightValue) * rightForceMultiplier;
        }

        // Safety: prevent NaN force
        if (!float.IsNaN(totalForce.x))
            rb.AddForce(totalForce, ForceMode.Force);
    }
}
