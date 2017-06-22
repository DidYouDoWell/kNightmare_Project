using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(PlayerController))]

public class Player : MonoBehaviour {


	protected ePlayerStateType CurrentState = ePlayerStateType.STATE_IDLE;
	public ePlayerStateType CURRENT_STATE
	{
		get { return CurrentState; }
	}
	Animator Anim = null;
	public Animator ANIMATOR
	{
		get
		{
			if (Anim == null)
			{
				Anim = gameObject.GetComponentInChildren<Animator>();
			}
			return Anim;
		}
	}

	bool bAttack = false;
	public bool IS_ATTACK
	{
		get { return bAttack; }
		set { bAttack = value; }
	}


	/// <summary>
	/// /
	/// </summary>



	//Vector3 vec3Dis;
	Vector3 playerposition;
	Vector3 preTransform;
	float curTime=0f;
	bool playerOnClicked = false;

	//public float MoveSpeed;
	public float Dis = 0;
	public float limitDis = 8;
	public float moveSpeed = 5;
	PlayerController controller;
	Camera viewCamera;
	Vector3 heightCorrectedPoint;
	bool Moving=false;
    //은기 소스 추가구문
    public float Velocity = 0.0f;
    public DrawTimeSkill DrawSkill = null;
    private Vector3 NextPosition = Vector3.zero;
    private int NextPositionNum = 0;

    // Use this for initialization
    void Start () {

        controller = GetComponent<PlayerController>();

        viewCamera = Camera.main;
        //은기 소스 추가구문
        Velocity = 3.0f;
        NextPositionNum = 0;
        //DrawSkill = GameObject.FindGameObjectWithTag("Skill").GetComponent<DrawTimeSkill>();

    }

	// Update is called once per frame
	void Update() {


		Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
		Vector3 moveVelocity = moveInput.normalized * moveSpeed;
		controller.Move(moveVelocity);

		Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);
		Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
		float rayDistance;



		if (groundPlane.Raycast(ray, out rayDistance))
		//true라면 ray가 바닥 플레인과 교차한 것이고 
		//그러면 카메라에서 ray가 부딫친 지점까지의 거리를 알 수 있고,
		//그럼 실제 교차 지점을 지정할 수 있음.
		{

			Vector3 point = ray.GetPoint(rayDistance);
			//Debug.Log(rayDistance);
			Debug.DrawLine(ray.origin, point, Color.red);

			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, rayDistance))
			{
				if (hit.collider.tag=="Player")
				{
					playerOnClicked = true;

					

				}
			}

				//
				if (Input.GetMouseButton(0) && Moving == false && playerOnClicked==true)
			{

				//
				ProcessCrouch();

				heightCorrectedPoint = new Vector3(point.x, transform.position.y, point.z);


				//	vec3Dis = heightCorrectedPoint - transform.position;
				//con = vec3Dis.normalized * moveSpeed;


				//Debug.Log(heightCorrectedPoint);
				controller.LookAt(heightCorrectedPoint);

				preTransform = transform.position;
				playerposition = preTransform;
				//Dis = Vector3.Distance(preTransform, heightCorrectedPoint);

			}




		}

        if (((Input.GetMouseButtonUp(0) || Moving == true) && playerOnClicked == true)
            && GameManager.Instance.IS_START_SKILL == false)// 교준아 여기 조건 바꿨으니까 확인
        //if ((Input.GetMouseButtonUp(0) || Moving == true) &&playerOnClicked==true)
        {

			ProcessAttack();
            Moving = true;

            curTime += Time.deltaTime;

			//움직이는 속도를 일정하게 할 것인가?
			playerposition= Vector3.MoveTowards(preTransform, heightCorrectedPoint, limitDis* curTime* Velocity);
			//playerposition = Vector3.Lerp(playerposition, heightCorrectedPoint, curTime*MoveSpeed );

			transform.position=playerposition;

			Dis = Vector3.Distance(preTransform, playerposition);



            if (transform.position == heightCorrectedPoint)
            {
                Moving = false;
                curTime = 0;
                playerOnClicked = false;
                ProcessIdle();

            }

            if (Dis >= limitDis)
            {
                Moving = false;
                curTime = 0;
                playerOnClicked = false;
                ProcessIdle();
            }
        }

        else if (GameManager.Instance.IS_START_SKILL == true)
        {
            //DrawSkill.DrawReal();
            ProcessAttack();
            transform.position = DrawSkill.positions[NextPositionNum];


            if (DrawSkill.position_num <= NextPositionNum)
            {
                GameManager.Instance.IS_START_SKILL = false;
                GameManager.Instance.SkillTimeCheck = 0.0f;
                curTime = 0;
                ProcessIdle();
                NextPositionNum = 0;
                DrawSkill.position_num = 0;
            }
            NextPositionNum++;
        }
        //if (Input.GetMouseButtonUp(0)|| Moving==true)
        //{
        //	Moving = true;
        //	Debug.Log("Move!");

            //	Vector3 ContmoveVelocity = heightCorrectedPoint.normalized * moveSpeed;
            //	controller.Move(ContmoveVelocity);

            //	if(controller.transform.position== heightCorrectedPoint)
            //	{
            //		Moving = false;
            //	}
            //}

    }

	private void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.tag=="Enemy")
		{
			Destroy(collision.gameObject);
		}
	}


	void ChangeAnimation()
	{
		if (ANIMATOR == null)
		{
			Debug.LogError(gameObject.name + " 에게 Animator가 없습니다.");
			return;
		}

		ANIMATOR.SetInteger("State", (int)CurrentState);
	}

	protected virtual void ProcessIdle()
	{
		CurrentState = ePlayerStateType.STATE_IDLE;
		ChangeAnimation();
	}

	protected virtual void ProcessCrouch()
	{
		CurrentState = ePlayerStateType.STATE_CROUCH;
		ChangeAnimation();
	}

	protected virtual void ProcessAttack()
	{
		//Target.ThrowEvent(ConstValue.EventKey_SelectSkill, 0);
		CurrentState = ePlayerStateType.STATE_ATTACK;
		ChangeAnimation();
	}

	protected virtual void ProcessDie()
	{
		CurrentState = ePlayerStateType.STATE_DEAD;
		ChangeAnimation();
	}


}
