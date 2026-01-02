using UnityEngine;

public class PhysicalLever : MonoBehaviour
{
    public float minAngle = -30f;
    public float maxAngle = 30f;

    private HingeJoint hinge;

    private void Awake()
    {
        hinge = GetComponent<HingeJoint>();
    }

    // 0 → 1 force output
    public float GetNormalizedValue()
    {
        float angle = hinge.angle;
        return Mathf.InverseLerp(minAngle, maxAngle, angle);
    }
}
