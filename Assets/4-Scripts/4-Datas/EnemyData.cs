using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyData", menuName = "Data/EnemyData")]
public class EnemyData : ScriptableObject
{
	[Header("Health Component")]
	[SerializeField] private int healthMax = 10;

	[Header("Movement Component")]
	[SerializeField] private float movementSpeed = 2.5f;

	[Header("Attack Component")]
	[SerializeField] private int attackComboMax = 3;
	[SerializeField] private float attackSpeed = 1.0f;
	[SerializeField] private float attackRange = 2.0f;
	[SerializeField] private float attackComboDelay = 0.6f;

	[SerializeField] private int damage = 1;
	[SerializeField] private float damageDelay = 0.5f;

	[Header("Sensor Component")]
	[SerializeField] private float sensorFov = 90.0f;
	[SerializeField] private float sensorRange = 15.0f;


	public int HealthMax => healthMax;

	public float MovementSpeed => movementSpeed;

	public int AttackComboMax => attackComboMax;
	public float AttackSpeed => attackSpeed;
	public float AttackRange => attackRange;
	public float AttackComboDelayMax => attackComboDelay;

	public int Damage => damage;
	public float DamageDelayMax => damageDelay;

	public float SensorFov => sensorFov;
	public float SensorRange => sensorRange;
}
