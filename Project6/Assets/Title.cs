using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
	UIButton StartBtn;
	UIButton ExitBtn;

	void Start()
	{
		//Transform temp = transform.Find("StartButton");
		//if (temp == null)   //예외처리 방법 찾기.
		//{
		//	Debug.LogError("StartBtn is not found.");
		//}
		StartBtn = GameObject.Find("StartButton").GetComponent<UIButton>();

		ExitBtn = GameObject.Find("ExitButton").GetComponent<UIButton>();

		EventDelegate.Add(StartBtn.onClick, () =>
		{
			print("StartButton is OK.");
			SceneManager.LoadScene("Lobby");
		}
			);

		EventDelegate.Add(ExitBtn.onClick, () =>
		{
			print("ExitButton is OK.");
			//Application.Quit();
			return;
		}
			);
	}
}
