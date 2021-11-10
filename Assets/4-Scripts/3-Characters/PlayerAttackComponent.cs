using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAttackComponent : MonoBehaviour
{
	private Player player = null;

	private float cooldown = 0.0f;
	private float cooldownMax = 0.0f;

	private int combo = 0;
	private float comboDelay = 0.0f;

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
	public float ComboDelayInPercent => comboDelay / player.Stats.AttackComboDelay;

	public UnityAction inComboCallback = () => { /*Debug.Log("InComboCallback");*/ };
	public UnityAction inCooldownCallback = () => { /*Debug.Log("InCooldownCallback");*/ };

	private void Awake()
	{
		player = GetComponent<Player>();
	}
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

			inCooldownCallback.Invoke();
		}
		else if (inCombo)
		{
			comboDelay -= Time.deltaTime;

			if (comboDelay <= 0.0f || combo == player.Stats.AttackComboMax)
			{
				comboDelay = 0.0f;
				combo = 0;
				inCombo = false;
			}

			inComboCallback.Invoke();
		}
	}

	public void Attack()
	{
		if (!CanAttack) return;

		cooldownMax = 1 / player.Stats.AttackSpeed;
		cooldown = cooldownMax;

		this.comboDelay = player.Stats.AttackComboDelay;

		combo++;

		inCooldown = true;
		inCombo = true;

		inComboCallback.Invoke();
	}
}
