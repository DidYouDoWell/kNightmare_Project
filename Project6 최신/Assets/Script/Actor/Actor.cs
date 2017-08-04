using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : TeamObject
{

	float attackRatio = 1;
	float armorRatio = 1;
	float HPRatio = 1;
	
	protected Dictionary<eSlotType, ItemInstance> equipDic;

	bool isHealing = false;

	float BuffTime = 0f;

    bool IsPlayer = false;
    protected bool IS_PLAYER
    {
        get { return IsPlayer; }
        set { IsPlayer = value; }
    }

	string curBuff = eBuffType.NONE.ToString();
	public string CUR_BUFF
	{
		get { return curBuff; }
		set { curBuff = value; }
	}
	//홍은기 완전 무적상태 추가
    bool bSuperInvulnerable = false;
    public bool IS_INVUL
    {
        get { return bSuperInvulnerable; }
        set { bSuperInvulnerable = value; }
    }

	[SerializeField]
	string TemplateKey = string.Empty;
	public string TEMPLATE_KEY
	{ get { return TemplateKey; } }

	GameCharacter SelfCharacter = null;
	public GameCharacter SELF_CHARACTER
	{ get { return SelfCharacter; } }


	// AI
	//BaseAI ai = null;
	//public BaseAI AI
	//{ get { return ai; } }


	BaseObject TargetObject = null;

	// Board -> HP Bar
	[SerializeField]
	bool bEnableBoard = true;

	protected void Awake()
	{
        //건희
		//GameObject aiObject = new GameObject();
		//aiObject.name = "NormalAI";
		//ai = aiObject.AddComponent<NormalAI>(); // AI가 많아지면 해당 항목을 스위치문으로 세분화해서 작성가능함.
		//aiObject.transform.SetParent(SelfTransform);// AI타겟을 자신으로 설정함.

		//// 없으면 동작  X
		//ai.Target = this;

		GameCharacter gameCharacter =
			CharacterManager.Instance.AddCharacter(TemplateKey);
		gameCharacter.TargetComponenet = this;
		SelfCharacter = gameCharacter;

		//플레이어 HP를 이어서
		if (TEAM_TYPE == eTeamType.TEAM_1 && PlayerPrefs.HasKey("RemainedPlayerHP"))
		{
			
			SelfCharacter.CURRENT_HP = PlayerPrefs.GetFloat("RemainedPlayerHP");
		}

		if(GameManager.Instance.SelectStage!=21)
		{
			//if (TEAM_TYPE != eTeamType.TEAM_1 && TEMPLATE_KEY != "ENEMY_BOSS" && TEMPLATE_KEY != "ENEMY_MINI_BOSS") //0714 추가. 
				if (TEAM_TYPE != eTeamType.TEAM_1 ) //0714 추가. 
				{
				//for (int i = 0; i < (int)eStatusData.MAX; i++)
				//{
				StageInfo stageinfo = null;
				StageManager.Instance.DIC_STAGEINFO.TryGetValue(GameManager.Instance.SelectStage, out stageinfo);

			//StatusData levelBalance.SetData = SelfCharacter.CHARACTER_TEMPLATE.STATUS; 
			// 캐릭터의 템플릿을 건들면 다음 같은 종류의 캐릭터의 스탯이 계속 중첩되어 바뀌기 때문. -> 포인트접근?

			StatusData levelBalance = new StatusData();
			
			levelBalance.SetData(eStatusData.ATTACK, SelfCharacter.CHARACTER_STATUS.GetStatusData(eStatusData.ATTACK) * (stageinfo.ATTACK_SCALE));
			levelBalance.SetData(eStatusData.DEFENCE, SelfCharacter.CHARACTER_STATUS.GetStatusData(eStatusData.DEFENCE) * (stageinfo.ARMOR_SCALE));
			levelBalance.SetData(eStatusData.MAX_HP, SelfCharacter.CHARACTER_STATUS.GetStatusData(eStatusData.MAX_HP) * (stageinfo.HP_SCALE));

			//기존 초기의 key의 스텟을 모두 지운 후에, 다시 추가된 스텟을 ADD한다.
			SelfCharacter.CHARACTER_STATUS.RemoveStatusData(ConstValue.CharacterStatusDataKey);

			SelfCharacter.CHARACTER_STATUS.AddStatusData("LEVEL_BALANCE", levelBalance);

			SelfCharacter.CURRENT_HP = SelfCharacter.CHARACTER_STATUS.GetStatusData(eStatusData.MAX_HP);
			
			//SelfCharacter.CHARACTER_TEMPLATE.STATUS.SetData(eStatusData.MAX_HP, SelfCharacter.CHARACTER_TEMPLATE.STATUS.GetStatusData(eStatusData.MAX_HP) * (stageinfo.HP_SCALE));
			//SelfCharacter.CHARACTER_TEMPLATE.STATUS.SetData(eStatusData.ATTACK, SelfCharacter.CHARACTER_TEMPLATE.STATUS.GetStatusData(eStatusData.ATTACK) * (stageinfo.ATTACK_SCALE));
			//SelfCharacter.CHARACTER_TEMPLATE.STATUS.SetData(eStatusData.DEFENCE, SelfCharacter.CHARACTER_TEMPLATE.STATUS.GetStatusData(eStatusData.DEFENCE) * (stageinfo.ARMOR_SCALE));


			//BaseBoard board = BoardManager.Instance.GetBoardData(this,
			//eBoardType.BOARD_HP);
			//if (board != null)
			//	board.SetData(ConstValue.SetData_HP,
			//		GetStatusData(eStatusData.MAX_HP),
			//		SelfCharacter.CURRENT_HP);

				//board = null;
				//}
				}
		}
		//스테이지마다 적들의 밸런스 조절
		

		for (int i = 0;
			i < gameCharacter.CHARACTER_TEMPLATE.LIST_SKILL.Count;
			i++)
		{
			SkillData data =
				SkillManager.Instance.GetSkillData(
				gameCharacter.CHARACTER_TEMPLATE.LIST_SKILL[i]);

			if (data == null)
			{
				Debug.LogError(
					gameCharacter.CHARACTER_TEMPLATE.LIST_SKILL[i]
					+ " 스킬 키를 찾을수 없습니다.");
			}
			else
				gameCharacter.AddSkill(data);
		}


		if (bEnableBoard)	//UI(board)
		{
			BaseBoard board = BoardManager.Instance.AddBoard(
				this, eBoardType.BOARD_HP);
			BaseBoard statusBoard = BoardManager.Instance.AddBoard(
				this, eBoardType.BOARD_STATUS);

			if (IsPlayer == true)
				board.IS_PLAYER = true;

			board.SetData(ConstValue.SetData_HP,
				GetStatusData(eStatusData.MAX_HP),
				SelfCharacter.CURRENT_HP);
			statusBoard.SetData(ConstValue.SetData_Status,
				eStateType.STATE_NONE);
		}

		ActorManager.Instance.AddActor(this);//Actor가 생성된 이후에 처리
	}

	public double GetStatusData(eStatusData statusData)
	{
		return SelfCharacter.CHARACTER_STATUS.GetStatusData(statusData);
	}
    //대문자 오브젝트 클래스의 상위객체
    //소문자 오브젝트 자료형태(int형 float형 등등....)

    //파람즈 - 매개변수들(갯수제한, 형태제한 없음.) 불안전함. 최대한 간단한 데이터만. 규칙을 정리해야함.
    public override object GetData(string keyData, params object[] datas)
	{
		if (keyData == ConstValue.ActorData_Team)
			return TEAM_TYPE;
		else if (keyData == ConstValue.ActorData_Character)
			return SelfCharacter;
		else if (keyData == ConstValue.ActorData_GetTarget)
			return TargetObject;
		else if (keyData == ConstValue.ActorData_GetThisActor)
			return this;
		else if(keyData == ConstValue.ActorData_SkillData)
		{
			int index = (int)datas[0];
			return SelfCharacter.GetSkillByIndex(index);
		}


		// Base 부모클래스 -> BaseObject
		return base.GetData(keyData, datas);
	}
    //메소드가 사용되어야 하는 상황을 구분짓기 위해 두개를 나눔.
	public override void ThrowEvent(string keyData, params object[] datas)
	{
		if(keyData == ConstValue.EventKey_Hit)
		{
			if (OBJECT_STATE == eBaseObjectState.STATE_DIE)
				return;

			// 공격 주체의 케릭터
			GameCharacter casterCharacter
				= datas[0] as GameCharacter;
			SkillTemplate skillTemplate =
				datas[1] as SkillTemplate;

			//0625 교준, 현재의 buff를 받기 위함.
			string curBuff = datas[2]as string;

			//0626 교준, 여기에 공격 방향도 받아야 할듯? -> 피의 방향을 위해
			//Vector3 damagedPos = (Vector3)datas[3];
			Quaternion attackDir = (Quaternion)datas[3];

			if (curBuff != eBuffType.NONE.ToString())
			{
				BuffTemplateData buffEffect = BuffManager.Instance.Get(curBuff);
				casterCharacter.CHARACTER_STATUS.AddStatusData("BUFF", buffEffect.STATUS_DATA);
			}

		
			

			casterCharacter.CHARACTER_STATUS.AddStatusData("SKILL",
				skillTemplate.STATUS_DATA);
            //데미지 수치를 랜덤값으로 만들면 더 좋다. 규칙을 정한것이지 구조가 아님. 다른곳에서도 처리 가능함.
			double attackDamage = casterCharacter.CHARACTER_STATUS.GetStatusData(eStatusData.ATTACK) - SelfCharacter.CHARACTER_STATUS.GetStatusData(eStatusData.DEFENCE);

			if (attackDamage < 0)
				attackDamage = 0;

			// SelfCharacter.CHARACTER_STATUS.GetStatusData(eStatusData.DEFFENCE);

			

			casterCharacter.CHARACTER_STATUS.RemoveStatusData("SKILL");


			if (curBuff != eBuffType.NONE.ToString())
			{
				casterCharacter.CHARACTER_STATUS.RemoveStatusData("BUFF");
			}
			//은기 완전 무적상태 추가
            if (IS_INVUL == false)
            {
                //은기 6.29 콤보게이지 관련 추가(양 조절)
                if (GameManager.Instance.PLAYER_ACTOR.GetComponent<Player>().ComboSkillSet.COMBO_GAGE < 1)
                {
                    GameManager.Instance.PLAYER_ACTOR.GetComponent<Player>().ComboSkillSet.COMBO_GAGE += 0.1f;
                    GameManager.Instance.PLAYER_ACTOR.GetComponent<Player>().ComboSkillSet.POWER_DOWN_STATE = false;
                }

                SelfCharacter.IncreaseCurrentHP(-attackDamage);
                Vector3 damagedPos = gameObject.transform.localPosition;

                if (attackDir != null)
                    BloodManager.Instance.CreateBloodParticle(gameObject, damagedPos, attackDir);
                else
                    BloodManager.Instance.CreateBloodParticle(gameObject, damagedPos);
            }
			//재인이형
			// HPBoard
			BaseBoard board = BoardManager.Instance.GetBoardData(this,
				eBoardType.BOARD_HP);
			if (board != null)
				board.SetData(ConstValue.SetData_HP,
					GetStatusData(eStatusData.MAX_HP),
					SelfCharacter.CURRENT_HP);

			//재인이형
			//// Board 초기화
			board = null;

			//// DamageBoard 
			//board = BoardManager.Instance.AddBoard(this, eBoardType.BOARD_DAMAGE);
			//if (board != null)
			//	board.SetData(ConstValue.SetData_Damage, attackDamage);

			//건희
			//// 피격 에니메이션
			//AI.ANIMATOR.SetInteger("Hit", 1);
		}
		else if(keyData == ConstValue.EventKey_SelectSkill)
		{
			int index = (int)datas[0];
			SkillData data = SelfCharacter.GetSkillByIndex(index);
			SelfCharacter.SELECT_SKILL = data;
		}
		else if(keyData == ConstValue.ActorData_SetTarget)
		{
			TargetObject = datas[0] as BaseObject;
		}
		else if (keyData == ConstValue.EventKey_SelectState)
		{
			if (this.tag != "Player")
			{
				BaseBoard board = BoardManager.Instance.GetBoardData(this,
					eBoardType.BOARD_STATUS);
				if (board != null)
					board.SetData(ConstValue.SetData_Status,
						(eStateType)datas[0],
						(BaseObject)datas[1]);
			}
		}
		else
			base.ThrowEvent(keyData, datas);
	}

	public virtual void equipItemStatus()
	{

	}

	public virtual void basicStatusUp(eStatusData type)
	{

	}

	protected virtual void Update()
	{

		//0625 교준, 현재 버프상태가 무엇인지 체크.
		if (curBuff != eBuffType.NONE.ToString())
		{
			//0702 교준, 힐링버프 추가
			if (curBuff.Contains(eBuffType.HEAL.ToString())&& isHealing==false)	//
			{
				isHealing = true;
				StartCoroutine("Healing");
			}


			BuffTime += Time.deltaTime;
			BuffTemplateData buffEffect = BuffManager.Instance.Get(curBuff);

			if(BuffTime>=buffEffect.TIME)
			{
				BuffTime = 0f;
				curBuff = eBuffType.NONE.ToString();
			}
			
		}




		//BaseBoard board = BoardManager.Instance.GetBoardData(this,
		//eBoardType.BOARD_HP);
		//if (board != null)
		//	board.SetData(ConstValue.SetData_HP,
		//		GetStatusData(eStatusData.MAX_HP),
		//		SelfCharacter.CURRENT_HP);

		////// Board 초기화
		//board = null;

		//건희
		//AI.UpdateAI();
		//if(AI.END)
		//{
		//	Destroy(SelfObject);
		//}
	}

	IEnumerator Healing()
	{
		while(curBuff.Contains(eBuffType.HEAL.ToString()))
		{
			BuffTemplateData buffEffect = BuffManager.Instance.Get(curBuff);
			int muchOfHeal = (int)(buffEffect.HEAL*0.01* SelfCharacter.CHARACTER_STATUS.GetStatusData(eStatusData.MAX_HP));
			SelfCharacter.IncreaseCurrentHP(muchOfHeal);


			BaseBoard board = BoardManager.Instance.GetBoardData(this,eBoardType.BOARD_HP);
			if (board != null)
				board.SetData(ConstValue.SetData_HP,
					GetStatusData(eStatusData.MAX_HP),
					SelfCharacter.CURRENT_HP);

			//// Board 초기화
			board = null;

			yield return new WaitForSeconds(1f);
		}
		
			isHealing = false;
			
	}

    //스킬 템플릿이라는 퍼즐조각이 있고 그걸 조합해서 만든게 스킬 데이터
	public void RunSkill()
	{
		SkillData selectSkill = SelfCharacter.SELECT_SKILL;
		if (selectSkill == null)
			return;
		//코루틴을 넣거나 딜레이를 잘 섞어서 스킬을 만듬.
		// 은기
		//for (int i = 0; i < selectSkill.SKILL_LIST.Count; i++)
		//{
		//	SkillManager.Instance.RunSkill(this,
		//		selectSkill.SKILL_LIST[i]);
		//}
		//건희 스킬딜레이
		StopCoroutine("SkillDelay");
		StartCoroutine("SkillDelay", selectSkill);
	}
	IEnumerator SkillDelay(SkillData skillData)
	{
		for (int i = 0; i < skillData.SKILL_LIST.Count; i++)
		{
			SkillManager.Instance.RunSkill(this,
				skillData.SKILL_LIST[i]);
			yield return new WaitForSeconds(skillData.DELAY);
			
		}
	}

	private void OnEnable()
	{
		// 재인이형
		if (BoardManager.Instance != null)
			BoardManager.Instance.ShowBoard(this, true);
	}

	public void OnDisable()
	{
		// 재인이형
		if (BoardManager.Instance != null
				  && GameManager.Instance.GAME_OVER == false)
			BoardManager.Instance.ShowBoard(this, false);
	}
    //플레이어를 찾아보고 있을때 클리어함.
    public void OnDestroy()
	{
		SelfCharacter.CHARACTER_STATUS.AddStatusData(ConstValue.CharacterStatusDataKey, SelfCharacter.CHARACTER_TEMPLATE.STATUS);
		SelfCharacter.CHARACTER_STATUS.RemoveStatusData("LEVEL_BALANCE");
		
		// 재인이형
		if (BoardManager.Instance != null)
			BoardManager.Instance.ClearBoard(this);

        if (ActorManager.Instance != null)
		{
			ActorManager.Instance.RemoveActor(this);
		}
	}

}
