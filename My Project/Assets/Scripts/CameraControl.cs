using UnityEngine;
using Unity.Netcode;

public class CameraControl : NetworkBehaviour
{
    [Header("Settings")]
    public float rotationSpeed = 3f;
    public float stomachOffset;

    [Header("References")]
    public Transform root;
    public ConfigurableJoint hipJoint;
    public ConfigurableJoint stomachJoint;
    public Camera cam;
    public AudioListener audioListener;

    private float mouseX, mouseY;

    public override void OnNetworkSpawn()
    {
        if (!IsOwner)
        {
            // 🔴 Disable camera & audio for non-owner
            cam.enabled = false;
            audioListener.enabled = false;
            enabled = false;
            return;
        }
    }

    void Start()
    {
        if (!IsOwner) return;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void FixedUpdate()
    {
        if (!IsOwner) return;

        HandleCamera();
        HandleCursorToggle();
    }

    void HandleCursorToggle()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            bool locked = Cursor.lockState == CursorLockMode.Locked;
            Cursor.lockState = locked ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = locked;
        }
    }

    void HandleCamera()
    {
        mouseX += Input.GetAxis("Mouse X") * rotationSpeed;
        mouseY -= Input.GetAxis("Mouse Y") * rotationSpeed;
        mouseY = Mathf.Clamp(mouseY, -35f, 60f);

        root.rotation = Quaternion.Euler(-mouseY, mouseX, 0f);

        hipJoint.targetRotation = Quaternion.Euler(0f, -mouseX, 0f);
        stomachJoint.targetRotation = Quaternion.Euler(mouseY + stomachOffset, 0f, 0f);
    }
}
