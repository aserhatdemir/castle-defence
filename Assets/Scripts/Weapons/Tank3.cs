public class Tank3 : Weapon
{
    public override WeaponUpgradableAttributes GetUpgradableAttributes()
    {
        return GameManager.instance.weaponManagerScript.tank3UpgradableAttributes;
    }

}