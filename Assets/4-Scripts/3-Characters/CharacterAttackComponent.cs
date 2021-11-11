using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterAttackComponent : MonoBehaviour
{
	//Parameters
	private int damage = 0;
	private float damageDelayMax = 0.0f;

	private float attackRange = 0.0f;
	private float attackSpeed = 0.0f;

	private int comboMax = 0;
	private float comboDelayMax = 0.0f;

	//Processing Variables
	private float damageDelay = 0.0f;

	private float cooldown = 0.0f;
	private float cooldownMax = 0.0f;

	private int combo = 0;
	private float comboDelay = 0.0f;

	private bool inCombo = false;
	private bool inAttack = false;
	private bool inCooldown = false;

	//Accessors
	public bool HasAttack => inCooldown;
	public bool CanAttack => !inCooldown;

	public bool InCombo => inCombo;
	public bool InCooldown => inCooldown;

	public float Cooldown => cooldown;
	public float CooldownInPercent => cooldown / cooldownMax;

	public int Combo => combo;
	public float ComboDelay => comboDelay;
	public float ComboDelayInPercent => comboDelay / comboDelayMax;

	//Callbacks
	public UnityAction inComboCallback = () => { /*Debug.Log("InComboCallback");*/ };
	public UnityAction inCooldownCallback = () => { /*Debug.Log("InCooldownCallback");*/ };

	private void Update()
	{
		UpdateDamage();

		UpdateCooldown();

		UpdateCombo();
	}

	private void ResetProcess()
	{
		cooldownMax = 1 / attackSpeed;
		cooldown = cooldownMax;

		damageDelay = cooldownMax * damageDelayMax;

		comboDelay = comboDelayMax;

		inCombo = true;
		inAttack = true;
		inCooldown = true;

		inComboCallback.Invoke();
		inCooldownCallback.Invoke();
	}
	private void ApplyDamage()
	{
		var halfSize = attackRange * 0.5f;
		var center = transform.position + transform.up + transform.forward * halfSize;
		var size = new Vector3(halfSize, halfSize, halfSize);

		var colliders = Physics.OverlapBox(center, size, transform.rotation);
		if (colliders == null || colliders.Length == 0) return;

		foreach (var collider in colliders)
		{
			var healthComponent = collider.GetComponentInParent<CharacterHealthComponent>();
			if (healthComponent != null && healthComponent.gameObject != gameObject)
			{
				healthComponent.TakeDamage(damage);
			}
		}
	}

	private void UpdateDamage()
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
	}
	private void UpdateCooldown()
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
	}
	private void UpdateCombo()
	{
		if (!inCooldown && inCombo)
		{
			comboDelay -= Time.deltaTime;

			if (comboDelay <= 0.0f || combo == comboMax)
			{
				comboDelay = 0.0f;
				combo = 0;
				inCombo = false;
			}

			inComboCallback.Invoke();
		}
	}

	public void Init(float attackRange, float attackSpeed, int comboMax, float comboDelayMax, int damage, float damageDelayMax)
	{
		this.attackRange = attackRange;
		this.attackSpeed = attackSpeed;

		this.comboMax = comboMax;
		this.comboDelayMax = comboDelayMax;

		this.damage = damage;
		this.damageDelayMax = damageDelayMax;
	}
	public void Attack()
	{
		if (!CanAttack) return;

		ResetProcess();

		combo++;
	}

	private void OnDrawGizmos()
	{
		Gizmos.matrix = transform.localToWorldMatrix;
		Gizmos.DrawWireCube(Vector3.up + Vector3.forward * attackRange * 0.5f, new Vector3(attackRange, attackRange, attackRange));
		Gizmos.matrix = Matrix4x4.identity;
	}
}
