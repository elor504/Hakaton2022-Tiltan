using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
	private static PathManager _instance;
	public static PathManager GetInstance => _instance;



	[SerializeField] List<Path> _pathList = new List<Path>();



	private void Awake()
	{
		if (_instance == null)
			_instance = this;
		else if (_instance != this)
			Destroy(this.gameObject);
	}


	public List<Transform> GetEnemyRandomizedPath(Transform enemyPos)
	{
		List<Transform> path = new List<Transform>();
		for (int i = 0; i < _pathList.Count; i++)
		{
			Transform randomizedPath = _pathList[i].GetRandomPath();
			if (enemyPos.position.x < randomizedPath.position.x)
				path.Add(randomizedPath);
		}


		return path;
	}
	public List<Transform> GetEnemyRandomizedPath()
	{
		List<Transform> path = new List<Transform>();
		for (int i = 0; i < _pathList.Count; i++)
		{
			path.Add(_pathList[i].GetRandomPath());
		}
		return path;
	}




}
