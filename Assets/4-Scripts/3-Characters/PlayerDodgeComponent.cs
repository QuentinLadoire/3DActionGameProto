using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerDodgeComponent : MonoBehaviour
{
	private Player player = null;
	private new Rigidbody rigidbody = null;

	private float duration = 0.0f;
	private float cooldown = 0.0f;
	private Vector3 direction = Vector3.zero;

	private bool hasDodge = false;
	private bool inCooldown = false;

	public bool HasDodge => hasDodge;
	public bool InCooldown => inCooldown;
	public bool CanDodge => !hasDodge && !inCooldown;

	public float Cooldown => cooldown;
	public float CooldownInPercent => cooldown / player.Stats.DodgeCooldown;

	public UnityAction inCooldownCallback = () => { /*Debug.Log("InCooldownCallback");*/ };

	private void Awake()
	{
		player = GetComponent<Player>();
		rigidbody = GetComponent<Rigidbody>();
	}
	private void FixedUpdate()
	{
		if (!hasDodge) return;

		var newPosition = transform.position + direction * player.Stats.DodgeSpeed * Time.fixedDeltaTime;
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

			inCooldownCallback.Invoke();
		}
	}

	public void Dodge(Vector3 direction)
	{
		if (!CanDodge) return;

		this.direction = direction;
		this.duration = player.Stats.DodgeDistance / player.Stats.DodgeSpeed;

		this.cooldown = player.Stats.DodgeCooldown;

		transform.forward = direction;

		hasDodge = true;
		inCooldown = true;
	}
}
