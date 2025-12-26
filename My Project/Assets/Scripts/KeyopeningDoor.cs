using UnityEngine;

public class DoorDisappearByTag : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered by: " + other.gameObject.name);

        if (other.gameObject.CompareTag("Key"))
        {
            Debug.Log("found: " + other.gameObject.name + "is a key");

            Destroy(other.gameObject); // destroy key
            Destroy(gameObject);                      // destroy door
        }
    }
}
