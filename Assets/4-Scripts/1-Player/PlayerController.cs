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
		playerMovementComponent = GetComponent<PlayerMovementComponent>();
	}
	private void Update()
	{
		if (dodgeAction.triggered && !playerDodgeComponent.HasDodge)
		{
			playerMovementComponent.Stop();
		
			var moveInput = moveAction.ReadValue<Vector2>();
			var direction = new Vector3(moveInput.x, 0.0f, moveInput.y);
			if (direction == Vector3.zero)
				direction = transform.forward;
		
			playerDodgeComponent.Dodge(direction, player.Stats.DodgeDistance, player.Stats.DodgeSpeed);
		}
		else if (!playerDodgeComponent.HasDodge)
		{
			var moveInput = moveAction.ReadValue<Vector2>();
			var direction = new Vector3(moveInput.x, 0.0f, moveInput.y);
		
			playerMovementComponent.Move(direction, player.Stats.MovementSpeed);
		}
	}
}
