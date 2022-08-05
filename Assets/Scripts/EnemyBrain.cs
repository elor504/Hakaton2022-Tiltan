using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBrain : MonoBehaviour
{
	public EnemyController Controller;


	BaseState _currentState;

	//states
	[SerializeField]private StateEnemyGoTowardGoal _goalState;
	[SerializeField] private StateEnemyPushBack _pushedState;

	private void Awake()
	{
		_goalState = new StateEnemyGoTowardGoal(Controller,this);
		_pushedState = new StateEnemyPushBack(Controller,this);
		_currentState = _goalState;
		_currentState.EnterState();
	}


	private void Update()
	{
		_currentState.UpdateState();


		if (Input.GetKeyDown(KeyCode.S))
		{
			ChangeState(1);
		}
	}

	public void ChangeState(int index)
	{
		_currentState.ExitState();
		_currentState = GetStateByIndex(index);
		_currentState.EnterState();
	}

	BaseState GetStateByIndex(int index)
	{
		if(index == 0)
		{
			return _goalState;
		} 
		else if(index == 1)
		{
			return _pushedState;
		}
		return null;
	}

}
