using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemy : BaseAI {

	BaseObject targetObject;

    Vector3 RollDir = Vector3.zero;
    float dodgeDis = 2f;

	protected override IEnumerator Idle()
	{
		searchDis = 30f;
		targetObject = ActorManager.Instance.GetSearchEnemy(Target, searchDis);

		if (targetObject != null)
		{
			SkillData sData =
				Target.GetData(ConstValue.ActorData_SkillData, 0) as SkillData;
			float attackRange = 1f;

			if (sData != null)
				attackRange = sData.RANGE;

			float distance = Vector3.Distance(
				targetObject.SelfTransform.position,
				SelfTransform.position);

			if (distance <= dodgeDis)
			{
				Stop();
				AddNextAI(eStateType.STATE_SPECIAL);
			}
			else if (distance < attackRange && IS_ATTACK_COOLDOWN == false)
			{
				Stop();
				AddNextAI(eStateType.STATE_ATTACK, targetObject);
			}
			else
			{
				AddNextAI(eStateType.STATE_IDLE);
			}
		}
		else
		{
			AddNextAI(eStateType.STATE_IDLE);
		}

		yield return StartCoroutine(base.Idle());
	}

	protected override IEnumerator Attack()
	{
		IS_ATTACK_COOLDOWN = true;
		Invoke("OffAttackCoolDown", 2f); // 시간부분은 나중에 JSON 공격속도 부분으로

		yield return new WaitForEndOfFrame();
		while (IS_ATTACK)
		{
			if (OBJECT_STATE == eBaseObjectState.STATE_DIE)
				break;
			yield return new WaitForEndOfFrame(); // IDLE로 넘어가기 전에 이 애니메이션이 끝났는지 체크.
												  // 코루틴은 따로돌기때문에 가능한 구성.
		}

		AddNextAI(eStateType.STATE_IDLE);

		yield return StartCoroutine(base.Attack());
	}
	void OffAttackCoolDown()
	{
		IS_ATTACK_COOLDOWN = false;
	}

	protected override IEnumerator Die()
	{
		AddNextAI(eStateType.STATE_DEAD);
        //StopCoroutine("RollDirCheck");
		Invoke("End", 4f);
		yield return StartCoroutine(base.Die());
	}
	void End()
	{
		END = true;
	}

	protected override IEnumerator Special()
	{
        RollDirCheck();
        
        SelfTransform.forward = RollDir;

        yield return new WaitForEndOfFrame();
        while (IS_SKILL)
        {
            RIGID_BODY.MovePosition(RIGID_BODY.position + RollDir * 2.3f * Time.deltaTime);
            if (OBJECT_STATE == eBaseObjectState.STATE_DIE)
                break;
            yield return new WaitForEndOfFrame();
        }
		AddNextAI(eStateType.STATE_IDLE);

		yield return StartCoroutine(base.Special());
	}
	void OffSkillCollTime()
	{
		IS_SKILL_COOLDOWN = false;
	}

    void RollDirChoice()
    {
		RollDir = new Vector3(Random.Range(-1.0f, 1.0f), 0, Random.Range(-1.0f, 1.0f));
    }
    void RollDirCheck()
    {
        RaycastHit other;
        RollDirChoice(); // RollDir 을 Random.Range함수를 이용해 동서남북 방향중 한방향으로 정해주는 함수.

        bool DonRoll = true;

        while (DonRoll)
        {
            if (Physics.Raycast(SelfTransform.position, RollDir, out other, 3f))
            {
                if (other.collider.gameObject.tag == "Obstacle"
                    || other.collider.gameObject.tag == "Player")
                {// 레이를 쏴서 맞은 콜라이더(객체)가 장애물이거나 플레이어 일때 다시 방향 설정.
                    RollDirChoice();
                }
                else // 레이를 쏴서 맞은 콜라이더가 장애물, 플레이어 둘다 아닐경우 bool값을 false줘서 while문을 빠져나가고 그 방향 설정.
                    DonRoll = false;
                
            }
            else // 레이를 쏴서 3f 내에 아무것도 없을시 그 방향 설정.
                DonRoll = false;
        }
    }
}
