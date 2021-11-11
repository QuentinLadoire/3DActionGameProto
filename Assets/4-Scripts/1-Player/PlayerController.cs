using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private PlayerData data = null;

    private PlayerInput playerInput = null;
	private InputAction moveAction = null;
	private InputAction dodgeAction = null;
	private InputAction attackAction = null;

	private Character character = null;
	private CharacterDodgeComponent dodgeComponent = null;
	private CharacterAttackComponent attackComponent = null;
	private CharacterHealthComponent healthComponent = null;
	private CharacterMovementComponent movementComponent = null;

	public Character Character => character;
	public CharacterDodgeComponent DodgeComponent => dodgeComponent;
	public CharacterAttackComponent AttackComponent => attackComponent;
	public CharacterHealthComponent HealthComponent => healthComponent;
	public CharacterMovementComponent MovementComponent => movementComponent;

	private void Awake()
	{
		playerInput = GetComponent<PlayerInput>();
		if (playerInput != null)
		{
			moveAction = playerInput.actions["Move"];
			dodgeAction = playerInput.actions["Dodge"];
			attackAction = playerInput.actions["Attack"];
		}

		character = GetComponent<Character>();
		dodgeComponent = GetComponent<CharacterDodgeComponent>();
		attackComponent = GetComponent<CharacterAttackComponent>();
		healthComponent = GetComponent<CharacterHealthComponent>();
		movementComponent = GetComponent<CharacterMovementComponent>();

		healthComponent.Init(data.HealthMax);
		movementComponent.Init(data.MovementSpeed);
		dodgeComponent.Init(data.DodgeSpeed, data.DodgeDistance, data.DodgeCooldown);
		attackComponent.Init(data.AttackRange, data.AttackSpeed, data.AttackComboMax, data.AttackComboDelayMax, data.Damage, data.DamageDelayMax);
	}
	private void Update()
	{
		if (character.State == CharacterState.Dead) return;

		var moveInput = moveAction.ReadValue<Vector2>();
		var direction = new Vector3(moveInput.x, 0.0f, moveInput.y);

		if (healthComponent.IsDead && character.State != CharacterState.Dead)
		{
			character.State = CharacterState.Dead;
			SetAnimatorState();
		}
		else if (dodgeComponent.CanDodge && dodgeAction.triggered)
		{
			Dodge(direction);
		}
		else if (attackComponent.CanAttack && attackAction.triggered)
		{
			Attack();
		}
		else if (!dodgeComponent.HasDodge && !attackComponent.HasAttack)
		{
			Move(direction);
		}
	}

	private void Attack()
	{
		healthComponent.Invulnerable = false;

		movementComponent.Stop();

		attackComponent.Attack();

		character.State = CharacterState.Attack;

		SetAnimatorState();
		SetAnimatorComboCount();
		SetAnimatorAttackMultiplier();
	}
	private void Move(Vector3 direction)
	{
		healthComponent.Invulnerable = false;

		movementComponent.Move(direction);

		if (direction != Vector3.zero)
			character.State = CharacterState.Move;
		else
			character.State = CharacterState.Idle;

		SetAnimatorState();
		SetAnimatorMoveMultiplier(direction.magnitude);
	}
	private void Dodge(Vector3 direction)
	{
		movementComponent.Stop();

		healthComponent.Invulnerable = true;

		if (direction == Vector3.zero)
			direction = transform.forward;

		dodgeComponent.Dodge(direction);

		character.State = CharacterState.Dodge;

		SetAnimatorState();
		SetAnimatorDodgeMultiplier();
	}

	private void SetAnimatorState()
	{
		character.Animator.SetInteger("PlayerState", (int)character.State);
	}
	private void SetAnimatorComboCount()
	{
		character.Animator.SetInteger("ComboCount", attackComponent.Combo);
	}

	private void SetAnimatorDodgeMultiplier()
	{
		var value = 0.5f * (data.DodgeSpeed / data.DodgeDistance);
		character.Animator.SetFloat("DodgeMultiplier", value);
	}
	private void SetAnimatorAttackMultiplier()
	{
		var value = 0.5f * data.AttackSpeed;
		character.Animator.SetFloat("AttackMultiplier", value);
	}
	private void SetAnimatorMoveMultiplier(float inputMultiplier)
	{
		var value = 0.5f * (data.MovementSpeed / 2.0f) * inputMultiplier;
		character.Animator.SetFloat("MoveMultiplier", value);
	}
}
