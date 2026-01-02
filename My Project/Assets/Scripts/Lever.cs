using UnityEngine;
using Unity.Netcode;

[RequireComponent(typeof(HingeJoint))]
public class NetworkPhysicalLever : NetworkBehaviour
{
    private HingeJoint hinge;

    [Header("Lever Settings")]
    public float maxAngle = 35f; // degrees

    private void Awake()
    {
        hinge = GetComponent<HingeJoint>();
    }

    // 🔥 SAFE angle getter (NO NaN)
    public float GetLeverNormalized()
    {
        if (hinge == null || maxAngle <= 0f)
            return 0f;

        float angle = hinge.angle;

        if (float.IsNaN(angle) || float.IsInfinity(angle))
            return 0f;

        return Mathf.Clamp(angle / maxAngle, -1f, 1f);
    }
}
