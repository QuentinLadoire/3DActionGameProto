using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterHealthComponent : MonoBehaviour
{
	//Parameters
	private int healthMax = 0;

	//Processing Variables
    private int health = 0;
	private bool invulnerable = false;

	//Accessors
	public int Health => health;
	public int HealthMax => healthMax;
	public float HealthInPercent => health / (float)healthMax;

	public bool IsDead => health == 0;
	public bool IsAlive => health != 0;

	public bool Invulnerable { get => invulnerable; set => invulnerable = value; }

	//Callbacks
	public UnityAction takeDamageCallback = () => { /*Debug.Log("TakeDamageCallback");*/ };
	public UnityAction takeHealCallback = () => { /*Debug.Log("TakeHealCallback");*/ };

	public void Init(int healthMax)
	{
		this.healthMax = healthMax;
		health = healthMax;
	}
	public void TakeDamage(int damage)
	{
		if (invulnerable) return;

		health -= damage;
		if (health < 0)
			health = 0;

		takeDamageCallback.Invoke();
	}
	public void TakeHeal(int heal)
	{
		health += heal;
		if (health > healthMax)
			health = healthMax;

		takeHealCallback.Invoke();
	}
}
