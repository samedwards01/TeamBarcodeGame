using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleHUD : MonoBehaviour
{

    public TextMeshProUGUI nameText;
    public Slider hpSlider;
    public TextMeshProUGUI hpText;

    public void SetHUD(Unit unit, int maxHP)
    {
        nameText.text = unit.unitName;
        hpSlider.maxValue = maxHP;
        hpSlider.value = unit.currentHP;
        UpdateHPText(unit.currentHP, maxHP);
    }

    public void SetHP(int hp)
    {
        hpSlider.value = hp;
        UpdateHPText(hp, (int)hpSlider.maxValue);
    }

    public void UpdateHPText(int currentHP, int maxHP)
    {
        hpText.text = "HP " + currentHP.ToString() + "/" + maxHP.ToString();
    }

}


