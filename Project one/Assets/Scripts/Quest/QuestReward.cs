using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestReward
{
    [SerializeField]
    private Weapon weapon;
    public string instructions;
    
    public Weapon GetWeapon()
    {
        return this.weapon;
    }
}
