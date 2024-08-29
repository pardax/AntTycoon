using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;
using Spine.Unity;
using System;

public class BaseController : UI_Base
{
	//protected SkeletonGraphic _anim = null;

	public override bool Init()
	{
		if (base.Init() == false)
			return false;

		//_anim = GetComponent<SkeletonGraphic>();
		return true;
	}

	protected virtual void UpdateAnimation() { }

}
