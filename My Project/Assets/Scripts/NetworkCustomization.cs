using UnityEngine;
using Unity.Netcode;

public class NetworkCustomization : NetworkBehaviour
{
    [Header("Body")]
    public Renderer bodyRenderer;
    public Material[] bodyMaterials;

    [Header("Hats (0 = None)")]
    public GameObject[] hats;

    public NetworkVariable<int> bodyMaterialIndex =
        new NetworkVariable<int>(
            0,
            NetworkVariableReadPermission.Everyone,
            NetworkVariableWritePermission.Owner
        );

    public NetworkVariable<int> hatIndex =
        new NetworkVariable<int>(
            0, // 0 = no hat
            NetworkVariableReadPermission.Everyone,
            NetworkVariableWritePermission.Owner
        );

    public override void OnNetworkSpawn()
    {
        ApplyBody(bodyMaterialIndex.Value);
        ApplyHat(hatIndex.Value);

        bodyMaterialIndex.OnValueChanged += (_, newVal) =>
            ApplyBody(newVal);

        hatIndex.OnValueChanged += (_, newVal) =>
            ApplyHat(newVal);
    }

    void ApplyBody(int index)
    {
        if (bodyRenderer == null) return;
        if (bodyMaterials.Length == 0) return;

        index = Mathf.Clamp(index, 0, bodyMaterials.Length - 1);
        bodyRenderer.material = bodyMaterials[index];
    }

    void ApplyHat(int index)
    {
        // Disable all hats first
        foreach (var hat in hats)
            hat.SetActive(false);

        // 0 = no hat
        if (index == 0)
            return;

        int hatArrayIndex = index - 1;

        if (hatArrayIndex < 0 || hatArrayIndex >= hats.Length)
            return;

        hats[hatArrayIndex].SetActive(true);
    }

    // ===== UI BUTTON METHODS =====

    public void NextBodyColor()
    {
        if (!IsOwner) return;

        bodyMaterialIndex.Value =
            (bodyMaterialIndex.Value + 1) % bodyMaterials.Length;
    }

    public void NextHat()
    {
        if (!IsOwner) return;

        // +1 because 0 = none
        hatIndex.Value =
            (hatIndex.Value + 1) % (hats.Length + 1);
    }
}
