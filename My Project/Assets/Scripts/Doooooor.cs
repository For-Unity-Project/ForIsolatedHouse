using UnityEngine;

public class DoorDisappearByTag : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered by: " + other.gameObject.name);

        if (other.transform.CompareTag("Key"))
        {
            Destroy(other.transform.gameObject); // destroy key
            Destroy(gameObject);                      // destroy door
        }
    }
}
