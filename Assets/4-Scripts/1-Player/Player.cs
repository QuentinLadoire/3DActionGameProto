using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayerState
{
	None = -1,
	Idle,
	Move,
	Dodge,
	Attack,
	Dead
}

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerStats stats = null;

	private Animator animator = null;

	private PlayerController controller = null;
	private PlayerDodgeComponent dodgeComponent = null;
	private PlayerAttackComponent attackComponent = null;
	private PlayerHealthComponent healthComponent = null;
	private PlayerMovementComponent movementComponent = null;

    public PlayerStats Stats => stats;
	public Animator Animator => animator;
	public PlayerState State { get; set; } = PlayerState.None;

	public PlayerController Controller => controller;
	public PlayerDodgeComponent DodgeComponent => dodgeComponent;
	public PlayerAttackComponent AttackComponent => attackComponent;
	public PlayerHealthComponent HealthComponent => healthComponent;
	public PlayerMovementComponent MovementComponent => movementComponent;

	private void Awake()
	{
		animator = GetComponent<Animator>();

		controller = GetComponent<PlayerController>();
		dodgeComponent = GetComponent<PlayerDodgeComponent>();
		attackComponent = GetComponent<PlayerAttackComponent>();
		healthComponent = GetComponent<PlayerHealthComponent>();
		movementComponent = GetComponent<PlayerMovementComponent>();
	}
}
