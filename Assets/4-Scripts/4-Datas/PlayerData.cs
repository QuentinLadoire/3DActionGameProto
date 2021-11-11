using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerData", menuName = "Data/PlayerData")]
public class PlayerData : ScriptableObject
{
	[Header("Health Stats")]
	[SerializeField] private int healthMax = 10;

	[Header("Movement Stats")]
	[SerializeField] private float movementSpeed = 5.0f;

	[Header("Dodge Stats")]
	[SerializeField] private float dodgeSpeed = 10.0f;
	[SerializeField] private float dodgeDistance = 2.0f;
	[SerializeField] private float dodgeCooldown = 5.0f;

	[Header("Attack Stats")]
	[SerializeField] private int attackComboMax = 3;
	[SerializeField] private float attackSpeed = 0.5f;
	[SerializeField] private float attackRange = 2.0f;
	[SerializeField] private float attackComboDelay = 0.6f;

	[SerializeField] private int damage = 1;
	[SerializeField] private float damageDelay = 0.5f;


	public int HealthMax => healthMax;

	public float MovementSpeed => movementSpeed;

	public float DodgeSpeed => dodgeSpeed;
	public float DodgeDistance => dodgeDistance;
	public float DodgeCooldown => dodgeCooldown;

	public int AttackComboMax => attackComboMax;
	public float AttackSpeed => attackSpeed;
	public float AttackRange => attackRange;
	public float AttackComboDelayMax => attackComboDelay;

	public int Damage => damage;
	public float DamageDelayMax => damageDelay;
}
