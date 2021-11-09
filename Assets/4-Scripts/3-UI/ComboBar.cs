using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboBar : MonoBehaviour
{
    [SerializeField] private Image gaugeImage = null;

	public void Start()
	{
		gameObject.SetActive(false);
	}
	public void SetGauge(float percent)
	{
		if (percent == 0.0f || percent == 1.0f)
			gameObject.SetActive(false);
		else
		{
			gameObject.SetActive(true);
			gaugeImage.fillAmount = percent;
		}
	}
}
