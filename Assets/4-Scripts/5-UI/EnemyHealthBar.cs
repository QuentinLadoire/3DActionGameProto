using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private Image gaugeImage = null;

    public void SetPercent(float percent)
	{
		gaugeImage.fillAmount = percent;
	}
}
