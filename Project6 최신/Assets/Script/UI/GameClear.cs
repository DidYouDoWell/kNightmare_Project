using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameClear : BaseObject
{
	float curTime = 0;
	UISprite GameClearSprite;
	UILabel GameClearLabel;
	UIButton GameClearButton;

	[SerializeField]
	public float FadeOutTime = 10; 

	// Use this for initialization
	void Start ()
	{
		GameClearSprite = GetComponentInChildren<UISprite>();
		GameClearLabel = GetComponentInChildren<UILabel>();
		GameClearButton = GetComponent<UIButton>();
		GameClearButton.enabled = false;

		EventDelegate.Add(GameClearButton.onClick,
			new EventDelegate(this, "GoEnding"));

		Invoke("GameClearButtonOn", FadeOutTime * 2/3);
	}
	
	// Update is called once per frame
	void Update ()
	{
		curTime += Time.deltaTime;
		if (curTime >= FadeOutTime + (FadeOutTime * 2 / 3))	//curTime을 일정 수치에서 정지시키지 않으면 알파값이 무한대로 상승하고 화면이 이상해지므로 반드시 정지시킬 것.
		{
			curTime = FadeOutTime + (FadeOutTime * 2 / 3);
		}
		GameClearSprite.alpha = curTime / FadeOutTime;
		GameClearLabel.alpha = curTime / FadeOutTime;

	}

	void GameClearButtonOn()
	{
		GameClearButton.enabled = true;
		//print("GameOverButtonOn");
	}

	void GoEnding()
	{
		UI_Tools.Instance.HideUI(eUIType.PF_UI_GAMECLEAR);
		Scene_Manager.Instance.LoadScene(eSceneType.SCENE_ENDING);
	}
}
