using UnityEngine;
using System.Collections;

public class LevelSpawning : MonoBehaviour
{
    public int levelIndex;
    public Transform spawnPoint;

    private GameObject Ui;
    private GameObject TextSpawn;

    private void Awake()
    {
        GameObject customUi = GameObject.Find("CustomUi");
        TextSpawn = GameObject.Find("Promt1"); // ✅ fixed semicolon

        if (customUi != null)
        {
            Transform subjects = customUi.transform.Find("Subjects");
            if (subjects != null)
            {
                Ui = subjects.gameObject;
                Ui.SetActive(false); // hide at start
            }
            else
            {
                Debug.LogError("Subjects not found under CustomUi");
            }
        }
        else
        {
            Debug.LogError("CustomUi not found");
        }
    }

    public void SpawnLevelButton()
    {
        if (LevelManager.Instance == null)
        {
            Debug.LogError("LevelManager not found");
            return;
        }

        if (Ui != null)
            Ui.SetActive(false);

        if (TextSpawn != null)
        {
            TextSpawn.SetActive(true);
            StartCoroutine(HideTextAfterDelay(5f)); // ✅ disable after 5 sec
        }

        LevelManager.Instance.SpawnLevel(
            levelIndex,
            spawnPoint.position,
            spawnPoint.rotation
        );
    }

    private IEnumerator HideTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (TextSpawn != null)
            TextSpawn.SetActive(false);
    }
}
