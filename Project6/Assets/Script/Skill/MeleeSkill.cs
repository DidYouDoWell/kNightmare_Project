using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeSkill : BaseSkill
{
    float StackTime = 0;
	Actor player;

    public override void InitSkill()
    {
		//Debug.LogError("");
		player = GameManager.Instance.PlayerActor;

	}
    //시간에 따라 움직임(거리 받을 필요 없음)
    public override void UpdateSkill()
    {
        StackTime += Time.deltaTime;
        if (StackTime >= 0.5f)
            END = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (END == true)
            return;
		if(other.tag =="Enemy")
		{
			GameObject colObject = other.gameObject;
			BaseObject actorObject = colObject.GetComponent<BaseObject>();


			Enemy TargetEnemy = other.gameObject.GetComponent<Enemy>();
			player.ThrowEvent(ConstValue.ActorData_SetTarget, TargetEnemy);

			//스킬이 생성될 때 타켓을 정해주는데, throw이벤트로 타겟을 정해주는 것은 그 후임.
			SkillManager.Instance.makeSkill.TARGET = SkillManager.Instance.makeSkill.OWNER.GetData(ConstValue.ActorData_GetTarget) as BaseObject;


			if (actorObject != TARGET)
				return;


			Destroy(other.gameObject);

			TARGET.ThrowEvent(ConstValue.EventKey_Hit,
				OWNER.GetData(ConstValue.ActorData_Character),
				SKILL_TEMPLATE);


		}

		
    }
}
