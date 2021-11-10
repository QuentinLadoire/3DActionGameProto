using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthPanel : MonoBehaviour
{
    [SerializeField] private Character character = null;
    [SerializeField] private Image gaugeImage = null;
    [SerializeField] private Text healthText = null;

	private void Start()
	{
		character.HealthComponent.takeHealCallback += TakeHealDamageCallback;
		character.HealthComponent.takeDamegeCallback += TakeHealDamageCallback;

		//call on start for init 
		TakeHealDamageCallback();
	}
	private void OnDestroy()
	{
		character.HealthComponent.takeHealCallback -= TakeHealDamageCallback;
		character.HealthComponent.takeDamegeCallback -= TakeHealDamageCallback;
	}

	private void TakeHealDamageCallback()
	{
		gaugeImage.fillAmount = character.HealthComponent.HealthInPercent;
		healthText.text = string.Format("{0}/{1}", character.HealthComponent.Health, character.HealthComponent.HealthMax);
	}
}
