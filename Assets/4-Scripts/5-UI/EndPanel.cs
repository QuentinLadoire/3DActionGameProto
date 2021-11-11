using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndPanel : MonoBehaviour
{
	[SerializeField] private Text titleText = null;

	public void SetText(string title)
	{
		titleText.text = title;
	}

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
