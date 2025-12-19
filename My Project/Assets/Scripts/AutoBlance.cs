using UnityEngine;

public class FakeRagdollBalance : MonoBehaviour
{
    public Rigidbody pelvis;

    public float uprightTorque = 1200f;
    public float torqueDamping = 80f;

    public float groundSpring = 8000f;
    public float springDamping = 300f;
    public float targetHeight = 1.1f;

    void FixedUpdate()
    {
        UprightForce();
        GroundSpringForce();
    }

    void UprightForce()
    {
        Vector3 currentUp = pelvis.transform.up;
        Vector3 targetUp = Vector3.up;

        Vector3 torque = Vector3.Cross(currentUp, targetUp);
        pelvis.AddTorque(torque * uprightTorque);
        pelvis.AddTorque(-pelvis.angularVelocity * torqueDamping);
    }

    void GroundSpringForce()
    {
        if (Physics.Raycast(pelvis.position, Vector3.down, out RaycastHit hit, 2f))
        {
            float heightError = targetHeight - hit.distance;
            float velocity = Vector3.Dot(pelvis.linearVelocity, Vector3.up);

            float force = (heightError * groundSpring) - (velocity * springDamping);
            pelvis.AddForce(Vector3.up * force);
        }
    }
}
