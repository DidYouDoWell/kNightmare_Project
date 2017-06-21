using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lobby : MonoBehaviour
{
	UIButton NewGameBtn;
	UIButton ContinueBtn;
	UIButton TitleBtn;

	void Start ()
	{
		NewGameBtn = GameObject.Find("NewGameButton").GetComponent<UIButton>();
		ContinueBtn = GameObject.Find("ContinueButton").GetComponent<UIButton>();
		TitleBtn = GameObject.Find("TitleButton").GetComponent<UIButton>();

		EventDelegate.Add(NewGameBtn.onClick, () =>
		{
			print("NewGameButton is OK.");
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
		}
			);
	}

	void Update ()
	{
		
	}
}
