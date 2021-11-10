using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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

    public static Player Player => instance.player;

    private void Awake()
	{
        instance = this;
	}
	private void Update()
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
}
