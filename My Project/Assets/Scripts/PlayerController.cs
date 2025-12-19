using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    
    public float speed;
    public float strafeSpeed;
    public float jumpForce;

    public Rigidbody hips;
    public bool isGrounded;

    void Start()
    {
        hips = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                animator.SetBool("IsWalk", true);
                animator.SetBool("IsRun", true);
                animator.SetBool("IsSidewayL", false);
                animator.SetBool("IsSidewayR", false);
                hips.AddForce(-hips.transform.forward * speed * 1.5f);
            }
            else
            {
                animator.SetBool("IsWalk", true);
                animator.SetBool("IsRun", false);
                animator.SetBool("IsSidewayL", false);
                animator.SetBool("IsSidewayR", false);
                hips.AddForce(-hips.transform.forward * speed);
            }
        }
        else
        {
            animator.SetBool("IsWalk", false);
            animator.SetBool("IsRun", false);
            animator.SetBool("IsSidewayL", false);
            animator.SetBool("IsSidewayR", false);
        }

        if (Input.GetKey(KeyCode.A))
        {
            animator.SetBool("IsWalk", false);
            animator.SetBool("IsRun", false);
            animator.SetBool("IsSidewayL", true);
            animator.SetBool("IsSidewayR", false);
            hips.AddForce(hips.transform.right * strafeSpeed);
        }

        if (Input.GetKey(KeyCode.S))
        {
            animator.SetBool("IsWalk", true);
            animator.SetBool("IsRun", false);
            animator.SetBool("IsSidewayL", false);
            animator.SetBool("IsSidewayR", false);
            hips.AddForce(hips.transform.forward * speed);
        }

        if (Input.GetKey(KeyCode.D))
        {
            animator.SetBool("IsWalk", false);
            animator.SetBool("IsRun", false);
            animator.SetBool("IsSidewayL", false);
            animator.SetBool("IsSidewayR", true);
            hips.AddForce(-hips.transform.right * strafeSpeed);
        }

        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            hips.AddForce(Vector3.up * jumpForce);
            isGrounded = false;
        }

    }
}
