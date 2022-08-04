using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
	public List<Transform> Portals = new List<Transform>();
	public Transform parent;
	[Header("Pool")]
	public List<EnemyController> EnemyPool = new List<EnemyController>();
	public EnemyController EnemyPF;


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
			EnemyController newEnemy = Instantiate(EnemyPF, GetRandomPortal().position, Quaternion.identity, parent);
			newEnemy.Init();
			EnemyPool.Add(newEnemy);
		}
		else
		{
			for (int i = 0; i < EnemyPool.Count; i++)
			{
				if (!EnemyPool[i].IsActive)
				{
					EnemyPool[i].transform.position = GetRandomPortal().position;
					EnemyPool[i].Init();
					EnemyPool[i].gameObject.SetActive(true);
					return;
				}
			}

			EnemyController newEnemy = Instantiate(EnemyPF, GetRandomPortal().position, Quaternion.identity, parent);
			newEnemy.Init();
			EnemyPool.Add(newEnemy);
		}
	}



}
