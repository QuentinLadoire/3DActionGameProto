using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInput playerInput = null;
	private InputAction moveAction = null;
	private InputAction dodgeAction = null;
	private InputAction attackAction = null;

	private Character character = null;
	private CharacterDodgeComponent playerDodgeComponent = null;
	private CharacterAttackComponent playerAttackComponent = null;
	private CharacterHealthComponent playerHealthComponent = null;
	private CharacterMovementComponent playerMovementComponent = null;

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
		playerDodgeComponent = GetComponent<CharacterDodgeComponent>();
		playerAttackComponent = GetComponent<CharacterAttackComponent>();
		playerHealthComponent = GetComponent<CharacterHealthComponent>();
		playerMovementComponent = GetComponent<CharacterMovementComponent>();
	}
	private void Update()
	{
		if (character.State == CharacterState.Dead) return;

		var moveInput = moveAction.ReadValue<Vector2>();
		var direction = new Vector3(moveInput.x, 0.0f, moveInput.y);
		
		if (playerHealthComponent.IsDead && character.State != CharacterState.Dead)
		{
			character.State = CharacterState.Dead;
			UpdateAnimatorState();
		}
		else if (playerDodgeComponent.CanDodge && dodgeAction.triggered)
		{
			Dodge(direction);
		}
		else if (playerAttackComponent.CanAttack && attackAction.triggered)
		{
			Attack();
		}
		else if (!playerDodgeComponent.HasDodge && !playerAttackComponent.HasAttack)
		{
			Move(direction);
		}
	}

	private void Attack()
	{
		playerMovementComponent.Stop();

		playerAttackComponent.Attack();

		character.State = CharacterState.Attack;

		UpdateAnimatorState();
		UpdateAnimatorComboCount();
	}
	private void Move(Vector3 direction)
	{
		playerMovementComponent.Move(direction);

		if (direction != Vector3.zero)
			character.State = CharacterState.Move;
		else
			character.State = CharacterState.Idle;

		UpdateAnimatorState();
	}
	private void Dodge(Vector3 direction)
	{
		playerMovementComponent.Stop();

		if (direction == Vector3.zero)
			direction = transform.forward;

		playerDodgeComponent.Dodge(direction);

		character.State = CharacterState.Dodge;
		UpdateAnimatorState();
	}

	private void UpdateAnimatorState()
	{
		character.Animator.SetInteger("PlayerState", (int)character.State);
	}
	private void UpdateAnimatorComboCount()
	{
		character.Animator.SetInteger("ComboCount", playerAttackComponent.Combo);
	}
}
