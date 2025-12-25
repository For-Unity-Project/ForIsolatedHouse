using UnityEngine;
using Unity.Netcode;

public class LevelManager : NetworkBehaviour
{
    public static LevelManager Instance;

    public NetworkObject[] levelPrefabs;
    NetworkObject currentLevel;

    void Awake()
    {
        Instance = this;
    }

    [ServerRpc(RequireOwnership = false)]
    public void SpawnLevelServerRpc(int levelIndex, Vector3 pos, Quaternion rot)
    {
        if (levelIndex < 0 || levelIndex >= levelPrefabs.Length)
            return;

        if (currentLevel != null)
            currentLevel.Despawn();

        currentLevel = Instantiate(
            levelPrefabs[levelIndex],
            pos,
            rot
        );

        currentLevel.Spawn();
    }
}
