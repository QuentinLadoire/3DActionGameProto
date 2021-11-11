using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSensorComponent : MonoBehaviour
{
	//Parameters
	private float fov = 90.0f;
	private float range = 15.0f;
	private float halfFov = 45.0f;
	private float sqrRange = 225.0f;
	private float reactionDelay = 0.5f;

	//Processing Variables
	private float currentReactionDelay = 0.0f;

	private bool hasTarget = false;
	private bool hasLastTargetPosition = false;

	private GameObject target = null;
	private Vector3 lastTargetPosition = Vector3.zero;

	//Accessors
	public bool HasTarget => hasTarget;
	public bool HasLastTargetPosition => hasLastTargetPosition;

	public GameObject Target => target;
	public Vector3 LastTargetPosition => lastTargetPosition;

	private void Start()
	{
		halfFov = fov * 0.5f;
		sqrRange = range * range;

		lastTargetPosition = transform.position;
	}
	private void Update()
	{
		currentReactionDelay -= Time.deltaTime;
		if (currentReactionDelay <= 0.0f)
		{
			Process();

			currentReactionDelay = reactionDelay;
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
		if (sqrMagnitude <= sqrRange) //In Range
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

	public void Init(float fov, float range, float reactionDelay)
	{
		this.fov = fov;
		this.range = range;
		this.reactionDelay = reactionDelay;

		halfFov = fov * 0.5f;
		sqrRange = range * range;
	}

	private void OnDrawGizmos()
	{
		var from = Quaternion.AngleAxis(-halfFov, transform.up) * transform.forward;
		var to = Quaternion.AngleAxis(halfFov, transform.up) * transform.forward;

		Gizmos.DrawLine(transform.position, transform.position + from * range);
		Gizmos.DrawLine(transform.position, transform.position + to * range);
		#if UNITY_EDITOR
		UnityEditor.Handles.DrawWireArc(transform.position, transform.up, from, fov, range);
		#endif
	}
}
