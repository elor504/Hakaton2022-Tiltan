using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIScore : MonoBehaviour
{
	private static UIScore _instance;
	public static UIScore GetInstance => _instance;

	public TextMeshProUGUI score;

	private void Awake()
	{
		if (_instance == null)
			_instance = this;
		else if (_instance != this)
			Destroy(this.gameObject);
	}


	public void SetTextScore(int scoreAmount)
	{
		string type = "";
		float amount = 0;

		if(scoreAmount < 1000)
		{
			score.text = scoreAmount + type;
		}
		else if (scoreAmount >= 1000 && scoreAmount < 1000000)
		{
			type = "K";
			amount = Mathf.FloorToInt(scoreAmount) * 0.001f;
			score.text = amount.ToString("F1") + type;
		}
		else if (scoreAmount >= 1000000)
		{
			type = "M";
			amount = Mathf.Round((scoreAmount / 1000000) * 10.0f) * 0.1f;
			score.text = amount.ToString("F1") + type;
		}




	
	}
}
