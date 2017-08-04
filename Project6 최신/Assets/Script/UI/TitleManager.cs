using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{

	Color DLColor;
	
	GameObject BGGo=null;

	private void Awake()
	{
		if(BGGo==null)
		{
			BGGo = Resources.Load("Prefabs/Map/PF_TITLE_BACK") as GameObject;
			
		}

		


		DLColor = GameObject.Find("Directional Light").GetComponent<Light>().color;
	}


	private void Start()
	{
		Instantiate(BGGo);
		Title2Lobby.mainCameraPos = Camera.main.transform.position;
		Title2Lobby.mainCameraRot = Camera.main.transform.rotation;
		Title2Lobby.DLColor = this.DLColor;

		UI_Tools.Instance.ShowUI(eUIType.PF_UI_TITLE);
	}
	




}
