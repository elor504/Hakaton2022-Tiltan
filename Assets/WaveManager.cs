using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{

	public List<WaveInfo> waveInfo;
	public int currentWave;


	void Awake()
	{
		StartCoroutine(StartWave());
	}
	IEnumerator StartWave()
	{
		yield return new WaitForSeconds(1f);

		while (true)
		{
			yield return new WaitForSeconds(waveInfo[currentWave].timeBetweenSpawn);
			if (waveInfo[currentWave].CanSpawnEnemies())
			{
				EnemyManager.GetInstance.InstantiateEnemy(waveInfo[currentWave].GetRandomEnemy());
				for (int i = 0; i < 1; i++)
				{
					yield return new WaitForSeconds(0.2f);
					HeroManager.GetInstance.InstantiateHero();
				}
			}
		}

	}



}
[Serializable]
public class WaveInfo
{
	public float timeBetweenSpawn;

	public List<EnemyController> enemiesPF;

	public int maxAmountOfEnemiesAlive;


	public bool CanSpawnEnemies()
	{
		return EnemyManager.GetInstance.GetAmountOfEnemiesAlive() < maxAmountOfEnemiesAlive;
	}

	public EnemyController GetRandomEnemy()
	{
		int randomIndex = UnityEngine.Random.Range(0, enemiesPF.Count);
		return enemiesPF[randomIndex];
	}

}