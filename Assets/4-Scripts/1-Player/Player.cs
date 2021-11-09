using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayerState
{
	None = -1,
	Idle,
	Move,
	Dodge,
	Attack
}

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerStats stats = null;

	private Animator animator = null;

	private PlayerController controller = null;
	private PlayerDodgeComponent dodgeComponent = null;
	private PlayerAttackComponent attackComponent = null;
	private PlayerMovementComponent movementComponent = null;

    public PlayerStats Stats => stats;
	public Animator Animator => animator;
	public PlayerState State { get; set; } = PlayerState.None;

	public PlayerController Controller => controller;
	public PlayerDodgeComponent DodgeComponent => dodgeComponent;
	public PlayerAttackComponent AttackComponent => attackComponent;
	public PlayerMovementComponent MovementComponent => movementComponent;

	private void Awake()
	{
		animator = GetComponent<Animator>();

		controller = GetComponent<PlayerController>();
		dodgeComponent = GetComponent<PlayerDodgeComponent>();
		attackComponent = GetComponent<PlayerAttackComponent>();
		movementComponent = GetComponent<PlayerMovementComponent>();
	}
}
