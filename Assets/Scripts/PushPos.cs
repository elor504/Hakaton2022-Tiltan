using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushPos : MonoBehaviour
{
	public bool IsTakenByHero;
	public HeroController Hero;
	public float GetResistanceFromHero => Hero.Resistance;



	public void SetPosTakenByHero(HeroController hero)
	{
		this.Hero = hero;
		IsTakenByHero = true;
	}


	public void Clear()
	{
		this.Hero = null;
		IsTakenByHero = false;
	}











}
