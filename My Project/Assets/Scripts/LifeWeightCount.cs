using UnityEngine;

public class WeightZone : MonoBehaviour
{
    public PulleyWeightSystem pulley;
    public bool isPlatformSide;

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.attachedRigidbody;
        if (!rb) return;

        if (isPlatformSide)
            pulley.AddMassToPlatform(rb.mass);
        else
            pulley.AddMassToCounter(rb.mass);
    }

    private void OnTriggerExit(Collider other)
    {
        Rigidbody rb = other.attachedRigidbody;
        if (!rb) return;

        Debug.Log("Mass added: " + rb.mass);

        if (isPlatformSide)
            pulley.RemoveMassFromPlatform(rb.mass);
        else
            pulley.RemoveMassFromCounter(rb.mass);
    }
}
