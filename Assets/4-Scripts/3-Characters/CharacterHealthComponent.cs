using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterHealthComponent : MonoBehaviour
{
	private Character character = null;

	//Parameters
	private int healthMax = 0;

	//Processing Variables
    private int health = 0;

	//Accessors
	public int Health => health;
	public int HealthMax => healthMax;
	public float HealthInPercent => health / (float)healthMax;

	public bool IsDead => health == 0;
	public bool IsAlive => health != 0;

	//Callbacks
	public UnityAction takeDamageCallback = () => { /*Debug.Log("TakeDamageCallback");*/ };
	public UnityAction takeHealCallback = () => { /*Debug.Log("TakeHealCallback");*/ };

	private void Awake()
	{
		character = GetComponent<Character>();
	}

	public void Init(int healthMax)
	{
		this.healthMax = healthMax;
		health = healthMax;
	}
	public void TakeDamage(int damage)
	{
		if (character.State == CharacterState.Dodge) return;

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
