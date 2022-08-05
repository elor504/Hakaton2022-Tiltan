using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroIdleState : BaseState
{
	HeroController _controller;
	HeroBrain _brain;
	HeroAnimation _animation;
	string _animationName;

	public HeroIdleState(HeroController controller,HeroBrain brain,HeroAnimation animation,string animationName) : base()
	{
		_controller = controller;
		_brain = brain;
		_animation = animation;
		_animationName = animationName;
	}

	public override void EnterState()
	{
		_animation.SetAnimatorBool(_animationName, true);
		_brain.PushPos = null;
	}

	public override void UpdateState()
	{
	
	}

	public override void ExitState()
	{
		_animation.SetAnimatorBool(_animationName, false);
	}

}
