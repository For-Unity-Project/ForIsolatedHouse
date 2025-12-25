using Unity.Netcode;
using UnityEngine;

public class LevelRequestSender : NetworkBehaviour
{
    public void RequestLevel(int levelIndex, Vector3 pos, Quaternion rot)
    {
        if (!IsOwner) return;

        LevelManager.Instance.SpawnLevelServerRpc(levelIndex, pos, rot);
    }
}
