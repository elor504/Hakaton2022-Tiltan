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
	public int ResistanceBaseCost;
	public int MovementSpeedBaseCost;

	[Header("Upgrade cost Per")]
	public float ResistanceCostPer;
	public float MovementSpeedCostPer;


	[Header("Bonus percentage")]
	public float ResistancePer;
	public float MovementSpeedPer;

	private void Awake()
	{

		if (_instance == null)
			_instance = this;
		else if (_instance != this)
			Destroy(this.gameObject);

	}
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.N))
		{
			UpgradeResistance();
		}
	}

	public void UpgradeResistance()
	{
		int currency = GameManager.GetInstance.Points;

		Debug.Log("Cost: " + GetCost(ResistanceLevel, ResistanceCostPer, ResistanceBaseCost));
		if (currency >= GetCost(ResistanceLevel, ResistanceCostPer, ResistanceBaseCost))
		{

		}

	}

	int GetCost(int level,float per,int baseCost)
	{
		if (level == 1)
			return baseCost;


		return baseCost + (Mathf.RoundToInt(baseCost * (per * level)));
	}

}
