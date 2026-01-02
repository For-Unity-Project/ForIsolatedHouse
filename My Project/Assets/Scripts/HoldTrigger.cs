using UnityEngine;
using Unity.Netcode;
using System.Collections;

public class ItemHoldTriggerKey : NetworkBehaviour
{
    [Header("References")]
    public GameObject key;            // key (NOT NetworkObject)
    public Transform keySpawnPoint;   // where the key appears

    [Header("Settings")]
    public float holdTime = 5f;

    private bool itemInside;
    private bool activated;
    private Coroutine holdRoutine;

    private void Start()
    {
        if (key)
            key.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!IsServer) return;
        if (activated) return;
        if (!other.CompareTag("Item")) return;

        itemInside = true;
        holdRoutine = StartCoroutine(HoldCountdown());
    }

    private void OnTriggerExit(Collider other)
    {
        if (!IsServer) return;
        if (!other.CompareTag("Item")) return;

        ResetHold();
    }

    private IEnumerator HoldCountdown()
    {
        float timer = holdTime;

        while (timer > 0f)
        {
            if (!itemInside)
            {
                ResetHold();
                yield break;
            }

            timer -= Time.deltaTime;
            yield return null;
        }

        activated = true;
        SpawnKeyClientRpc();
    }

    private void ResetHold()
    {
        itemInside = false;

        if (holdRoutine != null)
            StopCoroutine(holdRoutine);

        holdRoutine = null;
    }

    // 🔥 SHOW KEY FOR EVERYONE
    [ClientRpc]
    private void SpawnKeyClientRpc()
    {
        if (!key) return;

        if (keySpawnPoint)
        {
            key.transform.position = keySpawnPoint.position;
            key.transform.rotation = keySpawnPoint.rotation;
        }

        key.SetActive(true);
    }
}
