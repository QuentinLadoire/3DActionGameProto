using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDodgeComponent : MonoBehaviour
{
	private new Rigidbody rigidbody = null;

    private float speed = 10.0f;
	private float duration = 0.0f;
	private float cooldown = 0.0f;
	private float cooldownMax = 0.0f;
	private Vector3 direction = Vector3.zero;

	private bool hasDodge = false;
	private bool inCooldown = false;

	public bool HasDodge => hasDodge;
	public bool CanDodge => !hasDodge && cooldown == 0.0f;

	public float Cooldown => cooldown;
	public float CooldownInPercent => cooldown / cooldownMax;

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
			{
				duration = 0.0f;
				hasDodge = false;
			}
		}

		if (inCooldown)
		{
			cooldown -= Time.deltaTime;
			if (cooldown <= 0.0f)
			{
				cooldown = 0.0f;
				inCooldown = false;
			}
		}
	}

	public void Dodge(Vector3 direction, float distance, float speed, float cooldown)
	{
		if (!CanDodge) return;

		this.speed = speed;
		this.direction = direction;
		this.duration = distance / speed;

		this.cooldown = cooldownMax;
		this.cooldownMax = cooldown;

		transform.forward = direction;

		hasDodge = true;
		inCooldown = true;
	}
}
