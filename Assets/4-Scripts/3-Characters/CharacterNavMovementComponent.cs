using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterNavMovementComponent : MonoBehaviour
{
    private NavMeshAgent agent = null;

	private void Awake()
	{
		agent = GetComponent<NavMeshAgent>();
	}

	public void Init(float speed)
	{
		agent.speed = speed;
	}
	public void Stop()
	{
		agent.isStopped = true;
	}
	public void MoveTo(Vector3 destination)
	{
		agent.SetDestination(destination);

		agent.isStopped = false;
	}
}
