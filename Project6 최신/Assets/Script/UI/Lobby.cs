using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lobby : MonoBehaviour
{
	float curTime = 0;
	UIButton NewGameBtn = null;
	UIButton ContinueBtn = null;
	UIButton QuitBtn = null;
	//GameObject TBGo;

	public Vector3 goalPos = new Vector3(-7f,11.4f,-15.6f);
	Quaternion goalRot;

	private void Awake()
	{
		Light DL;
		DL = GameObject.Find("Directional Light").GetComponent<Light>();

		//TBGo = LobbyManager.Instance.TBGo;



		//goalPos = Camera.main.transform.position;
		Camera.main.transform.rotation = Title2Lobby.mainCameraRot;
		DL.color = Title2Lobby.DLColor;

		NewGameBtn = GameObject.Find("NewGameButton").GetComponent<UIButton>();
		ContinueBtn = GameObject.Find("ContinueButton").GetComponent<UIButton>();
		QuitBtn = GameObject.Find("QuitButton").GetComponent<UIButton>();

		//뉴 게임 버튼. 말 그대로 새 게임 시작. 이전 저장파일들은 초기화(?)
		EventDelegate.Add(NewGameBtn.onClick, 
			new EventDelegate(this, "StartNewGame"));

		//이어하기 버튼. 이전 저장파일을 불러온다. // 차후 구현
		EventDelegate.Add(ContinueBtn.onClick,
			new EventDelegate(this, "StartLoadGame"));

		//종료 버튼. 어플리케이션을 종료한다. // 07.07 12:35 건희수정 (타이틀로 굳이 가서 종료하는것 보단 여기서 종료하는게 좋아보임)
		EventDelegate.Add(QuitBtn.onClick, () =>
		{
			Application.Quit();
		}
		);
	}

	private void Update()
	{
		curTime += Time.deltaTime;

		if(curTime<1)
		Camera.main.transform.position = Vector3.Lerp (Title2Lobby.mainCameraPos,goalPos, curTime);
		
	}



	void StartNewGame()
	{
		//Destroy(TBGo);

		//TBGo.SetActive(false);

		Scene_Manager.Instance.LoadScene(eSceneType.SCENE_GAME);
		StartInfo.startType = eStartInfo.NEW_GAME;


       // PlayerPrefs.DeleteAll();


        if (PlayerPrefs.HasKey(ConstValue.LocalSave_ItemInstance))
		{
            ItemManager.Instance.DIC_EQUIP.Clear();
			PlayerPrefs.DeleteKey(ConstValue.LocalSave_ItemInstance);   // 게임 시작때 지정한 키값과 데이터값을 제거한다.

		
		}

		if(PlayerPrefs.HasKey("Plus" + eStatusData.ATTACK.ToString()))
			PlayerPrefs.DeleteKey("Plus" + eStatusData.ATTACK.ToString());

		if (PlayerPrefs.HasKey("Plus" + eStatusData.DEFENCE.ToString()))
			PlayerPrefs.DeleteKey("Plus" + eStatusData.DEFENCE.ToString());

		if (PlayerPrefs.HasKey("Plus" + eStatusData.MAX_HP.ToString()))
			PlayerPrefs.DeleteKey("Plus" + eStatusData.MAX_HP.ToString());

		PlayerPrefs.DeleteKey("RemainedPlayerHP");
	}

	void StartLoadGame()
	{
		//TBGo.SetActive(false);

		Scene_Manager.Instance.LoadScene(eSceneType.SCENE_GAME);
		StartInfo.startType = eStartInfo.LOAD_GAME;

		
	}
}
