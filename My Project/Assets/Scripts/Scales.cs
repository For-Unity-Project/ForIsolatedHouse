using UnityEngine;
using Unity.Netcode;
using TMPro;
using System.Collections;

public class DualScaleTeleportKey : NetworkBehaviour
{
    [Header("References")]
    public GameObject scale1;
    public GameObject scale2;
    public GameObject key;

    [Header("World Text (3D)")]
    public TextMeshPro statusText;

    [Header("Settings")]
    public float countdownTime = 5f;

    private bool scale1Inside;
    private bool scale2Inside;
    private bool countdownRunning;
    private bool keyActivated;

    private Coroutine countdownRoutine;

    private void Awake()
    {
        if (!statusText)
            statusText = GetComponentInChildren<TextMeshPro>();
    }

    private void Start()
    {
        if (key)
            key.SetActive(false);

        SetText("Not Balanced");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!IsServer) return;

        if (other.gameObject == scale1)
            scale1Inside = true;

        if (other.gameObject == scale2)
            scale2Inside = true;

        TryStartCountdown();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!IsServer) return;

        if (other.gameObject == scale1)
            scale1Inside = false;

        if (other.gameObject == scale2)
            scale2Inside = false;

        ResetCountdown();
    }

    private void TryStartCountdown()
    {
        if (keyActivated || countdownRunning) return;

        if (scale1Inside && scale2Inside)
            countdownRoutine = StartCoroutine(Countdown());
    }

    private void ResetCountdown()
    {
        if (countdownRoutine != null)
            StopCoroutine(countdownRoutine);

        countdownRoutine = null;
        countdownRunning = false;

        SetText("Not Balanced");
    }

    private IEnumerator Countdown()
    {
        countdownRunning = true;
        float t = countdownTime;

        while (t > 0f)
        {
            if (!scale1Inside || !scale2Inside)
            {
                ResetCountdown();
                yield break;
            }

            SetText(Mathf.CeilToInt(t).ToString());
            t -= Time.deltaTime;
            yield return null;
        }

        countdownRunning = false;
        keyActivated = true;

        ActivateKey();
    }

    // 🔥 LOCAL ONLY
    private void SetText(string text)
    {
        if (statusText)
            statusText.text = text;
    }

    private void ActivateKey()
    {
        if (!key) return;

        key.transform.position = transform.position;
        key.SetActive(true);

        SetText("Balanced!");
    }
}
