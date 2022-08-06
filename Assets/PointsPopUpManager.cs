using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsPopUpManager : MonoBehaviour
{
	private static PointsPopUpManager _instance;
	public static PointsPopUpManager GetInstance => _instance;


	public GameObject PopUpPF;
	public Transform Parent;
	public List<GameObject> PopUpPool;

	private void Awake()
	{
		if (_instance == null)
			_instance = this;
		else if (_instance != this)
			Destroy(this.gameObject);
	}

	public void InstantiatePopUp(Vector2 worldPosition,string pointsAmount)
	{
		return;


		if(PopUpPool.Count == 0)
		{
			GameObject newPopUp = Instantiate(PopUpPF, worldPosition,Quaternion.identity, Parent);
			//init
			PopUpPool.Add(newPopUp);
		}
		else
		{

			for (int i = 0; i < PopUpPool.Count; i++)
			{
				if (!PopUpPool[i].activeInHierarchy)
				{
					PopUpPool[i].transform.position = worldPosition;
					//init
					PopUpPool[i].SetActive(true);
					return;
				}
			}

			GameObject newPopUp = Instantiate(PopUpPF, worldPosition, Quaternion.identity, Parent);
			//init
			PopUpPool.Add(newPopUp);


		}

	}


}
