using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDodgeComponent : MonoBehaviour
{
	private new Rigidbody rigidbody = null;

	private bool hasDodge = false;

    private float speed = 10.0f;
	private float duration = 0.0f;
	private float cooldown = 0.0f;

	private Vector3 direction = Vector3.zero;

	public bool HasDodge => hasDodge;
	public bool CanDodge => !hasDodge && cooldown == 0.0f;

	private void Awake()
	{
		rigidbody = GetComponent<Rigidbody>();
	}
	private void FixedUpdate()
	{
		if (!hasDodge) return;

		var newPosition = transform.position + direction * speed * Time.fixedDeltaTime;
		rigidbody.MovePosition(newPosition);
	}
	private void Update()
	{
		if (hasDodge)
		{
			duration -= Time.deltaTime;
			if (duration <= 0.0f)
				hasDodge = false;
		}

		if (cooldown != 0.0f)
		{
			cooldown -= Time.deltaTime;
			if (cooldown <= 0.0f)
				cooldown = 0.0f;
		}
	}

	public void Dodge(Vector3 direction, float distance, float speed, float cooldown)
	{
		if (!CanDodge) return;

		this.speed = speed;
		this.cooldown = cooldown;
		this.direction = direction;
		this.duration = distance / speed;

		transform.forward = direction;

		hasDodge = true;
	}
}
