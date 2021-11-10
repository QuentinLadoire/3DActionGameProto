using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCanvas : MonoBehaviour
{
    [SerializeField] private EnemyHealthBar healthBar = null;
    [SerializeField] private CharacterHealthComponent healthComponent = null;

	private void Start()
	{
		healthComponent.takeHealCallback += TakeHealDamageCallback;
		healthComponent.takeDamageCallback += TakeHealDamageCallback;

		TakeHealDamageCallback(); //for init
	}
	private void Update()
	{
		transform.forward = transform.position - GameManager.TopdownCamera.transform.position;
	}
	private void OnDestroy()
	{
		healthComponent.takeHealCallback -= TakeHealDamageCallback;
		healthComponent.takeDamageCallback -= TakeHealDamageCallback;
	}

	private void TakeHealDamageCallback()
	{
		healthBar.SetPercent(healthComponent.HealthInPercent);
	}
}
