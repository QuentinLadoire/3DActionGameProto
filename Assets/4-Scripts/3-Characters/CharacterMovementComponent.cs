using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementComponent : MonoBehaviour
{
	private new Rigidbody rigidbody = null;

	//Parameters
	private float speed = 0.0f;

	//Processing Variables
	private bool move = false;
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
		if (!move) return;

		if (direction != Vector3.zero)
			transform.forward = direction;
	}

	public void Init(float speed)
	{
		this.speed = speed;
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
