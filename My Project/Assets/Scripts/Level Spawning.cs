using UnityEngine;

public class LevelButton : MonoBehaviour
{
    public int levelIndex;
    public Transform levelSpawnPoint;

    bool used;

    void OnTriggerEnter(Collider other)
    {
        if (used) return;

        LevelRequestSender sender =
            other.GetComponentInParent<LevelRequestSender>();

        if (sender != null)
        {
            used = true;
            sender.RequestLevel(levelIndex, levelSpawnPoint.position, levelSpawnPoint.rotation);
        }
    }
}
