using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroPushingState : BaseState
{
	HeroController _controller;
	HeroBrain _brain;

	public HeroPushingState(HeroController controller, HeroBrain brain) : base()
	{
		_brain = brain;
		_controller = controller;
	}

	public override void EnterState()
	{

	}

	public override void UpdateState()
	{
		if(_brain.PushPos.Hero == null)
		{
			_controller.DisableHero();
			//if its null it got resetted
			_brain.PushPos = null;
			_brain.ChangeState(0);
			return;
		}
		_controller.transform.position = _brain.PushPos.transform.position;
	}

	public override void ExitState()
	{

	}
}
