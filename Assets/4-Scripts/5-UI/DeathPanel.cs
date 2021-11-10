using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPanel : MonoBehaviour
{
    public void OnRestartButtonClick()
	{
		GameManager.RestartGame();
	}
	public void OnQuitButtonClick()
	{
		#if UNITY_EDITOR
			UnityEditor.EditorApplication.ExitPlaymode();
		#else
			Application.Quit();
		#endif
	}
}
