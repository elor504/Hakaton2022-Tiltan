using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
	private static UpgradeManager _instance;
	public static UpgradeManager GetInstance => _instance;

	[Header("Upgrades current Level")]
	public int ResistanceLevel;
	public int MovementSpeedLevel;
	public int AmountOfHeroesLevel;

	[Header("Upgrade cost Base")]
	public List<int> ResistanceCost = new List<int>();
	public List<int> MovementSpeedCost = new List<int>();
	public List<int> AmountOfHeroesCost = new List<int>();

	//[Header("Upgrade cost Per")]
	//public float ResistanceCostPer;
	//public float MovementSpeedCostPer;


	[Header("Bonus percentage")]
	public float ResistancePer;
	public float MovementSpeedPer;

	private void Awake()
	{
		ResistanceLevel = 1;
		MovementSpeedLevel = 1;
		AmountOfHeroesLevel = 1;
		if (_instance == null)
			_instance = this;
		else if (_instance != this)
			Destroy(this.gameObject);


		UIUpgrade.GetInstance.UpdateTexts();

	}
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.N))
		{
			UpgradeResistance();
			//UpgradeMovement();
		}
	}

	public void UpgradeResistance()
	{
		if (ResistanceLevel == 9)
			return;

		int currency = GameManager.GetInstance.Points;

		Debug.Log("Cost: " + GetResistanceCost());
		if(GameManager.GetInstance.HasEnoughPoints(GetResistanceCost()))
		{
			ResistanceLevel++;
			GameManager.GetInstance.RemovePoints(GetResistanceCost());
			UIUpgrade.GetInstance.UpdateTexts();
		}
	}

	public float GetResistancePercentage()
	{
		return (ResistancePer * ResistanceLevel) - ResistancePer;
	}
	public int GetResistanceCost()
	{
		return GetCost(ResistanceLevel, ResistanceCost);
	}


	public void UpgradeMovement()
	{
		if (MovementSpeedLevel == 9)
			return;

		Debug.Log("Cost: " + GetMovementCost());
		if(GameManager.GetInstance.HasEnoughPoints(GetMovementCost()))
		{
			MovementSpeedLevel++;
			GameManager.GetInstance.RemovePoints(GetMovementCost());
			UIUpgrade.GetInstance.UpdateTexts();
		}
	}

	public float GetMovementPercentage()
	{
		return (MovementSpeedPer * MovementSpeedLevel) - MovementSpeedPer;
	}

	public int GetMovementCost()
	{
		return GetCost(MovementSpeedLevel, MovementSpeedCost);
	}
	public void UpgradeMaxHeroes()
	{
		if (AmountOfHeroesLevel == 9)
			return;

		Debug.Log("Cost: " + GetMaxHeroesCost());
		if (GameManager.GetInstance.HasEnoughPoints(GetMaxHeroesCost()))
		{
			AmountOfHeroesLevel++;
			GameManager.GetInstance.RemovePoints(GetMaxHeroesCost());
			UIUpgrade.GetInstance.UpdateTexts();
		}
	}
	public int GetMaxHeroesCost()
	{
		return GetCost(AmountOfHeroesLevel, AmountOfHeroesCost);
	}

	int GetCost(int level,List<int> baseCost)
	{
		return baseCost[level - 1];
	}
}
