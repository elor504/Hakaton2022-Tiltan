using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushPos : MonoBehaviour
{
	public bool IsBeingTargetedByHero;
	public bool IsTakenByHero;
	public HeroController Hero;

	private EnemyController _controller;

	public float GetResistanceFromHero => Hero? Hero.Resistance : 0;

	private void Awake()
	{
		_controller = transform.GetComponentInParent<EnemyController>();
	}
	public void SetBeingTarget(bool isBeingTargeted)
	{
		IsBeingTargetedByHero = isBeingTargeted;
	}

	public void SetPosTakenByHero(HeroController hero)
	{
		this.Hero = hero;
		IsTakenByHero = true;
	}


	public void Clear()
	{
		this.Hero = null;
		IsTakenByHero = false;
		IsBeingTargetedByHero = false;
	}


	public bool IsEnemyActive()
	{
		return _controller.IsActive;
	}







}
