using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameUI : MonoSingleton<InGameUI>
{
	

	Actor PlayerActor;

	GameObject goInven = null;
	GameObject goCurInven = null;	//현재 플레이어가 가진 아이템 창.
	GameObject goStatusUp = null;
    ComboSkill ComboSystem = null;

	//메인메뉴 버튼.
	UIButton MainMenuBtn = null;
	bool MainMenuBtnOn = false; //메인메뉴 버튼의 on/off 판정. false가 메인메뉴 접힘.
	//GameObject MainMenu = null; //메인메뉴의 게임오브젝트 객체.

	//스테이지 클리어(인벤토리) 이벤트 발생용 버튼. 나중에 스테이지 클리어가 가능해지면 숨기거나 지울 것.
	UIButton CurInventoryBtn = null;

	//스테이지 클리어(인벤토리) 버튼의 on/off 판정. false가 메인메뉴 접힘.
	bool CurInventoryBtnOn = false;

	//타임 스킬 발동시키는 버튼. 스킬 게이지가 다 차면 활성화되고 사용 가능한 상태가 된다.
	UIButton TimeSkillBtn = null;

	////스킬게이지 충전량. ComboSkill 클래스로 이전.
	//float SkillGauge = 0f;	

	//스킬게이지 충전량을 스프라이트의 fill amount로 표시한다.
	UISprite SkillGaugeForeground;	

	//충전 수치 표시.
	UILabel SkillGaugeLabel;

	//스테이터스 표시.
	UILabel DamageLabel;
	UILabel DefenceLabel;

	//타임스킬버튼 콜라이더. 콜라에더에만 enabled를 써서 버튼의 스프라이트나 텍스트에 손대지 않고 기능만 켜고 끈다.
	SphereCollider TimeSkillBtnActivation;
    
    public SphereCollider TIME_SKILL_BTN_ACT
    {
        get { return TimeSkillBtnActivation; }
        set { TimeSkillBtnActivation = value; }
    }

	//GameObject goTimeSkill = null;

	//bool ChargedComplete = false;


	//CharacterStatusData InitialData = new CharacterStatusData();	//적 캐릭터 초기정보 불러오기.
	//GameCharacter CharacterInfo = new GameCharacter();              //적 캐릭터 현재정보 불러오기.

	public void Awake()
	{
		MainMenuBtn = GameObject.Find("MainMenuButton").GetComponent<UIButton>();
		CurInventoryBtn = GameObject.Find("CurInventoryButton").GetComponent<UIButton>();
		TimeSkillBtn = GameObject.Find("TimeSkillButton").GetComponent<UIButton>();

		SkillGaugeForeground = TimeSkillBtn.transform.Find("SkillGaugeForeground").GetComponent<UISprite>();
		if (SkillGaugeForeground == null)
		{
			Debug.LogError(SkillGaugeForeground.ToString() + "이 null입니당");
			return;
		}

		SkillGaugeLabel = TimeSkillBtn.GetComponentInChildren<UILabel>();

		DamageLabel = transform.Find("DamageLabel").GetComponent<UILabel>();
		DefenceLabel = transform.Find("DefenceLabel").GetComponent<UILabel>();


		TimeSkillBtnActivation = TimeSkillBtn.GetComponentInChildren<SphereCollider>();	

		//GameObject goMenu = null;
		 goCurInven = null;

		EventDelegate.Add(MainMenuBtn.onClick, () =>
		{
			//if (goMenu == null) //메인메뉴 중복생성 방지.
			//{
			//	goMenu = Instantiate(Resources.Load("Prefabs/UI/"+ eUIType.PF_UI_MENU.ToString()) as GameObject, Vector3.zero, Quaternion.identity);
			//}

			//메인메뉴 펼쳐짐 -> 접힘.
			if (MainMenuBtnOn)
			{
				//메인메뉴 비활성화.

				//print("MainMenu is folded.");
				UI_Tools.Instance.HideUI(eUIType.PF_UI_MENU);
				MainMenuBtnOn = false;
				Time.timeScale = 1.0f;
			}

			//메인메뉴 접힘 -> 펼쳐짐.
			else
			{
				//버튼 기능 삽입.

				//print("MainMenu is stretched.");
				UI_Tools.Instance.ShowUI(eUIType.PF_UI_MENU);
				MainMenuBtnOn = true;
				Time.timeScale = 0.0f;
			}
		});

		//스테이지 클리어 이벤트 발생용 버튼. 나중에 스테이지 클리어가 가능해지면 주석처리하거나 지울 것.
		EventDelegate.Add(CurInventoryBtn.onClick, () =>
		{
			if (goCurInven == null)    //인벤토리 중복생성 방지.
			{
				
				goCurInven = Instantiate(Resources.Load("Prefabs/UI/"+ eUIType.PF_UI_CURINVENTORY.ToString()) as GameObject, Vector3.zero, Quaternion.identity, transform);
				goCurInven.transform.localScale= Vector3.one;
				goCurInven.transform.localPosition = Vector3.zero;

			}

			//인벤토리 펼쳐짐 -> 접힘.
			if (CurInventoryBtnOn)
			{
				//인벤토리 비활성화.

				//print("Inventory is folded.");
				CurInventoryBtnOn = false;
				NGUITools.SetActive(goCurInven, false);
				Time.timeScale = 1.0f;
			}

			//인벤토리 접힘 -> 펼쳐짐.
			else
			{
				//버튼 기능 삽입.

				//print("Inventory is stretched.");
				CurInventoryBtnOn = true;

				goCurInven.GetComponent<CurInventory>().Init();
				NGUITools.SetActive(goCurInven, true);
				Time.timeScale = 0.0f;
			}

			goCurInven.GetComponent<CurInventory>().Init();
		});

        //타임스킬 버튼.
        EventDelegate.Add(TimeSkillBtn.onClick,
            new EventDelegate(this, "ActiveSkill"));


        //StartCoroutine("SkillGaugeCharge");



    }

	private void Start()
	{
		PlayerActor = GameManager.Instance.PLAYER_ACTOR;
	}

    void ActiveSkill()
    {

        if (ComboSystem.POWER_ON == true)
        {
            Debug.Log("스킬 작동!");
            if (ComboSystem.DRAW_SKILL.step == STEP.IDLE)
            {
                ComboSystem.DRAW_SKILL.next_step = STEP.DRAWED;
            }
            ComboSystem.DRAW_SKILL.IS_DRAW_SKILL = true;
            ComboSystem.SkillTimeCheck = 0.0f;
        }
        else
        {
            return;
        }
    }

	// Update is called once per frame
	void Update()
	{
	
		//공방 표시 코드
		int buffPlus1 = 0;
		int buffPlus2 = 0;

		if(PlayerActor.CUR_BUFF.Contains(eBuffType.ATTACK.ToString()))
		{
			buffPlus1 = (int)(BuffManager.Instance.Get(PlayerActor.CUR_BUFF).STATUS_DATA.GetStatusData(eStatusData.ATTACK));
		}
		else if (PlayerActor.CUR_BUFF.Contains(eBuffType.DEFENCE.ToString()))
		{
			buffPlus1 = (int)(BuffManager.Instance.Get(PlayerActor.CUR_BUFF).STATUS_DATA.GetStatusData(eStatusData.DEFENCE));
		}

		DamageLabel.text = "공격력: " + (PlayerActor.GetStatusData(eStatusData.ATTACK)
			+ SkillManager.Instance.GetSkillTemplate(PlayerActor.SELF_CHARACTER.GetSkillByIndex(0).SKILL_LIST[0]).STATUS_DATA.GetStatusData(eStatusData.ATTACK))+"+" + buffPlus1;

		//방어력 스킬 수정 필요, 변수로?
		DefenceLabel.text = "방어력: " + (PlayerActor.GetStatusData(eStatusData.DEFENCE)
			+ SkillManager.Instance.GetSkillTemplate(PlayerActor.SELF_CHARACTER.GetSkillByIndex(0).SKILL_LIST[0]).STATUS_DATA.GetStatusData(eStatusData.DEFENCE)) + "+" + buffPlus2;


	
        ComboSystem = GameManager.Instance.PlayerActor.GetComponent<Player>().ComboSkillSet;
        if (ComboSystem.COMBO_GAGE < 1)
		{
            //스킬 게이지 충전.
            SkillGaugeLabel.text = "Charging....";
            TimeSkillBtnActivation.enabled = false;
            SkillGaugeForeground.fillAmount = ComboSystem.COMBO_GAGE;

        }
		else if (ComboSystem.COMBO_GAGE >= 1)
		{
			//게이지 충전 중지, 타임스킬 버튼 활성화.
			SkillGaugeLabel.text = "Activated!";
            SkillGaugeForeground.fillAmount = 1.0f;
            TimeSkillBtnActivation.enabled = true ;
			//print("TimeSkill is fully charged.");
		}
	}

	public void showInventory()
	{ 

		if (goInven == null)    //인벤토리 중복생성 방지.
			{

				goInven = Instantiate(Resources.Load("Prefabs/UI/" + eUIType.PF_UI_INVENTORY.ToString()) as GameObject, Vector3.zero, Quaternion.identity, transform);
				goInven.transform.localScale = Vector3.one;
				goInven.transform.localPosition = Vector3.zero;

			}

			////인벤토리 펼쳐짐 -> 접힘.
			//if (InventoryBtnOn)
			//{
			//	//인벤토리 비활성화.

			//	//print("Inventory is folded.");
			//	InventoryBtnOn = false;
			//	NGUITools.SetActive(goInven, false);
			//	Time.timeScale = 1.0f;
			//}

			////인벤토리 접힘 -> 펼쳐짐.
			//else
			//{
			//	//버튼 기능 삽입.

			//	//print("Inventory is stretched.");
			//	InventoryBtnOn = true;
			//	NGUITools.SetActive(goInven, true);
			//	Time.timeScale = 0.0f;
			//}

			goInven.GetComponent<Inventory>().Init();
			NGUITools.SetActive(goInven, true);
			Time.timeScale = 0.0f;

	}

	public void showBasicStatusUp()
	{

		if (goStatusUp == null)    //인벤토리 중복생성 방지.
		{

			goStatusUp = Instantiate(Resources.Load("Prefabs/UI/"+ eUIType.PF_UI_STATUS_UP.ToString()) as GameObject, Vector3.zero, Quaternion.identity, transform);
			goStatusUp.transform.localScale = Vector3.one;
			goStatusUp.transform.localPosition = Vector3.zero;

		}


		goStatusUp.GetComponent<StatusUp>().Init();
		NGUITools.SetActive(goStatusUp, true);
		Time.timeScale = 0.0f;

	}

	//IEnumerator SkillGaugeCharge()	//스킬게이지 충전 코루틴.
	//{

	//       //while (GameManager.Instance.COMBO_GAGE <= 1)
	//       while (GameManager.Instance.POWER_ON)
	//       {
	//           SkillGauge = GameManager.Instance.COMBO_GAGE;    //스킬게이지 충전 코드가 완성되면 그것으로 대체.

	//		SkillGaugeForeground.fillAmount = SkillGauge;
	//           Debug.Log("스킬버튼 퍼센테지 : " + SkillGaugeForeground.fillAmount);
	//		SkillGaugeLabel.text = ((SkillGauge / 1) * 100).ToString("N0") + "%!";

	//		//print((SkillGauge * 100).ToString());
	//	    yield return new WaitForSeconds(0.02f);
	//	}

	//}
}
