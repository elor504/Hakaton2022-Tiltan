using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    public LayerMask EnemyMask;
    public float DetectionRadius;

    public Transform EnemyTrans;
	public float movementSpeed;
	public float Resistance;

	bool _startGoingToEnemy;
	Vector2 dir;
	Rigidbody2D rb;
	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
	}
	private void Update()
	{
		if(EnemyTrans == null)
		{
			if (Physics2D.OverlapCircle(this.transform.position, DetectionRadius, EnemyMask) != null)
			{
				EnemyTrans = Physics2D.OverlapCircle(this.transform.position, DetectionRadius, EnemyMask).transform;
				if (_startGoingToEnemy)
				{
					_startGoingToEnemy = false;
					rb.velocity = Vector2.zero;
				}
			}
			return;
		}
		else
		{
			if (!_startGoingToEnemy)
			{
				_startGoingToEnemy = true;

			}
		}

		if(!EnemyTrans.gameObject.activeInHierarchy)
		{
			EnemyTrans = null;
			return;
		}

		if (_startGoingToEnemy)
		{
			dir = (EnemyTrans.transform.position - transform.position).normalized;
			rb.position += (dir * movementSpeed) * Time.deltaTime;
		}

	}
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(this.transform.position, DetectionRadius);

	}


}
