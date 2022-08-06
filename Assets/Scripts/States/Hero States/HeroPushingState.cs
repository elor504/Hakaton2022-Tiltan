using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroPushingState : BaseState
{
	HeroController _controller;
	HeroBrain _brain;
	HeroAnimation _animation;
	string _animationName;

	public HeroPushingState(HeroController controller, HeroBrain brain, HeroAnimation animation, string animationName) : base()
	{
		_brain = brain;
		_controller = controller;
		_animation = animation;
		_animationName = animationName;
	}

	public override void EnterState()
	{
		SoundManager.getInstance.PlayUnitsMeetSfx();
		_animation.SetAnimatorBool(_animationName, true);
		GameManager.GetInstance.AddPoints(_controller.PointsOnPush);
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
		_animation.SetAnimatorBool(_animationName, false);
	}
}
