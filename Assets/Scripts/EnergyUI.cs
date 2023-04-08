using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyUI : MonoBehaviour
{
    [SerializeField] private Image energybar;
    [SerializeField] private Image mask;
    [SerializeField] private float transitDuraiton;

    public bool InTransition {get; private set; }
    public void UpdateEnergybar(float amount)
    {
        energybar.fillAmount = amount ;
    }
        
    public void UpdateTransiton()
    {
        StartCoroutine(TransitCoroutine());
    }

    IEnumerator TransitCoroutine()
    {
        InTransition = true;

        float time = 0;
        Color startValue = new Color(255, 255, 255, 1);
        Color endValue = new Color(255, 255, 255, 0);
        UpdateEnergybar(0);
 
        mask.gameObject.SetActive(true);

        while (time < transitDuraiton)
        {
            mask.color = Color.Lerp(startValue, endValue, time / transitDuraiton);
            time += Time.deltaTime;
            yield return null;
        }
        mask.gameObject.SetActive(false);
        InTransition = false;
    }
}