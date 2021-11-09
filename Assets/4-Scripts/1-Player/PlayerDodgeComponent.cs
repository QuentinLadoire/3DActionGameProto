using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDodgeComponent : MonoBehaviour
{
	private new Rigidbody rigidbody = null;

	private bool dodge = false;
    private float speed = 10.0f;
	private float duration = 0.0f;
	private Vector3 direction = Vector3.zero;

	public bool HasDodge => dodge;

	private void Awake()
	{
		rigidbody = GetComponent<Rigidbody>();
	}
	private void FixedUpdate()
	{
		if (!dodge) return;

		var newPosition = transform.position + direction * speed * Time.fixedDeltaTime;
		rigidbody.MovePosition(newPosition);

		duration -= Time.fixedDeltaTime;
		if (duration <= 0.0f)
			dodge = false;
	}
	private void Update()
	{
		if (!dodge) return;

		if (direction != Vector3.zero)
			transform.forward = direction;
	}

	public void Dodge(Vector3 direction, float distance, float speed)
	{
		this.speed = speed;
		this.direction = direction;
		this.duration = distance / speed;

		dodge = true;
	}
}
