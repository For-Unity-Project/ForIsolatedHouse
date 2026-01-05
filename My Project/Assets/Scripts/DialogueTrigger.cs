using UnityEngine;
using Unity.Netcode;

public class DialogueTriggerNetworked : NetworkBehaviour
{
    private LevelIntroSequence intro;
    private bool used;

    private void Awake()
    {
        intro = GetComponentInParent<LevelIntroSequence>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (used) return;
        if (!IsServer) return;

        if (!other.transform.root.CompareTag("Player"))
            return;

        used = true;

        // 🔥 Start dialogue on everyone
        StartIntroClientRpc();

        // 🔥 Remove trigger for everyone (NOT despawn)
        DestroyTriggerClientRpc();
    }

    [ClientRpc]
    private void StartIntroClientRpc()
    {
        intro.StartSequence();
    }

    [ClientRpc]
    private void DestroyTriggerClientRpc()
    {
        Destroy(gameObject);
    }
}
