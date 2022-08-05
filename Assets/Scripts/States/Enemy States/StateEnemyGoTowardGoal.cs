using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class StateEnemyGoTowardGoal : BaseState
{
    private EnemyController _controller;
	private EnemyBrain _brain;
	[SerializeField] List<Transform> _paths = new List<Transform>();

    public StateEnemyGoTowardGoal(EnemyController controller,EnemyBrain brain)
	{
		_controller = controller;
		_brain = brain;
	}


	public override void EnterState()
	{
		_paths = PathManager.GetInstance.GetEnemyRandomizedPath(_controller.transform);
	}

	public override void UpdateState()
	{
		//if current resistance is higher then resitance tolerance
		if (_controller.CheckIfNeedToBePushedBack())
		{
			_brain.ChangeState(1);
			return;
		}

		if (_paths.Count == 0)
			return;

		_controller.GoTowardPosition(_paths[0].position);
		float distance = Vector2.Distance(_controller.transform.position, _paths[0].position);
		if(distance <= 0.2f)
		{
			_paths.RemoveAt(0);
		}
	}

	public override void ExitState()
	{
		
	}
}
