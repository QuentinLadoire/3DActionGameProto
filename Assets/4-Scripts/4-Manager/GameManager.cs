using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public enum GameState
{
    None = -1,
    InMenu,
    InGame,
    InDeath
}

public class GameManager : MonoBehaviour
{
    [SerializeField] private Player player = null;
	[SerializeField] private GameState gameState = GameState.None;

	private UnityAction gameStateChangeCallback = () => { /*Debug.Log("GameStateChangeCallback);*/ };

    private void Awake()
	{
        instance = this;
	}
	private void Start()
	{
		gameState = GameState.InGame;
		gameStateChangeCallback.Invoke();
	}
	private void Update()
	{
		InGameUpdate();

		Debug();
	}

	private void InGameUpdate()
	{
		if (gameState != GameState.InGame) return;

		if (player.State == PlayerState.Dead)
		{
			gameState = GameState.InDeath;

			gameStateChangeCallback.Invoke();
		}
	}

	private void Debug()
	{
		//Debug player health
		if (Keyboard.current.numpadPlusKey.wasPressedThisFrame)
		{
			player.HealthComponent.TakeHeal(1);
		}
		else if (Keyboard.current.numpadMinusKey.wasPressedThisFrame)
		{
			player.HealthComponent.TakeDamege(1);
		}
	}


	private static GameManager instance = null;

	public static Player Player => instance.player;
	public static GameState GameState => instance.gameState;
	public static UnityAction GameStateChangeCallback { get => instance.gameStateChangeCallback; set => instance.gameStateChangeCallback = value; }

	public static void RestartGame()
	{
		SceneManager.LoadScene("Gameplay");
	}
}