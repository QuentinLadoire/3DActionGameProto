using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerStats stats = null;

    public PlayerStats Stats => stats;
}
