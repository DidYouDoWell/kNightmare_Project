using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyManager : MonoSingleton<LobbyManager> {

	public GameObject TBGo;
	public Animator PlayerAnim;

	private void Awake()
	{


		


	}



	public void LoadLobby()
	{
		PlayerAnim = GameObject.Find("PF_TITLE_BACK(Clone)").transform.Find("Player(Title)").GetComponentInChildren<Animator>();

		PlayerAnim.SetInteger("State", 0);
		UI_Tools.Instance.ShowUI(eUIType.PF_UI_LOBBY);
	}

	public void DisableLobby()
	{
		TBGo = GameObject.Find("PF_TITLE_BACK(Clone)");
		Destroy(TBGo);
		UI_Tools.Instance.HideUI(eUIType.PF_UI_LOBBY);
	}
}
