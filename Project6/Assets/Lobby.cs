using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lobby : MonoBehaviour
{
	UIButton NewGameBtn = null;
	UIButton ContinueBtn = null;
	UIButton TitleBtn = null;

	void Start()
	{
		//Transform trans = Find("NewGameBtn");
		//if (trans == null)
		//{
		//	Debug.LogError("NewGameBtn is Not Found");
		//	return;
		//}
		NewGameBtn = GameObject.Find("NewGameButton").GetComponent<UIButton>();
		ContinueBtn = GameObject.Find("ContinueButton").GetComponent<UIButton>();
		TitleBtn = GameObject.Find("TitleButton").GetComponent<UIButton>();

		EventDelegate.Add(NewGameBtn.onClick, () =>
		{
			print("NewGameButton is OK.");
			SceneManager.LoadScene("Game");
		}
			);

		EventDelegate.Add(ContinueBtn.onClick, () =>
		{
			print("ContinueButton is OK.");
		}
			);

		EventDelegate.Add(TitleBtn.onClick, () =>
		{
			print("TitleButton is OK.");
			SceneManager.LoadScene("Title");
		}
			);
	}
}
