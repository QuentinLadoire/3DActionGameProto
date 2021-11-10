using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthPanel : MonoBehaviour
{
    [SerializeField] private Player player = null;
    [SerializeField] private Image gaugeImage = null;
    [SerializeField] private Text healthText = null;

	private void Start()
	{
		player.HealthComponent.takeHealCallback += TakeHealDamageCallback;
		player.HealthComponent.takeDamegeCallback += TakeHealDamageCallback;

		//call on start for init 
		TakeHealDamageCallback();
	}
	private void OnDestroy()
	{
		player.HealthComponent.takeHealCallback -= TakeHealDamageCallback;
		player.HealthComponent.takeDamegeCallback -= TakeHealDamageCallback;
	}

	private void TakeHealDamageCallback()
	{
		gaugeImage.fillAmount = player.HealthComponent.HealthInPercent;
		healthText.text = string.Format("{0}/{1}", player.HealthComponent.Health, player.HealthComponent.HealthMax);
	}
}
