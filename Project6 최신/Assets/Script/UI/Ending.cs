using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour
{
	UILabel endLabel;
	float curTime = 0;
	UIButton EndingBtn = null;
	//UIButton ExitBtn = null;
	
	void Start ()
	{
		endLabel = GameObject.Find("EndLabel").GetComponent<UILabel>();
		EndingBtn = GameObject.Find("EndButton").GetComponent<UIButton>();
		//ExitBtn = GameObject.Find("ExitButton").GetComponent<UIButton>();

		//시작 버튼.
		EventDelegate.Add(EndingBtn.onClick,
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
		GameManager.Instance.SelectStage = 1;
		Scene_Manager.Instance.LoadScene(eSceneType.SCENE_LOBBY);
	}

	private void Update()
	{
		curTime += Time.deltaTime;


		endLabel.alpha = Mathf.PingPong(curTime*1.5f, 0.8f);
		endLabel.alpha += 0.2f;
	}


}
