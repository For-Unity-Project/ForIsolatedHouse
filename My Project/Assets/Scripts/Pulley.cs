using UnityEngine;
using Unity.Netcode;

public class PulleyWeightSystem : NetworkBehaviour
{
    [Header("Pulley Bodies")]
    public Rigidbody platform;
    public Rigidbody counterWeight;

    [Header("Movement Settings")]
    public float speed = 3f;

    [Header("Height Limits")]
    public float minHeight = 0f;
    public float maxHeight = 10f;

    private float platformExtraMass;
    private float counterExtraMass;

    void FixedUpdate()
    {
        // Server-authoritative
        if (NetworkManager.Singleton && !NetworkManager.Singleton.IsServer)
            return;

        float platformMass = platform.mass + platformExtraMass;
        float counterMass = counterWeight.mass + counterExtraMass;

        // Balanced → stop
        if (Mathf.Abs(platformMass - counterMass) < 0.1f)
        {
            Stop(platform);
            Stop(counterWeight);
            return;
        }

        // Decide roles explicitly
        if (platformMass > counterMass)
        {
            MoveDown(platform);
            MoveUp(counterWeight);
        }
        else
        {
            MoveUp(platform);
            MoveDown(counterWeight);
        }
    }

    void MoveUp(Rigidbody rb)
    {
        if (rb.position.y >= maxHeight)
        {
            Stop(rb);
            return;
        }

        rb.linearVelocity = Vector3.up * speed;
    }

    void MoveDown(Rigidbody rb)
    {
        if (rb.position.y <= minHeight)
        {
            Stop(rb);
            return;
        }

        rb.linearVelocity = Vector3.down * speed;
    }

    void Stop(Rigidbody rb)
    {
        rb.linearVelocity = Vector3.zero;
    }

    // Called by trigger zones
    public void AddMassToPlatform(float mass)
    {
        platformExtraMass += mass;
    }

    public void RemoveMassFromPlatform(float mass)
    {
        platformExtraMass -= mass;
    }

    public void AddMassToCounter(float mass)
    {
        counterExtraMass += mass;
    }

    public void RemoveMassFromCounter(float mass)
    {
        counterExtraMass -= mass;
    }
}
