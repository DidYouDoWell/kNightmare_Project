using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lobby : MonoBehaviour
{
	UIButton NewGameBtn = null;
	UIButton ContinueBtn = null;
	UIButton TitleBtn = null;

	void Start ()
	{
		NewGameBtn = GameObject.Find("NewGameButton").GetComponent<UIButton>();
		ContinueBtn = GameObject.Find("ContinueButton").GetComponent<UIButton>();
		TitleBtn = GameObject.Find("TitleButton").GetComponent<UIButton>();

		EventDelegate.Add(NewGameBtn.onClick, () =>
		{
			//버튼 기능 삽입.
			SceneManager.LoadScene("Game");
			//print("NewGameButton is OK.");
		}
		);

		EventDelegate.Add(ContinueBtn.onClick, () =>
		{
			//버튼 기능 삽입.
			SceneManager.LoadScene("Game");
			//print("ContinueButton is OK.");
		}
		);

		EventDelegate.Add(TitleBtn.onClick, () =>
		{
			//버튼 기능 삽입.
			SceneManager.LoadScene("Title");
			//print("TitleButton is OK.");
		}
		);
	}

	void Update ()
	{
		
	}
}
