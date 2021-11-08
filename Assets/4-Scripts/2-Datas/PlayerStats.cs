using UnityEngine;

[CreateAssetMenu(fileName = "NewEntityStats", menuName = "Data/Stats/EntityStats")]
public class PlayerStats : ScriptableObject
{
	[SerializeField] private int healthMax = 10;
	[SerializeField] private float movementSpeed = 5.0f;

	public int HealthMax => healthMax;
	public float MovementSpeed => movementSpeed;
}
