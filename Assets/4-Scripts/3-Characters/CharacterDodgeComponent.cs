using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterDodgeComponent : MonoBehaviour
{
	private new Rigidbody rigidbody = null;

	//Parameters
	private float speed = 0.0f;
	private float distance = 0.0f;
	private float cooldownMax = 0.0f;

	//Processing variables
	private float duration = 0.0f;
	private float cooldown = 0.0f;
	private Vector3 direction = Vector3.zero;

	private bool hasDodge = false;
	private bool inCooldown = false;

	//Accessors
	public bool HasDodge => hasDodge;
	public bool InCooldown => inCooldown;
	public bool CanDodge => !hasDodge && !inCooldown;

	public float Cooldown => cooldown;
	public float CooldownInPercent => cooldown / cooldownMax;

	//Callbacks
	public UnityAction inCooldownCallback = () => { /*Debug.Log("InCooldownCallback");*/ };

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
		UpdateDuration();

		UpdateCooldown();
	}

	private void ResetProcess(Vector3 newDirection)
	{
		duration = distance / speed;
		cooldown = cooldownMax;
		direction = newDirection;

		hasDodge = true;
		inCooldown = true;
	}

	private void UpdateDuration()
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
	}
	private void UpdateCooldown()
	{
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

	public void Init(float speed, float distance, float cooldownMax)
	{
		this.speed = speed;
		this.distance = distance;
		this.cooldownMax = cooldownMax;
	}
	public void Dodge(Vector3 direction)
	{
		if (!CanDodge) return;

		ResetProcess(direction);

		transform.forward = direction;
	}
}
