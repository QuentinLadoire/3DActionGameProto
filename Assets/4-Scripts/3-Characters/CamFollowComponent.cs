using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollowComponent : MonoBehaviour
{
	[SerializeField] private Vector3 idlePosition = Vector3.zero;
	[SerializeField] private Vector3 movePosition = new Vector3(0.0f, 0.0f, 2.0f);

    private Player player = null;

	private void Awake()
	{
		player = GetComponentInParent<Player>();
	}
	private void Update()
	{
		switch (player.State)
		{
			case PlayerState.Idle:
			case PlayerState.Attack:
			case PlayerState.Dead:
				IdlePosition();
				break;

			case PlayerState.Move:
			case PlayerState.Dodge:
				MovePosition();
				break;

			case PlayerState.None:
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
		transform.localPosition = movePosition;
	}
}
