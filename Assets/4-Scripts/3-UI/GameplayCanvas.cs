using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayCanvas : MonoBehaviour
{
    [SerializeField] private GameObject deathPanel = null;

	private void Start()
	{
		GameManager.GameStateChangeCallback += GameStateChangeCallback;
	}
	private void OnDestroy()
	{
		GameManager.GameStateChangeCallback -= GameStateChangeCallback;
	}

	private void GameStateChangeCallback()
	{
		if (GameManager.GameState == GameState.InGame)
		{
			deathPanel.SetActive(false);
		}
		else if (GameManager.GameState == GameState.InDeath)
		{
			deathPanel.SetActive(true);
		}
	}
}
