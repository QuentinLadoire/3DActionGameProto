using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthPanel : MonoBehaviour
{
    [SerializeField] private PlayerController playerController = null;
    [SerializeField] private Image gaugeImage = null;
    [SerializeField] private Text healthText = null;

	private void Start()
	{
		playerController.HealthComponent.takeHealCallback += TakeHealDamageCallback;
		playerController.HealthComponent.takeDamegeCallback += TakeHealDamageCallback;

		//call on start for init 
		TakeHealDamageCallback();
	}
	private void OnDestroy()
	{
		playerController.HealthComponent.takeHealCallback -= TakeHealDamageCallback;
		playerController.HealthComponent.takeDamegeCallback -= TakeHealDamageCallback;
	}

	private void TakeHealDamageCallback()
	{
		gaugeImage.fillAmount = playerController.HealthComponent.HealthInPercent;
		healthText.text = string.Format("{0}/{1}", playerController.HealthComponent.Health, playerController.HealthComponent.HealthMax);
	}
}
