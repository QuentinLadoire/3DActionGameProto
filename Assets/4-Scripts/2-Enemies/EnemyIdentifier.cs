using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdentifier : MonoBehaviour
{
	private void Start()
	{
		EnemiesManager.AddEnemyObject(gameObject);
	}
	private void OnDestroy()
	{
		EnemiesManager.RemoveEnemyObject(gameObject);
	}
}
