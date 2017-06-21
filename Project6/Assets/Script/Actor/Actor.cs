using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : BaseObject
{
    bool IsPlayer = false;
    public bool IS_PLAYER
    {
        get { return IsPlayer; }
        set { IsPlayer = value; }
    }

	[SerializeField]
	eTeamType TeamType;
	public eTeamType TEAM_TYPE
	{
		get
		{
			return TeamType ;
		}
	}

	[SerializeField]
	string TemplateKey = string.Empty;

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

	private void Awake()
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

		//for(int i = 0; 
		//	i< gameCharacter.CHARACTER_TEMPLATE.LIST_SKILL.Count;
		//	i++)
		//{
		//	SkillData data =
		//		SkillManager.Instance.GetSkillData(
		//		gameCharacter.CHARACTER_TEMPLATE.LIST_SKILL[i]);

		//	if (data == null)
		//	{
		//		Debug.LogError(
		//			gameCharacter.CHARACTER_TEMPLATE.LIST_SKILL[i]
		//			+ " 스킬 키를 찾을수 없습니다.");
		//	}
		//	else
		//		gameCharacter.AddSkill(data);
		//}

        //재인이형
		//if(bEnableBoard)//UI(boar)
		//{
		//	BaseBoard board = BoardManager.Instance.AddBoard(
		//		this, eBoardType.BOARD_HP);

		//	board.SetData(ConstValue.SetData_HP,
		//		GetStatusData(eStatusData.MAX_HP),
		//		SelfCharacter.CURRENT_HP);
		//}

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
			return TeamType;
		else if (keyData == ConstValue.ActorData_Character)
			return SelfCharacter;
		else if (keyData == ConstValue.ActorData_GetTarget)
			return TargetObject;
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

			casterCharacter.CHARACTER_STATUS.AddStatusData("SKILL",
				skillTemplate.STATUS_DATA);
            //데미지 수치를 랜덤값으로 만들면 더 좋다. 규칙을 정한것이지 구조가 아님. 다른곳에서도 처리 가능함.
			double attackDamage =
				casterCharacter
				.CHARACTER_STATUS
				.GetStatusData(eStatusData.ATTACK);
			// SelfCharacter.CHARACTER_STATUS.GetStatusData(eStatusData.DEFFENCE);

			casterCharacter.CHARACTER_STATUS.RemoveStatusData("SKILL");

			SelfCharacter.IncreaseCurrentHP(-attackDamage);

            //재인이형
			//// HPBoard
			//BaseBoard board = BoardManager.Instance.GetBoardData(this,
			//	eBoardType.BOARD_HP);
			//if (board != null)
			//	board.SetData(ConstValue.SetData_HP,
			//		GetStatusData(eStatusData.MAX_HP),
			//		SelfCharacter.CURRENT_HP);

            //재인이형
			//// Board 초기화
			//board = null;

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
		else
			base.ThrowEvent(keyData, datas);
	}

	protected virtual void Update()
	{
        //건희
		//AI.UpdateAI();
		//if(AI.END)
		//{
		//	Destroy(SelfObject);
		//}
	}
    //스킬 템플릿이라는 퍼즐조각이 있고 그걸 조합해서 만든게 스킬 데이터
	public void RunSkill()
	{
		SkillData selectSkill = SelfCharacter.SELECT_SKILL;
		if (selectSkill == null)
			return;
        //코루틴을 넣거나 딜레이를 잘 섞어서 스킬을 만듬.
        // 은기
		//for(int i = 0; i < selectSkill.SKILL_LIST.Count;i++)
		//{
		//	SkillManager.Instance.RunSkill(this, 
		//		selectSkill.SKILL_LIST[i]);
		//}
	}

	private void OnEnable()
	{
        // 재인이형
		//if (BoardManager.Instance != null)
		//	BoardManager.Instance.ShowBoard(this, true);
	}

	public void OnDisable()
	{
        // 재인이형
        //if (BoardManager.Instance != null
        //          && GameManager.Instance.GAME_OVER == false)
        //	BoardManager.Instance.ShowBoard(this, false);
    }
    //플레이어를 찾아보고 있을때 클리어함.
    public void OnDestroy()
	{
        // 재인이형
        //if (BoardManager.Instance != null)
        //	BoardManager.Instance.ClearBoard(this);

        if (ActorManager.Instance != null)
		{
			//ActorManager.Instance.RemoveActor(this);
		}
	}

}
