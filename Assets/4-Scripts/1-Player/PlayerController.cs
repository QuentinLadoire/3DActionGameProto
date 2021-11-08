using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInput playerInput = null;
	private InputAction moveAction = null;
	private InputAction dashAction = null;
	private InputAction attackAction = null;

	private Player player = null;
	private PlayerDodgeComponent playerDodge = null;
	private PlayerMovementComponent playerMovement = null;

	private void Awake()
	{
		playerInput = GetComponent<PlayerInput>();
		if (playerInput != null)
		{
			moveAction = playerInput.actions["Move"];
			dashAction = playerInput.actions["Dodge"];
			attackAction = playerInput.actions["Attack"];
		}

		player = GetComponent<Player>();
		playerMovement = GetComponent<PlayerMovementComponent>();
	}
	private void Update()
	{
		var moveInput = moveAction.ReadValue<Vector2>();
		playerMovement.direction = new Vector3(moveInput.x, 0.0f, moveInput.y);
		playerMovement.movementSpeed = player.Stats.MovementSpeed;
	}
}
