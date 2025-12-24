using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 100f; // Speed of rotation for the pickup item
    const string PLAYER_TAG = "Player";


    private void Update()
    {
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f); // Rotate the pickup item around the Y-axis
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(PLAYER_TAG))
        {
            ActiveWeapon activeWeapon = other.GetComponentInChildren<ActiveWeapon>(); // Get the ActiveWeapon component from the player GameObject
            OnPickup(activeWeapon); // Call the abstract method to handle the pickup logic specific to the derived class
            Destroy(gameObject);
        }
    }

    protected abstract void OnPickup(ActiveWeapon activeWeapon); // Abstract method to be implemented by derived classes for specific pickup behavior
}
