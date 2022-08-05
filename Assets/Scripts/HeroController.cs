using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour
{
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


	public void InitHero(Vector2 pos)
	{
		this.transform.position = pos;
		IsActive = true;
		this.gameObject.SetActive(true);
	}
	public void GoTowardPosition(Vector2 pos)
	{
		dir = (pos - (Vector2)this.transform.position).normalized;
		_rb.position += (dir * movementSpeed) * Time.deltaTime;
	}
	//
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
