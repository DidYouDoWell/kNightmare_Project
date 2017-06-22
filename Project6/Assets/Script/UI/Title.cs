using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
	UIButton StartBtn = null;
	UIButton ExitBtn = null;
	
	void Start ()
	{
		StartBtn = GameObject.Find("StartButton").GetComponent<UIButton>();
		ExitBtn = GameObject.Find("ExitButton").GetComponent<UIButton>();

		EventDelegate.Add(StartBtn.onClick, () =>
		{
			//버튼 기능 삽입.
			//print("StartButton is OK.");
			SceneManager.LoadScene("Lobby");
		}
		);

		EventDelegate.Add(ExitBtn.onClick, () =>
		{
			//버튼 기능 삽입.
			print("ExitButton is OK.");
			//return;
		}
		);
	}
	
	void Update ()
	{
		
	}
}
