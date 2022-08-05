using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
	public int ID;
	public int Points;

	[SerializeField] float _movementSpeed;
	[SerializeField] float _resistanceTolerance;

	public float CurrentTotalResistance;
	[SerializeField]float _totalResistance;

	[SerializeField]Rigidbody2D _rb;
	Vector2 dir;

	public bool IsActive;
	Portal _portalPos;
	public Vector2 GetPortalPos => _portalPos.transform.position;

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

	public float GetTotalResistancePercentage()
	{
		float per = 0;
		per = Map(GetCurrentResistance(), 0, _resistanceTolerance, 0, 1);
		return per;
	}



	private void Awake()
	{
		_rb = GetComponent<Rigidbody2D>();
	}

	public void Init(Portal portalPos)
	{
		_portalPos = portalPos;
		_portalPos.ActivateSpawnVFX();
		IsActive = true;
		for (int i = 0; i < HeroPos.Count; i++)
		{
			HeroPos[i].Clear();
		}
	}
	public void GoTowardPosition(Vector2 pos)
	{
		dir = (pos - (Vector2)this.transform.position).normalized;

		float percentage = Map(GetCurrentResistance(), 0, _resistanceTolerance, 0, 1);
		float speed = _movementSpeed - (_movementSpeed * percentage);
		//Debug.Log("Percentage: " + percentage + " movementSpeed: " + speed);
		speed = Mathf.Clamp(speed, 0, _movementSpeed);
		//float speed = _movementSpeed /2;
		_rb.position += (dir * speed) * Time.deltaTime;
	}
	public void GoTowardPortal()
	{
		dir = ((Vector2)_portalPos.transform.position - (Vector2)this.transform.position).normalized;
		float percentage = Map(GetCurrentResistance(), 0, _resistanceTolerance, 0, 1);

		float speed = _movementSpeed - (_movementSpeed / percentage);
		speed = Mathf.Clamp(speed, 0, _movementSpeed);
		_rb.position += (dir * speed) * Time.deltaTime;
	}
	public void DeactivateEnemy()
	{
		GameManager.GetInstance.AddPoints(Points);
		SoundManager.getInstance.PlayDespawnMonsterSfx();
		IsActive = false;
		_portalPos.ActivateDespawnVFX();

		this.gameObject.SetActive(false);
		for (int i = 0; i < HeroPos.Count; i++)
		{
			HeroPos[i].Clear();
		}
	
	}
	public bool CheckIfNeedToBePushedBack()
	{
		return GetCurrentResistance() >= _resistanceTolerance;
	}

	public bool CanBeTargeted()
	{
		for (int i = 0; i < HeroPos.Count; i++)
		{
			if (!HeroPos[i].IsBeingTargetedByHero)
				return true;
		}
		return false;
	}
	public PushPos GetAvailablePushPos()
	{
		for (int i = 0; i < HeroPos.Count; i++)
		{
			if (!HeroPos[i].IsBeingTargetedByHero)
			{
				HeroPos[i].SetBeingTarget(true);
				return HeroPos[i];
			}
		}
		return null;
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
