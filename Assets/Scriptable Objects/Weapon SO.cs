    using UnityEngine;

[CreateAssetMenu(fileName = "WeaponSO", menuName = "Scriptable Objects/WeaponSO")]
public class WeaponSO : ScriptableObject
{
    public GameObject weaponPrefab;
    public int damage; 
    public int magazineSize;
    public float fireRate;
    public bool canZoom;
    public float zoomFOV;

    [Header("Audio Settings")]
    public AudioClip gunSound; //Unique gun sound for this weapon
}
