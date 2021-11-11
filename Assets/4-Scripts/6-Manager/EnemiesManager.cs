using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
	private List<GameObject> enemyObjects = new List<GameObject>();

	private void Awake()
	{
		instance = this;
	}

    private static EnemiesManager instance = null;

	public static int EnemyCount => instance.enemyObjects.Count;

	public static void AddEnemyObject(GameObject enemyObject)
	{
		instance.enemyObjects.Add(enemyObject);
	}
	public static void RemoveEnemyObject(GameObject enemyObject)
	{
		instance.enemyObjects.Remove(enemyObject);
	}
}
