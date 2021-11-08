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

	private Vector3 moveDirection = Vector3.zero;

	private void Awake()
	{
		rigidbody = GetComponent<Rigidbody>();

		playerInput = GetComponent<PlayerInput>();
		if (playerInput != null)
		{
			moveAction = playerInput.actions["Move"];
			dashAction = playerInput.actions["Dodge"];
			attackAction = playerInput.actions["Attack"];
		}
	}
	private void Start()
	{
		
	}
	private void Update()
	{
		var moveInput = moveAction.ReadValue<Vector2>();
		moveDirection = new Vector3(moveInput.x, 0.0f, moveInput.y);
	}
	private void FixedUpdate()
	{		
		var desiredPosition = transform.position + moveDirection * movementSpeed * Time.deltaTime;
		rigidbody.MovePosition(desiredPosition);
	}
	private void OnDestroy()
	{
		
	}
}
