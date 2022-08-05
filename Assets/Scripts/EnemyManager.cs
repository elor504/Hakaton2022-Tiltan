using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
	private static EnemyManager _instance;
	public static EnemyManager GetInstance => _instance;


	public List<Portal> Portals = new List<Portal>();
	public Transform parent;
	public Transform EndTrans;
	[Header("Pool")]
	public List<EnemyController> EnemyPool = new List<EnemyController>();
	public EnemyController EnemyPF;
	public EnemyController EnemyTwoPF;
	public EnemyController EnemyThreePF;

	private void Awake()
	{
		if (_instance == null)
			_instance = this;
		else if(_instance != this)
			Destroy(this.gameObject);
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.D))
		{
			//InstantiateEnemy(EnemyPF);
			//InstantiateEnemy(EnemyTwoPF);
			//InstantiateEnemy(EnemyThreePF);
		}
	}

	Portal GetRandomPortal()
	{
		int randomIndex = Random.Range(0, Portals.Count);
		return Portals[randomIndex];
	}
	public void InstantiateEnemy(EnemyController PF)
	{
		SoundManager.getInstance.PlaySpawnMonsterSfx();
		if (EnemyPool.Count == 0)
		{
			Portal portalPos = GetRandomPortal();
			EnemyController newEnemy = Instantiate(PF, portalPos.transform.position, Quaternion.identity, parent);
			newEnemy.Init(portalPos);
			EnemyPool.Add(newEnemy);
		}
		else
		{
			Portal portalPos = GetRandomPortal();
			for (int i = 0; i < EnemyPool.Count; i++)
			{
				if (!EnemyPool[i].IsActive && EnemyPool[i].ID == PF.ID)
				{
					EnemyPool[i].transform.position = portalPos.transform.position;
					EnemyPool[i].Init(portalPos);
					EnemyPool[i].gameObject.SetActive(true);
					return;
				}
			}


			EnemyController newEnemy = Instantiate(PF, portalPos.transform.position, Quaternion.identity, parent);
			newEnemy.Init(portalPos);
			EnemyPool.Add(newEnemy);
		}
	}
	public EnemyController GetClosestToEndEnemy()
	{
		float distance = float.MaxValue;
		EnemyController closestEnemy = null;
		if (EnemyPool.Count != 0)
		{
			for (int i = 0; i < EnemyPool.Count; i++)
			{
				if (EnemyPool[i].IsActive)
				{
					if (EnemyPool[i].CanBeTargeted())
					{
						if (Vector2.Distance(EnemyPool[i].transform.position, EndTrans.position) < distance)
						{
							distance = Vector2.Distance(EnemyPool[i].transform.position, EndTrans.position);
							closestEnemy = EnemyPool[i];
						}
					}

				}
			}
		}
		return closestEnemy;
	}
	public int GetAmountOfEnemiesAlive()
	{
		int amount = 0;

		for (int i = 0; i < EnemyPool.Count; i++)
		{
			if (EnemyPool[i].IsActive)
			{
				amount++;
			}
		}

		return amount;
	}
}
