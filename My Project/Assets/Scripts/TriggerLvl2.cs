using UnityEngine;
using Unity.Netcode;

public class ItemSpawnKeyRPC : NetworkBehaviour
{
    [Header("References")]
    public GameObject key;          // child object inside prefab
    public Transform spawnPoint;    // where the key should appear

    private void OnTriggerEnter(Collider other)
    {
        if (!IsServer) return;              // 🔥 server authority
        if (!other.CompareTag("Item")) return;

        if (key == null || spawnPoint == null)
            return;

        key.transform.position = spawnPoint.position;
        key.transform.rotation = spawnPoint.rotation;
        key.SetActive(true);
    }
}
