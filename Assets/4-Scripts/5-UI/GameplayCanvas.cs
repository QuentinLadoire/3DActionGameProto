using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayCanvas : MonoBehaviour
{
    [SerializeField] private EndPanel endPanel = null;

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
			endPanel.gameObject.SetActive(false);
		}
		else if (GameManager.GameState == GameState.InDeath)
		{
			endPanel.SetText("GameOver");
			endPanel.gameObject.SetActive(true);
		}
		else if (GameManager.GameState == GameState.InVictory)
		{
			endPanel.SetText("Victory !!!");
			endPanel.gameObject.SetActive(true);
		}
	}
}
