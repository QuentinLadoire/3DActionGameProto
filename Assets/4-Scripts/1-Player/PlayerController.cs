using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInput playerInput = null;
	private InputAction moveAction = null;
	private InputAction dodgeAction = null;
	private InputAction attackAction = null;

	private Player player = null;
	private PlayerDodgeComponent playerDodgeComponent = null;
	private PlayerAttackComponent playerAttackComponent = null;
	private PlayerHealthComponent playerHealthComponent = null;
	private PlayerMovementComponent playerMovementComponent = null;

	private void Awake()
	{
		playerInput = GetComponent<PlayerInput>();
		if (playerInput != null)
		{
			moveAction = playerInput.actions["Move"];
			dodgeAction = playerInput.actions["Dodge"];
			attackAction = playerInput.actions["Attack"];
		}

		player = GetComponent<Player>();
		playerDodgeComponent = GetComponent<PlayerDodgeComponent>();
		playerAttackComponent = GetComponent<PlayerAttackComponent>();
		playerHealthComponent = GetComponent<PlayerHealthComponent>();
		playerMovementComponent = GetComponent<PlayerMovementComponent>();
	}
	private void Update()
	{
		if (player.State == PlayerState.Dead) return;

		var moveInput = moveAction.ReadValue<Vector2>();
		var direction = new Vector3(moveInput.x, 0.0f, moveInput.y);
		
		if (playerHealthComponent.IsDead && player.State != PlayerState.Dead)
		{
			player.State = PlayerState.Dead;
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

		player.State = PlayerState.Attack;

		UpdateAnimatorState();
		UpdateAnimatorComboCount();
	}
	private void Move(Vector3 direction)
	{
		playerMovementComponent.Move(direction);

		if (direction != Vector3.zero)
			player.State = PlayerState.Move;
		else
			player.State = PlayerState.Idle;

		UpdateAnimatorState();
	}
	private void Dodge(Vector3 direction)
	{
		playerMovementComponent.Stop();

		if (direction == Vector3.zero)
			direction = transform.forward;

		playerDodgeComponent.Dodge(direction);

		player.State = PlayerState.Dodge;
		UpdateAnimatorState();
	}

	private void UpdateAnimatorState()
	{
		player.Animator.SetInteger("PlayerState", (int)player.State);
	}
	private void UpdateAnimatorComboCount()
	{
		player.Animator.SetInteger("ComboCount", playerAttackComponent.Combo);
	}
}
