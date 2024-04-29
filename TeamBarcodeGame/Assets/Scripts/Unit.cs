using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string unitName;
    public int magDamage;
    public int melDamage;
    public int maxHP;
    public int currentHP;
    public int curePotency;
    public int exp;


    public bool TakeDamage(int dmg)
    {
        currentHP -= dmg;

        if (currentHP <=0)
        {
            currentHP = 0;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Cure(int amt)
    {
        currentHP += amt;
        if(currentHP > GameManager.Instance.maxPlayerHealth)
            currentHP = GameManager.Instance.maxPlayerHealth;
    }

}
