using UnityEngine;
using TMPro;
using System.Collections;

public class DialoguNow2 : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public string[] lines;
    public float textSpeed = 0.04f;

    private int index;
    private Coroutine typingRoutine;

    public System.Action OnDialogueFinished;

    private void Awake()
    {
        // Start disabled, enabled by other scripts
        gameObject.SetActive(false);
    }

    // 🔥 AUTO START when enabled
    private void OnEnable()
    {
        StartDialogue();
    }

    private void StartDialogue()
    {
        index = 0;
        dialogueText.text = "";

        if (typingRoutine != null)
            StopCoroutine(typingRoutine);

        typingRoutine = StartCoroutine(TypeLine());
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (dialogueText.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopCoroutine(typingRoutine);
                dialogueText.text = lines[index];
            }
        }
    }

    private IEnumerator TypeLine()
    {
        dialogueText.text = "";

        foreach (char c in lines[index])
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    private void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            typingRoutine = StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
            OnDialogueFinished?.Invoke();
        }
    }
}
