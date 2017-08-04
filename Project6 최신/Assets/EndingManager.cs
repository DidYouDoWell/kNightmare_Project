using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingManager : MonoSingleton<EndingManager>
{

	GameObject _TBGo;
	Animator _PlayerAnim;
	GameObject BGGo;

	private void Awake()
	{
		BGGo = Resources.Load("Prefabs/Map/PF_TITLE_BACK") as GameObject;

	}



	private void Start()
	{
		_TBGo = Instantiate(BGGo);



		//PlayerAnim.SetInteger("State", 1);

		//UI_Tools.Instance.ShowUI(eUIType.PF_UI_ENDING);
	}




	public void LoadEnding()
	{
		//맵은 TITLE에서 만들고 LOBBY에서 GAME으로 갈떄 DESTROY하고, 엔딩에서 다시 만들고 다시 GAME으로 갈때 DESTROY한다.
		//?? 안되는 이유??
		//BGGo = Resources.Load("Prefabs/Map/PF_TITLE_BACK") as GameObject;
		//_TBGo = Instantiate(BGGo);


		_PlayerAnim = GameObject.Find("PF_TITLE_BACK(Clone)").transform.Find("Player(Title)").GetComponentInChildren<Animator>();
		_PlayerAnim.SetInteger("State", 1);
		//	PlayerAnim = GameObject.Find("PF_TITLE_BACK(Clone)").transform.Find("Player(Title)").GetComponentInChildren<Animator>();


		UI_Tools.Instance.ShowUI(eUIType.PF_UI_ENDING);
	}

	public void DisableEnding()
	{
		//Destroy(TBGo);
	//	UI_Tools.Instance.HideUI(eUIType.PF_UI_TITLE);
	}

}
