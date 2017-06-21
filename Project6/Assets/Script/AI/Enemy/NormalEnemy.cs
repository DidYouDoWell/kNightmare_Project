using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemy : BaseAI {

	protected override IEnumerator Idle()
	{
		// 근거리 적 탐색
		BaseObject targetObject = ActorManager.Instance.GetSearchEnemy(Target);

		if (targetObject != null)
		{
			SkillData sData =
				Target.GetData(ConstValue.ActorData_SkillData, 0) as SkillData;
			float attackRange = sData.RANGE;

			float distance = Vector2.Distance(
				targetObject.SelfTransform.position,
				SelfTransform.position);

			if (distance < attackRange)
			{
				Stop();
				AddNextAI(eStateType.STATE_ATTACK, targetObject);
			}
			else
			{
				AddNextAI(eStateType.STATE_WALK);
			}
		}

		yield return StartCoroutine(base.Idle());
	}

	protected override IEnumerator Move()
	{
		BaseObject targetObject =
			ActorManager.Instance.GetSearchEnemy(Target);

		if (targetObject != null)
		{
			SkillData sData =
				Target.GetData(ConstValue.ActorData_SkillData, 0) as SkillData;
			float attackrange = 1f;

			if (sData != null)
				attackrange = sData.RANGE;

			float distance = Vector3.Distance(
				targetObject.SelfTransform.position,
				SelfTransform.position);
			if (distance < attackrange)
			{
				Stop();
				AddNextAI(eStateType.STATE_ATTACK, targetObject);
			}
			else
			{
				SetMove(targetObject.SelfTransform.position);
			}
		}

		yield return StartCoroutine(base.Move());
	}

	protected override IEnumerator Attack()
	{
		yield return new WaitForEndOfFrame();
		while(IS_ATTACK)
		{
			if (OBJECT_STATE == eBaseObjectState.STATE_DIE)
				break;
			yield return new WaitForEndOfFrame();
		}
		AddNextAI(eStateType.STATE_IDLE);

		yield return StartCoroutine(base.Attack());
	}

	protected override IEnumerator Die()
	{
		END = true;
		yield return StartCoroutine(base.Die());
	}
}
