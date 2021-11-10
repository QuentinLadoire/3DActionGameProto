using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSensorComponent : MonoBehaviour
{
	private float fov = 90.0f;
	private float radius = 15.0f;
	private float halfFov = 45.0f;
	private float sqrRadius = 225.0f;

	private float processDelay = 0.5f;
	private float currentProcessDelay = 0.0f;

	private bool hasTarget = false;
	private bool hasLastTargetPosition = false;

	private GameObject target = null;
	private Vector3 lastTargetPosition = Vector3.zero;

	public bool HasTarget => hasTarget;
	public bool HasLastTargetPosition => hasLastTargetPosition;

	public GameObject Target => target;
	public Vector3 LastTargetPosition => lastTargetPosition;

	private void Start()
	{
		halfFov = fov * 0.5f;
		sqrRadius = radius * radius;

		lastTargetPosition = transform.position;
	}
	private void Update()
	{
		currentProcessDelay -= Time.deltaTime;
		if (currentProcessDelay <= 0.0f)
		{
			Process();

			currentProcessDelay = processDelay;
		}
	}

	private void Process()
	{
		var player = GameManager.PlayerController;

		hasTarget = false;
		hasLastTargetPosition = false;

		if (target != null)
		{
			lastTargetPosition = target.transform.position;
			hasLastTargetPosition = true;
		}

		target = null;

		var direction = (player.transform.position - transform.position);
		var sqrMagnitude = direction.sqrMagnitude;
		if (sqrMagnitude <= sqrRadius) //In Range
		{
			var dot = Vector3.Dot(transform.forward, direction);
			if (dot >= 0.0f) //In Front
			{
				var angle = Vector3.Angle(transform.forward, direction);
				if (angle <= halfFov) //In Fov
				{
					target = player.gameObject;

					hasTarget = true;
				}
			}
		}
	}

	private void OnDrawGizmos()
	{
		var from = Quaternion.AngleAxis(-halfFov, transform.up) * transform.forward;
		var to = Quaternion.AngleAxis(halfFov, transform.up) * transform.forward;

		Gizmos.DrawLine(transform.position, transform.position + from * radius);
		Gizmos.DrawLine(transform.position, transform.position + to * radius);
		#if UNITY_EDITOR
		UnityEditor.Handles.DrawWireArc(transform.position, transform.up, from, fov, radius);
		#endif
	}
}
