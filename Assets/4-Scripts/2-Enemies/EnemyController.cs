using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
	[SerializeField] private EnemyData data = null;

	private Character character = null;
	private CharacterSensorComponent sensorComponent = null;
	private CharacterAttackComponent attackComponent = null;
	private CharacterHealthComponent healthComponent = null;
	private CharacterDespawnComponent despawnComponent = null;
	private CharacterNavMovementComponent navMovementComponent = null;

	private Vector3 spawnPosition = Vector3.zero;

	private void Awake()
	{
		character = GetComponent<Character>();
		sensorComponent = GetComponent<CharacterSensorComponent>();
		attackComponent = GetComponent<CharacterAttackComponent>();
		healthComponent = GetComponent<CharacterHealthComponent>();
		despawnComponent = GetComponent<CharacterDespawnComponent>();
		navMovementComponent = GetComponent<CharacterNavMovementComponent>();

		spawnPosition = transform.position;

		healthComponent.Init(data.HealthMax);
		navMovementComponent.Init(data.MovementSpeed);
		sensorComponent.Init(data.SensorFov, data.SensorRange);
		despawnComponent.Init(data.DespawnDelayMax, data.DespawnScalingDurationMax);
		attackComponent.Init(data.AttackRange, data.AttackSpeed, data.AttackComboMax, data.AttackComboDelayMax, data.Damage, data.DamageDelayMax);
	}
	private void Update()
	{
		if (character.State == CharacterState.Dead) return;

		if (healthComponent.IsDead && character.State != CharacterState.Dead)
		{

			Death();

			return;
		}

		if (sensorComponent.HasTarget)
		{
			if (IsInAttackRange())
			{
				if (attackComponent.CanAttack)
				{
					Attack();
				}
			}
			else
			{
				MoveTo(sensorComponent.Target.transform.position);
			}
		}
		else
		{
			if (sensorComponent.HasLastTargetPosition)
			{
				if (HasReachLastTargetPosition())
				{
					Idle();
				}
				else
				{
					MoveTo(sensorComponent.LastTargetPosition);
				}
			}
			else
			{
				if (HasReachSpawnPosition())
				{
					Idle();
				}
				else
				{
					MoveTo(spawnPosition);
				}
			}
		}
	}

	private void Idle()
	{
		navMovementComponent.Stop();

		character.State = CharacterState.Idle;
		UpdateAnimatorState();
	}
	private void Death()
	{
		despawnComponent.Despawn();

		character.State = CharacterState.Dead;
		UpdateAnimatorState();
	}
	private void Attack()
	{
		navMovementComponent.Stop();

		attackComponent.Attack();

		character.State = CharacterState.Attack;

		UpdateAnimatorState();
		UpdateAnimatorComboCount();
	}
	private void MoveTo(Vector3 destination)
	{
		navMovementComponent.MoveTo(destination);

		character.State = CharacterState.Move;
		UpdateAnimatorState();
	}

	private bool IsInAttackRange()
	{
		var targetPosition = sensorComponent.Target.transform.position;
		var sqrMagnitude = (targetPosition - transform.position).sqrMagnitude;

		return sqrMagnitude < data.AttackRange * data.AttackRange;
	}
	private bool HasReachLastTargetPosition()
	{
		var sqrMagnitude = (sensorComponent.LastTargetPosition - transform.position).sqrMagnitude;

		return sqrMagnitude < 0.09f;
	}
	private bool HasReachSpawnPosition()
	{
		var sqrMagnitude = (spawnPosition - transform.position).sqrMagnitude;

		return sqrMagnitude < 0.09f;
	}

	private void UpdateAnimatorState()
	{
		character.Animator.SetInteger("PlayerState", (int)character.State);
	}
	private void UpdateAnimatorComboCount()
	{
		character.Animator.SetInteger("ComboCount", attackComponent.Combo);
	}
}
