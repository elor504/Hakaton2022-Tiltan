using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroBrain : MonoBehaviour
{
	public HeroController Controller;


	private BaseState _currentState;

	private HeroIdleState _idleState;
	private HeroGoTowardPushPosState _goTowardPushPosState;
	private HeroPushingState _pushingState;

	public PushPos PushPos;

	private void Awake()
	{
		_idleState = new HeroIdleState(Controller, this);
		_goTowardPushPosState = new HeroGoTowardPushPosState(Controller, this);
		_pushingState = new HeroPushingState(Controller, this);
		_currentState = _idleState;
		_currentState.EnterState();
	}

	private void Update()
	{
		if (_currentState == null)
			return;
		_currentState.UpdateState();
	}



	public void SetHeroTarget(PushPos pushPos)
	{
		PushPos = pushPos;
		ChangeState(1);
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
			return _idleState;
		}
		else if (index == 1)
		{
			return _goTowardPushPosState;
		}
		else if(index == 2)
		{
			return _pushingState;
		}
		return null;
	}

	public bool IsHeroAttacking()
	{
		return _currentState != _idleState;
	}

}
