using UnityEngine;

public enum CharacterState
{
	None = -1,
	Idle,
	Move,
	Dodge,
	Attack,
	Dead
}

public class Character : MonoBehaviour
{
    [SerializeField] private PlayerStats stats = null;

	private Animator animator = null;

	public PlayerStats Stats => stats;
	public Animator Animator => animator;
	public CharacterState State { get; set; } = CharacterState.None;

	private void Awake()
	{
		animator = GetComponent<Animator>();
	}
}
