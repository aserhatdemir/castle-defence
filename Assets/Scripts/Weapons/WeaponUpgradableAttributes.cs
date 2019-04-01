using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

[System.Serializable]
public class WeaponUpgradableAttributes
{
    public String name;

    [System.Serializable]
    public class WeaponAttribute
    {
        public string name;
        public float baseValue;
        public float currentValue = 0f;
        public int level;


        public float multiplier = 1.05f;
        public int maxNumLevels = 5;

        public void LevelUp()
        {
            if (level >= maxNumLevels) return;
            level++;
            refresh();
        }

        public void refresh()
        {
            this.currentValue = baseValue * Mathf.Pow(multiplier, level);
        }
    }

    public WeaponAttribute speed;
    public WeaponAttribute health;
    public WeaponAttribute range;
    public WeaponAttribute attackSpeed;
    public WeaponAttribute aimingSpeed;
    public WeaponAttribute damage;

    public void refresh()
    {
        speed.refresh();
        health.refresh();
        range.refresh();
        attackSpeed.refresh();
        aimingSpeed.refresh();
        damage.refresh();
    }
}