using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour
{
	public HeroAnimation heroAnimation;
	public LayerMask EnemyMask;
	public float DetectionRadius;
	public HeroBrain Brain;
	public Transform EnemyTrans;
	public float MovementSpeed;
	public float Resistance;

	float _movementSpeed()
	{
		if (GameManager.GetInstance.IsHeroesBoosted)
		{
			return MovementSpeed + (MovementSpeed * GameManager.GetInstance.MovementPercentage)
				+ (MovementSpeed * UpgradeManager.GetInstance.GetMovementPercentage());
		}
		return MovementSpeed + (MovementSpeed * UpgradeManager.GetInstance.GetMovementPercentage());
	}
	public float GetMovementSpeed => _movementSpeed();
	float _resistance()
	{
		if (GameManager.GetInstance.IsHeroesBoosted)
		{
			return Resistance + (Resistance * GameManager.GetInstance.ResistancePercentage)
				+ (Resistance * UpgradeManager.GetInstance.GetResistancePercentage());
		}
		return Resistance + (Resistance * UpgradeManager.GetInstance.GetResistancePercentage());
	}
	public float GetResistance => _resistance();

	Vector2 dir;
	Rigidbody2D _rb;

	public bool IsActive;

	private void Awake()
	{
		_rb = GetComponent<Rigidbody2D>();
	}

	public bool respawn;

	public void InitHero(Vector2 pos)
	{
		this.transform.position = pos;
		IsActive = true;
		this.gameObject.SetActive(true);
		respawn = true;
	}
	public void GoTowardPosition(Vector2 pos)
	{
		dir = (pos - (Vector2)this.transform.position).normalized;
		_rb.position += (dir * GetMovementSpeed) * Time.deltaTime;

		if (respawn)
		{
			float distance = Vector2.Distance(transform.position, pos);
			if (distance <= 0.2f)
				respawn = false;
		}

	}
	public void ReleaseHero()
	{
		Brain.PushPos = null;
	}
	public void DisableHero()
	{
		IsActive = false;
		this.gameObject.SetActive(false);
	}
	public bool IsHeroAttacking()
	{
		return Brain.IsHeroAttacking();
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(this.transform.position, DetectionRadius);

	}
}
