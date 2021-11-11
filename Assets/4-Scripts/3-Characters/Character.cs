using UnityEngine;

public enum CharacterState
{
	None = -1,
	Idle,
	Move,
	Dodge,
	Attack,
	Dead
}

public class Character : MonoBehaviour
{
	private Animator animator = null;

	public Animator Animator => animator;
	public CharacterState State { get; set; } = CharacterState.None;

	private void Awake()
	{
		animator = GetComponent<Animator>();
	}
}
