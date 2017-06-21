using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonPlayer : Actor {


	public BaseAI ai = null;
	public BaseAI AI
	{ get { return ai; } }

	bool IsInvincibility = false;
	public bool INVINCIBILITY
	{
		get { return IsInvincibility; }
		set { IsInvincibility = value; }
	}
}
