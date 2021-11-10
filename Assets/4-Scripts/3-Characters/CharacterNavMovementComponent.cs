using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterNavMovementComponent : MonoBehaviour
{
    private NavMeshAgent agent = null;

	private Character character = null;

	private void Awake()
	{
		agent = GetComponent<NavMeshAgent>();

		character = GetComponent<Character>();
	}
	private void Start()
	{
		agent.speed = character.Stats.MovementSpeed;
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
