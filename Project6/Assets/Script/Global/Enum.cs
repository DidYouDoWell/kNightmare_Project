﻿
public enum eBaseObjectState
{
    STATE_NORMAL,
    STATE_DIE,
}

public enum eStateType
{
    STATE_NONE = 0,
    STATE_IDLE,
    STATE_ATTACK,
    STATE_WALK,
    STATE_DEAD
}

public enum ePlayerStateType
{
	STATE_NONE = 0,
	STATE_IDLE,
	STATE_ATTACK,
	STATE_CROUCH,
	STATE_DEAD
}

public enum eStatusData
{
    MAX_HP,
    ATTACK,
    DEFFENCE,
    MAX,
}

public enum eTeamType
{
    TEAM_1,
    TEAM_2,
}


// Monster 관련
public enum eRegeneratorType
{
    NONE,
    REGENTIME_EVENT,
    TRIGGER_EVENT,
}

public enum eMonsterType
{
    A_Monster,
    B_Monster,
    C_Monster,
    MAX,
}

// Skill 관련
public enum eSkillTemplateType
{
    TARGET_ATTACK,
    RANGE_ATTACK,
}

public enum eSkillAttackRangeType
{
    RANGE_BOX,
    RANGE_SPHERE,
}

public enum eSkillModelType
{
    CIRCLE,
    BOX,
    MAX
}

public enum eBoardType
{
    BOARD_NONE,
    BOARD_HP,
    BOARD_DAMAGE,
}

public enum eClearType
{
    CLEAR_KILLCOUNT = 0,
    CLEAR_TIME,
}

public enum eSceneType
{
    SCENE_NONE,
    SCENE_TITLE,
    SCENE_GAME,
    SCENE_LOBBY,
}

public enum eUIType
{
    PF_UI_LOGO,
    PF_UI_LOADING,
    PF_UI_LOBBY,
    PF_UI_INVENTORY,
    PF_UI_POPUP,
    PF_UI_STAGE,
    PF_UI_GACHA,
}

public enum eSlotType
{
    SLOT_NONE = -1,
    SLOT_WEAPON = 0,
    SLOT_ARMOR,
    SLOT_HELMAT,
    SLOT_GUNTLET,
    SLOT_MAX,
}

// AI 관련
public enum eAIType
{
	AI_MELEE,
	AI_RANGE,
}

public enum STEP
{

    NONE = -1,

    IDLE = 0,       // 대기 중.
    DRAWING,        // 라인 그리는 중 （드래그 중）.
    DRAWED,         // 라인 그리기 종료. 
    CREATED,        // 도로 모델이 생성됨.

    NUM,
};