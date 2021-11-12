using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCameraFollowComponent : MonoBehaviour
{
	[SerializeField] private Vector3 idlePosition = Vector3.zero;
	[SerializeField] private Vector3 movePosition = new Vector3(0.0f, 0.0f, 2.0f);

    private Character character = null;

	private void Awake()
	{
		character = GetComponentInParent<Character>();
	}
	private void Update()
	{
		switch (character.State)
		{
			case CharacterState.Idle:
			case CharacterState.Attack:
			case CharacterState.Dead:
				IdlePosition();
				break;

			case CharacterState.Move:
			case CharacterState.Dodge:
				MovePosition();
				break;

			case CharacterState.None:
			default:
				DefaultPosition();
				break;
		}
	}

	private void DefaultPosition()
	{
		transform.position = Vector3.zero;
	}
	private void IdlePosition()
	{
		transform.localPosition = idlePosition;
	}
	private void MovePosition()
	{
		var origin = character.transform.position + character.transform.up * 0.1f;
		var direction = transform.TransformVector(movePosition);
		if (Physics.Raycast(origin, direction, out RaycastHit hit, direction.magnitude))
		{
			var point = hit.point;
			point.y = 0.0f;

			direction = point - character.transform.position;
		}

		transform.localPosition = transform.InverseTransformVector(direction);
	}
}
