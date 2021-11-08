using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementComponent : MonoBehaviour
{
	private new Rigidbody rigidbody = null;

	private bool move = false;
	private float speed = 5.0f;
	private Vector3 direction = Vector3.zero;

	private void Awake()
	{
		rigidbody = GetComponent<Rigidbody>();
	}
	private void FixedUpdate()
	{
		if (!move) return;

		var desiredPosition = transform.position + direction * speed * Time.fixedDeltaTime;
		rigidbody.MovePosition(desiredPosition);
	}
	private void Update()
	{
		if (direction != Vector3.zero)
			transform.forward = direction;
	}

	public void Move(Vector3 direction, float speed)
	{
		this.direction = direction;
		this.speed = speed;

		move = true;
	}
	public void Stop()
	{
		direction = Vector3.zero;
		speed = 0.0f;

		move = false;
	}
}
