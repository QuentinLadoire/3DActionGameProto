using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillIcon : MonoBehaviour
{
    [SerializeField] private Text cooldownText = null;
    [SerializeField] private Image cooldownImage = null;

	private void Start()
	{
		cooldownText.gameObject.SetActive(false);
	}
	public void SetCooldown(float time, float percent)
	{
		if (time <= 0.0f)
		{
			cooldownText.gameObject.SetActive(false);
		}
		else
		{
			cooldownText.gameObject.SetActive(true);

			if (time < 1.0f)
				cooldownText.text = time.ToString();
			else
				cooldownText.text = ((int)time).ToString();
		}

		cooldownImage.fillAmount = 1 - percent;
	}
}
