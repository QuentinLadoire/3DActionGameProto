using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterNavMovementComponent : MonoBehaviour
{
    private NavMeshAgent agent = null;

	private float speed = 0.0f;

	private void Awake()
	{
		agent = GetComponent<NavMeshAgent>();
	}

	public void Init(float speed)
	{
		this.speed = speed;
	}
	public void Stop()
	{
		agent.isStopped = true;
	}
	public void MoveTo(Vector3 destination)
	{
		agent.speed = speed;
		agent.SetDestination(destination);

		agent.isStopped = false;
	}
}
