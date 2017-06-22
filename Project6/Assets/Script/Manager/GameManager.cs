using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public DrawTimeSkill DrawSkill = null;
    public float SkillTimeCheck = 0.0f;
    private bool IsStartSkill = false;
    public bool IS_START_SKILL
    {
        get { return IsStartSkill; }
        set { IsStartSkill = value; }
    }
	// Use this for initialization
	void Start () {
        DrawSkill = GameObject.FindGameObjectWithTag("Skill").GetComponent<DrawTimeSkill>();
        DrawSkill.gameObject.SetActive(false);
        SkillTimeCheck = 0.0f;
        IsStartSkill = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (DrawSkill.IS_DRAW_SKILL && SkillTimeCheck < 2f)
        {
            DrawSkill.gameObject.SetActive(true);

            SkillTimeCheck += Time.deltaTime;
        }
        else if(SkillTimeCheck >= 2f)
        {
            IsStartSkill = true;
            DrawSkill.IS_DRAW_SKILL = false;
            DrawSkill.gameObject.SetActive(false);
            SkillTimeCheck += Time.deltaTime;
            if (DrawSkill.step == STEP.DRAWED)
            {
                DrawSkill.next_step = STEP.IDLE;
            }
        }
    }

    public void OnTimeSkillBtn()
    {
        if (DrawSkill.step == STEP.IDLE)
        {
            DrawSkill.next_step = STEP.DRAWED;
        }
        DrawSkill.IS_DRAW_SKILL = true;
        SkillTimeCheck = 0.0f;
    }
}
