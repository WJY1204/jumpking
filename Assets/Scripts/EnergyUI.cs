using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyUI : MonoBehaviour
{
    [SerializeField] private Image energybar;

    public void UpdateEnergybar(float amount)
    {
        energybar.fillAmount = amount ;
    }
        
}