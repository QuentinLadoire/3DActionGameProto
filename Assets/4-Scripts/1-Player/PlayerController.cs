using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
	public float movementSpeed = 5.0f;

	private new Rigidbody rigidbody = null;

    private PlayerInput playerInput = null;

	private InputAction moveAction = null;
	private InputAction dashAction = null;
	private InputAction attackAction = null;

	private Vector2 moveInput = Vector2.zero;

	private void Awake()
	{
		rigidbody = GetComponent<Rigidbody>();

		playerInput = GetComponent<PlayerInput>();
		if (playerInput != null)
		{
			moveAction = playerInput.actions["Move"];
			dashAction = playerInput.actions["Dash"];
			attackAction = playerInput.actions["Attack"];
		}
	}
	private void Start()
	{
		
	}
	private void Update()
	{
		moveInput = moveAction.ReadValue<Vector2>();
	}
	private void FixedUpdate()
	{
		var direction = new Vector3(moveInput.x, 0.0f, moveInput.y);
		var desiredPosition = transform.position + direction * movementSpeed * Time.deltaTime;
		rigidbody.MovePosition(desiredPosition);
	}
	private void OnDestroy()
	{
		
	}
}
