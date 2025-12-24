using UnityEngine;

public class AmmoPickup : Pickup
{
    [SerializeField] int ammo = 10; // Amount of ammo to be added when picked up
    protected override void OnPickup(ActiveWeapon activeWeapon)
    {
        activeWeapon.ChangeAmmo(ammo);
    }
}
