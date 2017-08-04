
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBoard : BaseBoard {

	// [SerializeField]
	// Force Unity to serialize a private field.
	// private 변수 유니티 강제 동기화 ( 이스펙터 동기화 )
	[SerializeField]
	UIProgressBar ProgressBar = null;
	[SerializeField]
	UILabel HPLabel = null;

	[SerializeField]
	UISprite HPFore = null;
	[SerializeField]
	UISprite HPBack = null;

	public override eBoardType BOARD_TYPE
	{
		get
		{
			return eBoardType.BOARD_HP;
		}
	}

	private void Start()
	{
		//if (this.IS_PLAYER == true)
		//{
			
		//}

		if (TARGET_ACTOR != null)
		{
			switch (TARGET_ACTOR.TEMPLATE_KEY)
			{

				case "CHARACTER_1":
					{
						HPFore.width = 700;
						HPFore.height = 40;
						HPBack.width = 700;
						HPBack.height = 40;

						HPLabel.width = 700;
						HPLabel.height = 40;
						HPLabel.fontSize = 30;
						break;
					}

				case "ENEMY_3":
					{
						HPFore.width = 100;
						HPFore.height = 20;
						HPBack.width = 100;
						HPBack.height = 20;

						HPLabel.width = 150;
						HPLabel.height = 20;
						HPLabel.fontSize = 20;
					}
					break;

			}
		}
	}

	public override void SetData(string strKey, params object[] datas)
	{
		if (strKey.Equals(ConstValue.SetData_HP) )
		{
			// [0] Max   , [1] Cur
			double MaxHP = (double)datas[0];
			double CurHP = (double)datas[1];

			ProgressBar.value = (float)(CurHP / MaxHP); // 0f ~ 1f

			//if (this.IS_PLAYER == true)
			//{
			
			//}

			//actor가 존재하는 경우
			if (TARGET_ACTOR != null)
			{
				switch (TARGET_ACTOR.TEMPLATE_KEY)
				{
					case "CHARACTER_1":
						{
							HPLabel.text = ((int)CurHP).ToString() + " / " + MaxHP.ToString();
						}
						break;

					case "ENEMY_3":
						{
							HPLabel.text = ((int)CurHP).ToString() + " / " + MaxHP.ToString();
						}
						break;

					default:
						HPLabel.text = string.Empty;
						//HPLabel.text = ((int)CurHP).ToString() + " / " + MaxHP.ToString();

						

						
						break;
				}


			}
			else
			{
				HPLabel.text = string.Empty;

				//HPLabel.text = CurHP.ToString()
				//	 + " / " + MaxHP.ToString();
			}
		}
	}

}
