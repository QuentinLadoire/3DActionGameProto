using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealthComponent : MonoBehaviour
{
    private int health = 0;

	private Player player = null;

	public int Health => health;
	public int HealthMax => player.Stats.HealthMax;
	public int HealthInPercent => health / player.Stats.HealthMax;

	public bool IsDead => health == 0;
	public bool IsAlive => health != 0;

	public UnityAction takeDamegeCallback = () => { /*Debug.Log("TakeDamageCallback");*/ };
	public UnityAction takeHealCallback = () => { /*Debug.Log("TakeHealCallback");*/ };

	private void Awake()
	{
		player = GetComponent<Player>();

		health = player.Stats.HealthMax;
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
		if (health > player.Stats.HealthMax)
			health = player.Stats.HealthMax;

		takeHealCallback.Invoke();
	}
}
