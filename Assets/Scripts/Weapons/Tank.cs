public class Tank : Weapon
{
    // Start is called before the first frame update
    public override WeaponUpgradableAttributes GetUpgradableAttributes()
    {
        return GameManager.instance.weaponManagerScript.tankUpgradableAttributes;
    }
}