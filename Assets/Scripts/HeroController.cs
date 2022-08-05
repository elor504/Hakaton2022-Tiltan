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
	public float movementSpeed;
	public float Resistance;

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
		_rb.position += (dir * movementSpeed) * Time.deltaTime;

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
