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
		playerMovementComponent = GetComponent<PlayerMovementComponent>();
	}
	private void Update()
	{
		var moveInput = moveAction.ReadValue<Vector2>();
		var direction = new Vector3(moveInput.x, 0.0f, moveInput.y);

		if (!playerDodgeComponent.HasDodge && dodgeAction.triggered)
		{
			Dodge(direction);
		}
		else if (!playerAttackComponent.HasAttack && attackAction.triggered)
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
		player.State = PlayerState.Attack;
	}
	private void Move(Vector3 direction)
	{
		playerMovementComponent.Move(direction, player.Stats.MovementSpeed);

		if (direction != Vector3.zero)
			player.State = PlayerState.Move;
		else
			player.State = PlayerState.Idle;
	}
	private void Dodge(Vector3 direction)
	{
		playerMovementComponent.Stop();

		if (direction == Vector3.zero)
			direction = transform.forward;

		playerDodgeComponent.Dodge(direction, player.Stats.DodgeDistance, player.Stats.DodgeSpeed);

		player.State = PlayerState.Dodge;
	}
}
