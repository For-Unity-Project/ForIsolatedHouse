using UnityEngine;

public class CopyMotion : MonoBehaviour
{
    public Transform targetLimb;
    public ConfigurableJoint joint;

    Quaternion initialRotation;

    void Start()
    {
        joint = GetComponent<ConfigurableJoint>();
        initialRotation = transform.localRotation;
    }

    void FixedUpdate()
    {
        joint.targetRotation = GetTargetRotation();
    }

    Quaternion GetTargetRotation()
    {
        // Convert target rotation into joint space
        return Quaternion.Inverse(targetLimb.localRotation) * initialRotation;
    }
}
