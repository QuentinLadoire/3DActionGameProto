using UnityEngine;

[CreateAssetMenu(fileName = "NewEntityStats", menuName = "Data/Stats/EntityStats")]
public class PlayerStats : ScriptableObject
{
	[Header("Health Stats")]
	[SerializeField] private int healthMax = 10;

	[Header("Movement Stats")]
	[SerializeField] private float movementSpeed = 5.0f;

	[Header("Dodge Stats")]
	[SerializeField] private float dodgeSpeed = 10.0f;
	[SerializeField] private float dodgeDistance = 2.0f;
	[SerializeField] private float dodgeCooldown = 5.0f;


	public int HealthMax => healthMax;

	public float MovementSpeed => movementSpeed;

	public float DodgeSpeed => dodgeSpeed;
	public float DodgeDistance => dodgeDistance;
	public float DodgeCooldown => dodgeCooldown;

}
