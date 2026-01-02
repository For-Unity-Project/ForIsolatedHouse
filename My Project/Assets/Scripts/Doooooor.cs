using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class KeyQuizDoor : NetworkBehaviour
{
    [Header("UI")]
    public GameObject quizUI;
    public GameObject correctUI;
    public GameObject wrongUI;

    [Header("Choices")]
    public Button correctButton;
    public List<Button> wrongButtons;

    [Header("World")]
    public GameObject door;

    private bool used;

    private void Awake()
    {
        quizUI?.SetActive(false);
        correctUI?.SetActive(true);
        wrongUI?.SetActive(true);
        StartCoroutine(HideAwake());
    }

    private void Start()
    {
        if (correctButton != null)
            correctButton.onClick.AddListener(() => OnChoicePressed(true));

        foreach (Button b in wrongButtons)
        {
            if (b != null)
                b.onClick.AddListener(() => OnChoicePressed(false));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (used) return;
        if (!other.transform.root.CompareTag("Player")) return;

        NetworkObject net = other.transform.root.GetComponent<NetworkObject>();
        if (net == null || !net.IsOwner) return;

        quizUI.SetActive(true);
    }

    // 🔘 CALLED BY ALL BUTTONS
    private void OnChoicePressed(bool isCorrect)
    {
        if (used) return;

        if (isCorrect)
        {
            used = true;
            StartCoroutine(CorrectRoutine());
        }
        else
        {
            wrongUI.SetActive(true);
        }
    }

    private IEnumerator CorrectRoutine()
    {
        quizUI.SetActive(false);
        wrongUI.SetActive(false);
        correctUI.SetActive(true);

        yield return new WaitForSeconds(2f);

        // ✅ SERVER resolves world (NO RPC)
        if (IsServer)
        {
            ResolveWorld();
        }
    }

    private IEnumerator HideAwake()
    {
        yield return new WaitForSeconds(1f);
        wrongUI.SetActive(false);
        correctUI.SetActive(false);
    }

    // ✅ SERVER-ONLY LOGIC
    private void ResolveWorld()
    {
        Destroy(gameObject); // key
        if (door) Destroy(door);
    }
}
