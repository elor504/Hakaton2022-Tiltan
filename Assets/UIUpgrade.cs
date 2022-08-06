using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIUpgrade : MonoBehaviour
{
	private static UIUpgrade _instance;
	public static UIUpgrade GetInstance => _instance;


	public TextMeshProUGUI MovementCostText;
    public TextMeshProUGUI ResistanceCostText;
    public TextMeshProUGUI MaxHeroesCostText;

	private void Awake()
	{
		if (_instance == null)
			_instance = this;
		else if (_instance != this)
			Destroy(this.gameObject);
	}
	public void UpdateTexts()
	{
		MovementCostText.text = GetCostToString(UpgradeManager.GetInstance.GetMovementCost());
		ResistanceCostText.text = GetCostToString(UpgradeManager.GetInstance.GetResistanceCost());
		MaxHeroesCostText.text = GetCostToString(UpgradeManager.GetInstance.GetMaxHeroesCost());


	}


    string GetCostToString(int scoreAmount)
	{
		string type = "";
		float amount = 0;

		if (scoreAmount < 1000)
		{
			return scoreAmount + type;
		}
		else if (scoreAmount >= 1000 && scoreAmount < 1000000)
		{
			type = "K";
			amount = Mathf.FloorToInt(scoreAmount) * 0.001f;
			return amount.ToString("F1") + type;
		}
		else if (scoreAmount >= 1000000)
		{
			type = "M";
			amount = Mathf.Round((scoreAmount / 1000000) * 10.0f) * 0.1f;
			return amount.ToString("F1") + type;
		}
		return null;
	}

}
