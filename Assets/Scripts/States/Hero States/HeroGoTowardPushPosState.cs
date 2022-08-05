using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroGoTowardPushPosState : BaseState
{
	HeroController _controller;
	HeroBrain _brain;

	public HeroGoTowardPushPosState(HeroController controller,HeroBrain brain) : base()
	{
		_brain = brain;
		_controller = controller;
	}

	public override void EnterState()
	{

	}

	public override void UpdateState()
	{
		if(_brain.PushPos == null || !_brain.PushPos.IsEnemyActive())
		{
			_brain.ChangeState(0);
			return;
		}

		if (!_brain.PushPos.IsTakenByHero)//the hero is not near it
		{
			float distance = Vector2.Distance(_brain.transform.position, _brain.PushPos.transform.position);
			_controller.GoTowardPosition(_brain.PushPos.transform.position);
			if (distance < 0.05f)
			{
				_brain.PushPos.IsTakenByHero = true;
			}
		}
		else// the hero is near it
		{
			_brain.PushPos.SetPosTakenByHero(_controller);
			_brain.ChangeState(2);
		}
	}

	public override void ExitState()
	{

	}
}
