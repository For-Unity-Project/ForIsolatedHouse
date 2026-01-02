using UnityEngine;
using Unity.Netcode;

public class LevelManager : NetworkBehaviour
{
    public static LevelManager Instance;

    public NetworkObject[] levelPrefabs;
    private NetworkObject currentLevel;

    private void Awake()
    {
        Instance = this;
    }

    public void SpawnLevel(int levelIndex, Vector3 pos, Quaternion rot)
    {
        if (IsServer)
            SpawnLevel_Internal(levelIndex, pos, rot);
        else
            SpawnLevelServerRpc(levelIndex, pos, rot);
    }

    [ServerRpc(RequireOwnership = false)]
    private void SpawnLevelServerRpc(int levelIndex, Vector3 pos, Quaternion rot)
    {
        SpawnLevel_Internal(levelIndex, pos, rot);
    }

    private void SpawnLevel_Internal(int levelIndex, Vector3 pos, Quaternion rot)
    {
        if (levelIndex < 0 || levelIndex >= levelPrefabs.Length)
            return;

        if (currentLevel != null)
            currentLevel.Despawn(true);

        currentLevel = Instantiate(levelPrefabs[levelIndex], pos, rot);
        currentLevel.Spawn();

        ShowSpawnTextClientRpc(); // ✅ tell clients to show text
    }

    [ClientRpc]
    private void ShowSpawnTextClientRpc()
    {
        UIManager.Instance.ShowSpawnText();
    }
}
