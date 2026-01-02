using UnityEngine;
using System.Collections;

public class LevelIntroSequence : MonoBehaviour
{
    public Canvas canvas;

    public CanvasGroup levelText;
    public CanvasGroup levelName;

    public Dialogue dialogue;

    private void Awake()
    {
        canvas.gameObject.SetActive(false);

        levelText.gameObject.SetActive(false);
        levelName.gameObject.SetActive(false);
    }

    public void StartSequence()
    {
        canvas.gameObject.SetActive(true);
        StartCoroutine(Sequence());
    }

    private IEnumerator Sequence()
    {
        yield return FadeIn(levelText);
        yield return FadeIn(levelName);

        dialogue.OnDialogueFinished = OnDialogueFinished;
        dialogue.StartDialogue();
    }

    private IEnumerator FadeIn(CanvasGroup cg)
    {
        cg.gameObject.SetActive(true);
        cg.alpha = 0f;

        while (cg.alpha < 1f)
        {
            cg.alpha += Time.deltaTime;
            yield return null;
        }

        cg.alpha = 1f;
    }

    private void OnDialogueFinished()
    {
        levelText.gameObject.SetActive(false);
        levelName.gameObject.SetActive(false);
    }
}
