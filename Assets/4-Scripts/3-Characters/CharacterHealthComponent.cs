using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterHealthComponent : MonoBehaviour
{
    private int health = 0;

	private Character character = null;

	public int Health => health;
	public int HealthMax => character.Stats.HealthMax;
	public float HealthInPercent => health / (float)character.Stats.HealthMax;

	public bool IsDead => health == 0;
	public bool IsAlive => health != 0;

	public UnityAction takeDamegeCallback = () => { /*Debug.Log("TakeDamageCallback");*/ };
	public UnityAction takeHealCallback = () => { /*Debug.Log("TakeHealCallback");*/ };

	private void Awake()
	{
		character = GetComponent<Character>();

		health = character.Stats.HealthMax;
	}

	public void TakeDamege(int damage)
	{
		health -= damage;
		if (health < 0)
			health = 0;

		takeDamegeCallback.Invoke();
	}
	public void TakeHeal(int heal)
	{
		health += heal;
		if (health > character.Stats.HealthMax)
			health = character.Stats.HealthMax;

		takeHealCallback.Invoke();
	}
}
