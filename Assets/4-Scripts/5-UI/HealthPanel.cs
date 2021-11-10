using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthPanel : MonoBehaviour
{
    [SerializeField] private Image gaugeImage = null;
    [SerializeField] private Text healthText = null;

	private PlayerController playerController = null;

	private void Start()
	{
		playerController = GameManager.PlayerController;

		playerController.HealthComponent.takeHealCallback += TakeHealDamageCallback;
		playerController.HealthComponent.takeDamageCallback += TakeHealDamageCallback;

		//call on start for init 
		TakeHealDamageCallback();
	}
	private void OnDestroy()
	{
		playerController.HealthComponent.takeHealCallback -= TakeHealDamageCallback;
		playerController.HealthComponent.takeDamageCallback -= TakeHealDamageCallback;
	}

	private void TakeHealDamageCallback()
	{
		gaugeImage.fillAmount = playerController.HealthComponent.HealthInPercent;
		healthText.text = string.Format("{0}/{1}", playerController.HealthComponent.Health, playerController.HealthComponent.HealthMax);
	}
}
