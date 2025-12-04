using UnityEngine;

public class LimbCollision : MonoBehaviour
{
    public PlayerController PlayerController;


    private void Start()
    {
        PlayerController = GameObject.FindFirstObjectByType<PlayerController>().GetComponent<PlayerController>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        PlayerController.isGrounded = true;
    }
}
