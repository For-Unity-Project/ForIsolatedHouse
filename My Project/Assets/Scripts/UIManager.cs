using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private GameObject textSpawn;

    private void Awake()
    {
        Instance = this;
        textSpawn.SetActive(false);
    }

    public void ShowSpawnText()
    {
        StopAllCoroutines();
        textSpawn.SetActive(true);
        StartCoroutine(HideAfterDelay(15f));
    }

    private IEnumerator HideAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        textSpawn.SetActive(false);
    }
}
