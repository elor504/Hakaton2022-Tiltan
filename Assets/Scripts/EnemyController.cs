using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
	[SerializeField] float _movementSpeed;
	[SerializeField] float _resistanceTolerance;

	public float CurrentTotalResistance;
	[SerializeField]float _totalResistance;
	bool _moveTowardGoal => CurrentTotalResistance < _resistanceTolerance;

	[SerializeField]Rigidbody2D _rb;
	Vector2 dir;

	public bool IsActive;
	Vector2 _portalPos;
	public Vector2 GetPortalPos => _portalPos;

	public List<PushPos> HeroPos = new List<PushPos>();

	public float GetCurrentResistance()
	{
		float currentResistance = 0;
		for (int i = 0; i < HeroPos.Count; i++)
		{
			if (HeroPos[i].IsTakenByHero)
				currentResistance += HeroPos[i].GetResistanceFromHero;
		}
		return currentResistance;
	}



	private void Awake()
	{
		_rb = GetComponent<Rigidbody2D>();
	}

	public void Init(Vector2 portalPos)
	{
		_portalPos = portalPos;
		IsActive = true;
	}
	public void GoTowardPosition(Vector2 pos)
	{
		dir = (pos - (Vector2)this.transform.position).normalized;
		
		float speed = _movementSpeed /2;
		_rb.position += (dir * speed) * Time.deltaTime;
	}
	public void GoTowardPortal()
	{
		dir = (_portalPos - (Vector2)this.transform.position).normalized;
		float percentage = Map(CurrentTotalResistance, 0, _resistanceTolerance, 0, 1);
		float speed = _movementSpeed - (_movementSpeed * percentage);
		_rb.position += (dir * speed) * Time.deltaTime;
	}
	public void DeactivateEnemy()
	{
		IsActive = false;
		this.gameObject.SetActive(false);
	}

	public bool CheckIfNeedToBePushedBack()
	{
		return GetCurrentResistance() >= _resistanceTolerance;
	}



	public float Map(float value, float inMin, float inMax, float OutMin, float outMax)
	{
		return (value - inMin) * (outMax - OutMin) / (inMax - inMin) + OutMin;
	}



	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Hero"))
		{
			_totalResistance += collision.GetComponent<HeroController>().Resistance;
		}
	}
	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("Hero"))
		{
			_totalResistance -= collision.GetComponent<HeroController>().Resistance;
			if (_totalResistance < 0)
				_totalResistance = 0;
		}
	}

}
