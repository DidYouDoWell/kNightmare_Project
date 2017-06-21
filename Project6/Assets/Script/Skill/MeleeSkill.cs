using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeSkill : BaseSkill
{
    float StackTime = 0;

    public override void InitSkill()
    {

    }
    //시간에 따라 움직임(거리 받을 필요 없음)
    public override void UpdateSkill()
    {
        StackTime += Time.deltaTime;
        if (StackTime >= 0.1f)
            END = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (END == true)
            return;

        GameObject colObject = other.gameObject;
        BaseObject actorObject = colObject.GetComponent<BaseObject>();

        if (actorObject != TARGET)
            return;


        TARGET.ThrowEvent(ConstValue.EventKey_Hit,
            OWNER.GetData(ConstValue.ActorData_Character),
            SKILL_TEMPLATE);
    }
}
