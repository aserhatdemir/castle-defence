public class Tank2 : Weapon
{
    public override WeaponUpgradableAttributes GetUpgradableAttributes()
    {
        return GameManager.instance.weaponManagerScript.tank2UpgradableAttributes;
    }

}