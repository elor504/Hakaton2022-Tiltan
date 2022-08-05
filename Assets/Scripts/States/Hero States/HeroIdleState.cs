using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroIdleState : BaseState
{
	HeroController _controller;
	HeroBrain _brain;



	public HeroIdleState(HeroController controller,HeroBrain brain) : base()
	{
		_controller = controller;
		_brain = brain;
	}

	public override void EnterState()
	{
		_brain.PushPos = null;
	}

	public override void UpdateState()
	{
	
	}

	public override void ExitState()
	{
		
	}

}
