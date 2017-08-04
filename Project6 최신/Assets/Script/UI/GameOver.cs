using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : BaseObject
{
	float curTime = 0;
	UISprite GameOverSprite;
	UILabel GameOverLabel;
	UIButton GameOverButton;

	[SerializeField]
	public float FadeOutTime = 10; 

	// Use this for initialization
	void Start ()
	{
		GameOverSprite = GetComponentInChildren<UISprite>();
		GameOverLabel = GetComponentInChildren<UILabel>();
		GameOverButton = GetComponent<UIButton>();
		GameOverButton.enabled = false;

		EventDelegate.Add(GameOverButton.onClick,
			new EventDelegate(this, "GoLobby"));

		Invoke("GameOverButtonOn", FadeOutTime * 2/3);
	}
	
	// Update is called once per frame
	void Update ()
	{
		curTime += Time.deltaTime;
		if (curTime >= FadeOutTime + (FadeOutTime * 2 / 3))	//curTime을 일정 수치에서 정지시키지 않으면 알파값이 무한대로 상승하고 화면이 이상해지므로 반드시 정지시킬 것.
		{
			curTime = FadeOutTime + (FadeOutTime * 2 / 3);
		}
		GameOverSprite.alpha = curTime / FadeOutTime;
		GameOverLabel.alpha = curTime / FadeOutTime;

	}

	void GameOverButtonOn()
	{
		GameOverButton.enabled = true;
		//print("GameOverButtonOn");
	}

	void GoLobby()
	{
		UI_Tools.Instance.HideUI(eUIType.PF_UI_GAMEOVER);
		Scene_Manager.Instance.LoadScene(eSceneType.SCENE_TITLE);
	}
}
