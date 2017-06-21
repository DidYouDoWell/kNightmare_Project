using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonPlayer : Actor {


	BaseAI ai = null;
	public BaseAI AI
	{ get { return ai; } }

	private void Start()
	{
		switch (TEMPLATE_KEY)
		{
			case "ENEMY_1":
				{
					AiSetting(eAIType.NormalEnemy);
				}
				break;
			case "ENEMY_2":
				{
					AiSetting(eAIType.NormalEnemy);
				}
				break;
		}

		ai.Target = this;
	}

	protected override void Update()
	{
		AI.UpdateAI();
		if (AI.END)
		{
			Destroy(SelfObject);
		}

		base.Update();
	}

	void AiSetting(eAIType aitype)
	{
		GameObject aiObject = new GameObject();
		aiObject.name = aitype.ToString();
		switch (aitype)
		{
			case eAIType.NormalEnemy:
				{
					ai = aiObject.AddComponent<NormalEnemy>();
				}
				break;
		}
		aiObject.transform.SetParent(SelfTransform);
	}
}
