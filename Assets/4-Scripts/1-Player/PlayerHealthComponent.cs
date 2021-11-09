using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthComponent : MonoBehaviour
{
    private int health = 0;

	private Player player = null;

	public bool IsDead => health == 0;
	public bool IsAlive => health != 0;

	private void Awake()
	{
		player = GetComponent<Player>();
	}

	public void TakeDamege(int damage)
	{
		health -= damage;
		if (health < 0)
			health = 0;
	}
	public void TakeHeal(int heal)
	{
		health += heal;
		if (health > player.Stats.HealthMax)
			health = player.Stats.HealthMax;
	}
}
