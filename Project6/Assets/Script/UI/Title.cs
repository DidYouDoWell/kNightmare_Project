using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour
{
	UIButton StartBtn;
	UIButton ExitBtn;
	
	void Start ()
	{
		StartBtn = GameObject.Find("StartButton").GetComponent<UIButton>();
		ExitBtn = GameObject.Find("ExitButton").GetComponent<UIButton>();

		EventDelegate.Add(StartBtn.onClick, () =>
		{
			print("StartButton is OK.");
			//LoadScene("SCENE_LOBBY");
		}
			);

		EventDelegate.Add(ExitBtn.onClick, () =>
		{
			print("ExitButton is OK.");
			//return;
		}
			);
	}
	
	void Update ()
	{
		
	}
}
