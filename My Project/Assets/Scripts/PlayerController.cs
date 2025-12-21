using UnityEngine;
using Unity.Netcode;

public class PlayerController : NetworkBehaviour
{
    [Header("References")]
    public Animator animator;
    public Rigidbody hips;

    [Header("Movement")]
    public float speed = 30f;
    public float strafeSpeed = 25f;
    public float jumpForce = 8f;

    [Header("Ground")]
    public bool isGrounded;

    public override void OnNetworkSpawn()
    {
        if (!IsOwner)
        {
            enabled = false;
            return;
        }
    }

    void Awake()
    {

        if (hips == null)
            hips = GetComponent<Rigidbody>();

        if (animator == null)
            animator = GetComponentInParent<Animator>();
    }

    void FixedUpdate()
    {
        // FORWARD
        if (Input.GetKey(KeyCode.W))
        {
            bool running = Input.GetKey(KeyCode.LeftShift);

            animator.SetBool("IsWalk", true);
            animator.SetBool("IsRun", running);
            animator.SetBool("IsSidewayL", false);
            animator.SetBool("IsSidewayR", false);

            hips.AddForce(
                -hips.transform.forward * speed * (running ? 1.5f : 1f),
                ForceMode.Acceleration
            );
        }
        else
        {
            animator.SetBool("IsWalk", false);
            animator.SetBool("IsRun", false);
            animator.SetBool("IsSidewayL", false);
            animator.SetBool("IsSidewayR", false);
        }

        // LEFT
        if (Input.GetKey(KeyCode.A))
        {
            animator.SetBool("IsSidewayL", true);
            animator.SetBool("IsSidewayR", false);
            hips.AddForce(hips.transform.right * strafeSpeed, ForceMode.Acceleration);
        }

        // RIGHT
        if (Input.GetKey(KeyCode.D))
        {
            animator.SetBool("IsSidewayL", false);
            animator.SetBool("IsSidewayR", true);
            hips.AddForce(-hips.transform.right * strafeSpeed, ForceMode.Acceleration);
        }

        // BACK
        if (Input.GetKey(KeyCode.S))
        {
            animator.SetBool("IsWalk", true);
            hips.AddForce(hips.transform.forward * speed, ForceMode.Acceleration);
        }

        // JUMP
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            hips.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }
}
