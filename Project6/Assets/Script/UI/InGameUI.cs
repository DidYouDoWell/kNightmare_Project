using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUI : MonoBehaviour
{
	UIButton MainMenuBtn = null;
	bool MainMenuBtnOn = false; //메인베뉴 버튼의 on/off 판정. false가 메인메뉴 접힘.
	GameObject MainMenu = null;	//메인메뉴의 게임오브젝트 객체.
	
	void Start ()
	{
		MainMenuBtn = GameObject.Find("MainMenuButton").GetComponent<UIButton>();
		MainMenu = GameObject.Find("MainMenu(prefab)"); //(prefab)은 나중에 제거.

		EventDelegate.Add(MainMenuBtn.onClick, () =>
		{
			//메인메뉴 접힘.
			if(MainMenuBtnOn)
			{
				//버튼 기능 삽입.
				//print("MainMenu is folded.");
				MainMenu.SetActive(false);	
				MainMenuBtnOn = false;
			}
			//메인메뉴 펼쳐짐.
			else
			{
				//버튼 기능 삽입.
				//print("MainMenu is stretched.");
				MainMenu.SetActive(true);
				MainMenuBtnOn = true;
			}
		}
		);
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	
}
