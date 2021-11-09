using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackComponent : MonoBehaviour
{
	private bool hasAttack = false;

	private int comboMax = 0;
	private int currentCombo = 0;

	private float comboDelay = 0.0f;
	private float attackDelay = 0.0f;

    public bool HasAttack => hasAttack;
	public bool CanAttack => !hasAttack;
	public int CurrentCombo => currentCombo;

	private void Update()
	{
		if (hasAttack)
		{
			attackDelay -= Time.deltaTime;
			if (attackDelay <= 0.0f)
			{
				hasAttack = false;
			}
		}
		else if (currentCombo != 0)
		{
			if (currentCombo == comboMax)
			{
				currentCombo = 0;
			}
			else
			{
				comboDelay -= Time.deltaTime;
				if (comboDelay <= 0.0f)
					currentCombo = 0;
			}
		}
	}

	public void Attack(float attackSpeed, float comboDelay, int comboMax)
	{
		if (!CanAttack) return;

		this.comboMax = comboMax;
		this.comboDelay = comboDelay;
		attackDelay = 1 / attackSpeed;

		currentCombo++;

		hasAttack = true;
	}
}
