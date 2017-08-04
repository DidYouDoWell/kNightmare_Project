using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
	UILabel tabLabel;
	float curTime = 0;
	UIButton StartBtn = null;
	//UIButton ExitBtn = null;
	
	void Start ()
	{
		tabLabel = GameObject.Find("TabLabel").GetComponent<UILabel>();
		StartBtn = GameObject.Find("StartButton").GetComponent<UIButton>();
		//ExitBtn = GameObject.Find("ExitButton").GetComponent<UIButton>();

		//시작 버튼.
		EventDelegate.Add(StartBtn.onClick,
			new EventDelegate(this, "ShowLobby"));

		////종료 버튼.
		//EventDelegate.Add(ExitBtn.onClick, () =>
		//{
		//	Application.Quit();
		//}
		//);
	}

	void ShowLobby()
	{
		Scene_Manager.Instance.LoadScene(eSceneType.SCENE_LOBBY);
	}

	private void Update()
	{
		curTime += Time.deltaTime;

	
		tabLabel.alpha = Mathf.PingPong(curTime*1.5f, 0.8f);
		tabLabel.alpha += 0.2f;
	}


}
