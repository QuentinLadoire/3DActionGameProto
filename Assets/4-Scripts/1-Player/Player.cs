using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayerState
{
	None = -1,
	Idle,
	Move,
	Dodge,
	Attack
}

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerStats stats = null;

    public PlayerStats Stats => stats;
	public PlayerState State { get; set; } = PlayerState.None;
}
