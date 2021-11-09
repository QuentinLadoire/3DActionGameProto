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

	private Animator animator = null;

    public PlayerStats Stats => stats;
	public Animator Animator => animator;
	public PlayerState State { get; set; } = PlayerState.None;

	private void Awake()
	{
		animator = GetComponent<Animator>();
	}
}
