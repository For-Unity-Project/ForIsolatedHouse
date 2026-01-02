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

        StartIntroClientRpc();

        GetComponent<NetworkObject>().Despawn(true);
    }

    [ClientRpc]
    private void StartIntroClientRpc()
    {
        intro.StartSequence();
    }
}
