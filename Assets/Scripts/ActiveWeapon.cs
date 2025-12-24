using UnityEngine;
using Cinemachine; // Import Cinemachine for camera control
using UnityEngine.UI;
using TMPro; // Import TextMeshPro for UI text display
using StarterAssets; // Import Unity UI for Image component



public class ActiveWeapon : MonoBehaviour
{
    [SerializeField] WeaponSO startingWeapon;
    [SerializeField] WeaponSO weaponSO; // Reference to the WeaponSO scriptable object
    [SerializeField] GameObject zoomEffect; // GameObject to activate when zooming in
    [SerializeField] float zoomSens;
    [SerializeField] TMP_Text currentAmmoUI;
    [SerializeField] TMP_Text totalAmmoUI;
    [SerializeField] AudioSource gunAudioSource; // attach to Player (same as shooting sound if you like)
    [SerializeField] AudioClip switchSound;      // one common switch sound



    Weapon currentWeapon; // Reference to the currentWeapon component
    CinemachineVirtualCamera virtualCamera; // Reference to the Cinemachine virtual camera for zoom functionality
    FirstPersonController fpc; // Reference to the FirstPersonController for player movement control

    float initTime; // Variable to store the initial time when the weapon is activated
    float defaultFOV; // Default field of view for the camera
    float defaultZoomSens; // Default zoom sensitivity
    int currentAmmo; // Variable to store the current ammo count


    private void Start()
    {
        currentWeapon = FindFirstObjectByType<Weapon>(); // Find the first Weapon component in the scene
        SwitchWeapon(startingWeapon);  // Switch to the starting weapon defined in the inspector
        virtualCamera = FindFirstObjectByType<CinemachineVirtualCamera>(); // Find the first Cinemachine virtual camera in the scene
        fpc = FindFirstObjectByType<FirstPersonController>(); // Find the first FirstPersonController component in the scene 
        initTime = 10; // Initialize the timer to a value greater than the weapon's fire rate to allow immediate shooting
        defaultFOV = virtualCamera.m_Lens.FieldOfView; // Store the default field of view of the camera
        defaultZoomSens = 1; // Store the default zoom sensitivity
        

    }
    private void Update()
    {
        initTime += Time.deltaTime; // Increment the timer by the time since the last frame
        HabndleShoot(); // Call the method to handle shooting functionality
        HandleZoom(); // Call the method to handle zoom functionality
    }

    private void HabndleShoot()  // Method to handle shooting functionality also to play gun sound
    {
        if (Input.GetKey(KeyCode.Mouse0) && currentAmmo > 0) // Check if the left mouse button is pressed
        {
            if (initTime >= weaponSO.fireRate)
            {
                currentWeapon.Shoot(weaponSO); // Call the Shoot method on the Weapon component with the weaponSO data
                ChangeAmmo(-1); // Decrease the current ammo count by 1
                initTime = 0; // Reset the timer
            }
        }
    }

    private void HandleZoom()
    {
        if (!weaponSO.canZoom) return; // If the weapon cannot zoom, exit the method
        if (Input.GetKey(KeyCode.Mouse1))
        {
            virtualCamera.m_Lens.FieldOfView = weaponSO.zoomFOV; // Set the camera's field of view to the zoom level defined in the weaponSO
            zoomEffect.SetActive(true); // Activate the zoom effect GameObject
            fpc.ChangeSens(zoomSens); // Change the sensitivity of the FirstPersonController to the zoom sensitivity defined in the weaponSO
        }
        else
        {
            virtualCamera.m_Lens.FieldOfView = defaultFOV; // Reset the camera's field of view to the default value
            zoomEffect.SetActive(false); // Deactivate the zoom effect GameObject
            fpc.ChangeSens(defaultZoomSens); // Reset the sensitivity of the FirstPersonController to the default value
        }
    }

    public void SwitchWeapon(WeaponSO weaponSO)
    {
        Debug.Log("Weapon switched to: " + weaponSO.name);
        if(currentWeapon != null) // Check if there is an existing currentWeapon
        {
            Destroy(currentWeapon.gameObject); // Destroy the current weapon instance
        }
        totalAmmoUI.text = weaponSO.magazineSize.ToString("D2"); // Display the total ammo count in the UI in a two-digit format
        currentAmmo = weaponSO.magazineSize; // Display the current ammo count in the UI in a two-digit format
        currentAmmoUI.text = weaponSO.magazineSize.ToString("D2"); // Initialize the current ammo count to the magazine size defined in the weaponSO
        Weapon newWeapon = Instantiate(weaponSO.weaponPrefab, transform).GetComponent<Weapon>(); // Instantiate the new weapon prefab and get its Weapon component
        currentWeapon = newWeapon; // Update the weapon reference
        this.weaponSO = weaponSO; // Update the weaponSO reference to the new weapon's scriptable object
        //  Play common switch sound
        if (switchSound != null)
            gunAudioSource.PlayOneShot(switchSound);
    }   

    public void ChangeAmmo(int amount)
    {
        currentAmmo += amount; // Change the current ammo count by the specified amount
        currentAmmo = Mathf.Clamp(currentAmmo, 0, weaponSO.magazineSize); // Ensure the current ammo count does not exceed the magazine size or go below zero
        currentAmmoUI.text = currentAmmo.ToString("D2"); // Update the UI to display the current ammo count in a two-digit format

    }
}
