using UnityEngine;
using TMPro;
using System.Collections;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public string[] lines;
    public float textSpeed = 0.04f;

    private int index;
    private Coroutine typingRoutine;

    public System.Action OnDialogueFinished;

    private void Awake()
    {
        gameObject.SetActive(false); // IMPORTANT
    }

    public void StartDialogue()
    {
        index = 0;
        dialogueText.text = "";
        gameObject.SetActive(true);

        typingRoutine = StartCoroutine(TypeLine());
    }

    private void Update()
    {
        if (!gameObject.activeInHierarchy) return;

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
