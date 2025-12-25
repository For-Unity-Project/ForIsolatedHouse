using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;

public class CustomizationUI : NetworkBehaviour
{
    GameObject customizationPanel;
    bool isOpen;

    Button colorButton;
    Button hatButton;

    NetworkCustomization customization;

    public override void OnNetworkSpawn()
    {
        if (!IsOwner)
        {
            enabled = false;
            return;
        }

        customization = GetComponent<NetworkCustomization>();

        Canvas canvas = FindObjectOfType<Canvas>(true);
        customizationPanel =
            canvas.transform.Find("CustomizationPanel").gameObject;

        colorButton =
            customizationPanel.transform.Find("Colour").GetComponent<Button>();
        hatButton =
            customizationPanel.transform.Find("Hat").GetComponent<Button>();

        colorButton.onClick.AddListener(customization.NextBodyColor);
        hatButton.onClick.AddListener(customization.NextHat);

        customizationPanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
            ToggleUI();
    }

    public void ToggleUI()
    {
        isOpen = !isOpen;
        customizationPanel.SetActive(isOpen);

        Cursor.lockState =
            isOpen ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isOpen;
    }
}
