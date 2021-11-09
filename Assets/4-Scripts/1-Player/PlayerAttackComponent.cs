using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackComponent : MonoBehaviour
{
	private float cooldown = 0.0f;
	private float cooldownMax = 0.0f;

	private int combo = 0;
	private int comboMax = 0;
	private float comboDelay = 0.0f;
	private float comboDelayMax = 0.0f;

	private bool inCooldown = false;
	private bool inCombo = false;

	public bool HasAttack => inCooldown;
	public bool CanAttack => !inCooldown;

	public bool InCombo => inCombo;
	public bool InCooldown => inCooldown;

	public float Cooldown => cooldown;
	public float CooldownInPercent => cooldown / cooldownMax;

	public int Combo => combo;
	public float ComboDelay => comboDelay;
	public float ComboDelayInPercent => comboDelay / comboDelayMax;

	private void Update()
	{
		if (inCooldown)
		{
			cooldown -= Time.deltaTime;
			if (cooldown <= 0.0f)
			{
				cooldown = 0.0f;
				inCooldown = false;
			}
		}
		else if (inCombo)
		{
			comboDelay -= Time.deltaTime;

			if (comboDelay <= 0.0f || combo == comboMax)
			{
				comboDelay = 0.0f;
				combo = 0;
				inCombo = false;
			}
		}
	}

	public void Attack(float attackSpeed, float comboDelay, int comboMax)
	{
		if (!CanAttack) return;

		cooldownMax = 1 / attackSpeed;
		cooldown = cooldownMax;

		this.comboMax = comboMax;
		this.comboDelayMax = comboDelay;
		this.comboDelay = comboDelayMax;

		combo++;

		inCooldown = true;
		inCombo = true;
	}
}
