using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
	private static EnemyManager _instance;
	public static EnemyManager GetInstance => _instance;


	public List<Transform> Portals = new List<Transform>();
	public Transform parent;
	public Transform EndTrans;
	[Header("Pool")]
	public List<EnemyController> EnemyPool = new List<EnemyController>();
	public EnemyController EnemyPF;

	private void Awake()
	{
		if (_instance == null)
			_instance = this;
		else if(_instance != this)
		{
			Destroy(this.gameObject);
		}
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.D))
		{
			InstantiateEnemy();
		}
	}


	Transform GetRandomPortal()
	{
		int randomIndex = Random.Range(0, Portals.Count);
		return Portals[randomIndex];
	}
	public void InstantiateEnemy()
	{
		if (EnemyPool.Count == 0)
		{
			Vector2 portalPos = GetRandomPortal().position;
			EnemyController newEnemy = Instantiate(EnemyPF, portalPos, Quaternion.identity, parent);
			newEnemy.Init(portalPos);
			EnemyPool.Add(newEnemy);
		}
		else
		{
			Vector2 portalPos = GetRandomPortal().position;
			for (int i = 0; i < EnemyPool.Count; i++)
			{
				if (!EnemyPool[i].IsActive)
				{

					EnemyPool[i].transform.position = portalPos;
					EnemyPool[i].Init(portalPos);
					EnemyPool[i].gameObject.SetActive(true);
					return;
				}
			}


			EnemyController newEnemy = Instantiate(EnemyPF, portalPos, Quaternion.identity, parent);
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


}
