using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementComponent : MonoBehaviour
{
	private Character character = null;
	private new Rigidbody rigidbody = null;

	private bool move = false;
	private Vector3 direction = Vector3.zero;

	private void Awake()
	{
		character = GetComponent<Character>();
		rigidbody = GetComponent<Rigidbody>();
	}
	private void FixedUpdate()
	{
		if (!move) return;

		var desiredPosition = transform.position + direction * character.Stats.MovementSpeed * Time.fixedDeltaTime;
		rigidbody.MovePosition(desiredPosition);
	}
	private void Update()
	{
		if (!move) return;

		if (direction != Vector3.zero)
			transform.forward = direction;
	}

	public void Move(Vector3 direction)
	{
		this.direction = direction;

		move = true;
	}
	public void Stop()
	{
		direction = Vector3.zero;

		move = false;
	}
}