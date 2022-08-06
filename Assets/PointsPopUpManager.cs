using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsPopUpManager : MonoBehaviour
{
	private static PointsPopUpManager _instance;
	public static PointsPopUpManager GetInstance => _instance;


	public PopUpPoints PopUpPF;
	public Transform Parent;
	public List<PopUpPoints> PopUpPool = new List<PopUpPoints>();

	private void Awake()
	{
		if (_instance == null)
			_instance = this;
		else if (_instance != this)
			Destroy(this.gameObject);
	}

	public void InstantiatePopUp(Vector2 worldPosition,string pointsAmount)
	{

		if(PopUpPool.Count == 0)
		{
			PopUpPoints newPopUp = Instantiate(PopUpPF, worldPosition,Quaternion.identity, Parent);
			//init 
			newPopUp.Init(pointsAmount, worldPosition);
			PopUpPool.Add(newPopUp);
		}
		else
		{
			for (int i = 0; i < PopUpPool.Count; i++)
			{
				if (!PopUpPool[i].IsActive)
				{
					PopUpPool[i].transform.position = worldPosition;
					PopUpPool[i].Init(pointsAmount, worldPosition);
					//PopUpPool[i].gameObject.SetActive(true);
					return;
				}
			}

			PopUpPoints newPopUp = Instantiate(PopUpPF, worldPosition, Quaternion.identity, Parent);
			newPopUp.Init(pointsAmount, worldPosition);
			PopUpPool.Add(newPopUp);
		}

	}


}
