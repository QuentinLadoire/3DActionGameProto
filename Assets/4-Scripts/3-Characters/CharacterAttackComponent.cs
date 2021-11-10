using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterAttackComponent : MonoBehaviour
{
	private Character character = null;

	private float damageDelay = 0.0f;

	private float cooldown = 0.0f;
	private float cooldownMax = 0.0f;

	private int combo = 0;
	private float comboDelay = 0.0f;

	private bool inCombo = false;
	private bool inAttack = false;
	private bool inCooldown = false;

	public bool HasAttack => inCooldown;
	public bool CanAttack => !inCooldown;

	public bool InCombo => inCombo;
	public bool InCooldown => inCooldown;

	public float Cooldown => cooldown;
	public float CooldownInPercent => cooldown / cooldownMax;

	public int Combo => combo;
	public float ComboDelay => comboDelay;
	public float ComboDelayInPercent => comboDelay / character.Stats.AttackComboDelay;

	public UnityAction inComboCallback = () => { /*Debug.Log("InComboCallback");*/ };
	public UnityAction inCooldownCallback = () => { /*Debug.Log("InCooldownCallback");*/ };

	private void Awake()
	{
		character = GetComponent<Character>();
	}
	private void Update()
	{
		if (inAttack)
		{
			damageDelay -= Time.deltaTime;
			if (damageDelay <= 0.0f)
			{
				ApplyDamage();

				damageDelay = 0.0f;
				inAttack = false;
			}
		}

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

		if (!inCooldown && inCombo)
		{
			comboDelay -= Time.deltaTime;

			if (comboDelay <= 0.0f || combo == character.Stats.AttackComboMax)
			{
				comboDelay = 0.0f;
				combo = 0;
				inCombo = false;
			}

			inComboCallback.Invoke();
		}
	}

	private void ApplyDamage()
	{
		var halfSize = character.Stats.AttackRange * 0.5f;
		var center = transform.position + transform.up + transform.forward * halfSize;
		var size = new Vector3(halfSize, halfSize, halfSize);

		var colliders = Physics.OverlapBox(center, size);
		if (colliders == null || colliders.Length == 0) return;

		foreach (var collider in colliders)
		{
			if (collider.gameObject != gameObject) //if is not themself
			{
				var healthComponent = collider.GetComponentInParent<CharacterHealthComponent>();
				if (healthComponent != null)
				{
					healthComponent.TakeDamage(character.Stats.Damage);
				}
			}
		}
	}

	public void Attack()
	{
		if (!CanAttack) return;

		cooldownMax = 1 / character.Stats.AttackSpeed;
		cooldown = cooldownMax;

		this.comboDelay = character.Stats.AttackComboDelay;

		damageDelay = cooldownMax * character.Stats.DamageDelay;

		combo++;

		inCombo = true;
		inAttack = true;
		inCooldown = true;

		inComboCallback.Invoke();
	}

	private void OnDrawGizmos()
	{
		if (character == null) return;

		var attackRange = character.Stats.AttackRange;
		Gizmos.DrawWireCube(transform.position + transform.up + transform.forward * attackRange * 0.5f, new Vector3(attackRange, attackRange, attackRange));
	}
}
