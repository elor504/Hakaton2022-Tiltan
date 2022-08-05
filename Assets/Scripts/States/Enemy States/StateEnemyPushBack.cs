using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateEnemyPushBack : BaseState
{
	private EnemyController _controller;
	private EnemyBrain _brain;
	Vector2 _portalPos;

	public StateEnemyPushBack(EnemyController controller,EnemyBrain brain)
	{
		_controller = controller;
		_brain = brain;
	}


	public override void EnterState()
	{
		_portalPos = _controller.GetPortalPos;
	}

	public override void UpdateState()
	{
		if (!_controller.CheckIfNeedToBePushedBack())
		{
			_brain.ChangeState(0);
			return;
		}


		float distance = Vector2.Distance(_portalPos, _controller.transform.position);
		if(distance <= 0.2f)
		{
			_controller.DeactivateEnemy();
		}

		_controller.GoTowardPortal();
	}

	public override void ExitState()
	{

	}
}
