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
	
	private PlayerController controller = null;
	private CharacterDodgeComponent dodgeComponent = null;
	private CharacterAttackComponent attackComponent = null;
	private CharacterHealthComponent healthComponent = null;
	private CharacterMovementComponent movementComponent = null;

    public PlayerStats Stats => stats;
	public Animator Animator => animator;
	public CharacterState State { get; set; } = CharacterState.None;

	public PlayerController Controller => controller;
	public CharacterDodgeComponent DodgeComponent => dodgeComponent;
	public CharacterAttackComponent AttackComponent => attackComponent;
	public CharacterHealthComponent HealthComponent => healthComponent;
	public CharacterMovementComponent MovementComponent => movementComponent;

	private void Awake()
	{
		animator = GetComponent<Animator>();

		controller = GetComponent<PlayerController>();
		dodgeComponent = GetComponent<CharacterDodgeComponent>();
		attackComponent = GetComponent<CharacterAttackComponent>();
		healthComponent = GetComponent<CharacterHealthComponent>();
		movementComponent = GetComponent<CharacterMovementComponent>();
	}
}
