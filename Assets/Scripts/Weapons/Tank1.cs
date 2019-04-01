public class Tank1 : Weapon
{
    public override WeaponUpgradableAttributes GetUpgradableAttributes()
    {
        return GameManager.instance.weaponManagerScript.tank1UpgradableAttributes;
    }
    

}
