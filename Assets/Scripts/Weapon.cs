using UnityEngine;

public class Weapon : MonoBehaviour
{
    RaycastHit hit;

    [SerializeField] GameObject hitFx;
    [SerializeField] ParticleSystem muzzleFx;
    [SerializeField] LayerMask layerMask;
    [SerializeField] AudioSource audioSource; //AudioSource to play sound

    public void Shoot(WeaponSO weaponSO) 
    {
          // Play muzzle flash + sound first
        //muzzleFx.Play();

        if (weaponSO.gunSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(weaponSO.gunSound); // Play the weapon’s sound
        }
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity, layerMask ,QueryTriggerInteraction.Ignore))
        {
            muzzleFx.Play(); // Play the muzzle flash effect
            Instantiate(hitFx, hit.point, Random.rotation);
            //Debug.Log(hit.transform.gameObject.name);
            EnemyHealth em = hit.transform.GetComponent<EnemyHealth>();
      
            em?.TakeDamage(weaponSO.damage); // Use null-conditional operator to avoid null reference exception

        }
    }
}
