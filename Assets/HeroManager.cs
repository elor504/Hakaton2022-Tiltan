using UnityEngine;
using System.Collections.Generic;

public class HeroManager : MonoBehaviour
{
	private static HeroManager _instance;
	public static HeroManager GetInstance => _instance;

	public HeroController heroPF;
	public List<HeroController> HeroPool = new List<HeroController>();
	public Transform parent;
	public Transform endTrans;
	private void Awake()
	{
		if (_instance == null)
			_instance = this;
		else if (_instance != this)
			Destroy(this.gameObject);
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.S))
		{
			InstantiateHero();
		}
		Test();
	}

	void Test()
	{
		EnemyController closestEnemyToEnd = EnemyManager.GetInstance.GetClosestToEndEnemy();

		if (closestEnemyToEnd != null)
		{
			HeroController closestHeroToEnemy = GetClosestHeroToEnemy(closestEnemyToEnd);
			if (closestHeroToEnemy != null)
			{
				closestHeroToEnemy.Brain.SetHeroTarget(closestEnemyToEnd.GetAvailablePushPos());
			}
		}
	}

	HeroController GetClosestHeroToEnemy(EnemyController enemy)
	{
		HeroController closestHero = null;
		float distance = float.MaxValue;
		for (int i = 0; i < HeroPool.Count; i++)
		{
			if (HeroPool[i].IsActive && !HeroPool[i].IsHeroAttacking())
			{
				if (Vector2.Distance(HeroPool[i].transform.position, enemy.transform.position) < distance)
				{
					distance = Vector2.Distance(HeroPool[i].transform.position, enemy.transform.position);
					closestHero = HeroPool[i];
				}
			}
		}

		return closestHero;
	}

	public void InstantiateHero()
	{
		if (HeroPool.Count == 0)
		{
			HeroController newHero = Instantiate(heroPF, endTrans.position, Quaternion.identity, parent);
			newHero.InitHero(endTrans.position);
			HeroPool.Add(newHero);
		}
		else
		{
			for (int i = 0; i < HeroPool.Count; i++)
			{
				if (!HeroPool[i].IsActive)
				{
					HeroPool[i].InitHero(endTrans.position);
					return;
				}
			}
			HeroController newHero = Instantiate(heroPF, endTrans.position, Quaternion.identity, parent);
			newHero.InitHero(endTrans.position);
			HeroPool.Add(newHero);
		}
	}

}
