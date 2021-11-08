using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	private new Rigidbody rigidbody = null;

	//set by Player Controller
	public float movementSpeed = 5.0f;
	public Vector3 direction = Vector3.zero;

	private void Awake()
	{
		rigidbody = GetComponent<Rigidbody>();
	}
	private void FixedUpdate()
	{
		var desiredPosition = transform.position + direction * movementSpeed * Time.deltaTime;
		rigidbody.MovePosition(desiredPosition);
	}
}
