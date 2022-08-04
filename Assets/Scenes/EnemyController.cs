using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float _movementSpeed;
    [SerializeField] float _resistance;


	Rigidbody2D _rb;
	List<Transform> _path = new List<Transform>();
	Vector2 dir;

	public bool IsActive;


	private void Awake()
	{
		_rb = GetComponent<Rigidbody2D>();
	}
	//private void Start()
	//{
	//	IsActive = true;
	//	_path = PathManager.GetInstance.GetEnemyRandomizedPath();
	//	dir = (_path[0].position - this.transform.position).normalized;
	//}

	public void Init()
	{
		IsActive = true;
		_path = PathManager.GetInstance.GetEnemyRandomizedPath();
		dir = (_path[0].position - this.transform.position).normalized;
	}

	private void FixedUpdate()
	{
		if (_path.Count == 0)
			return;
		float distance = Vector2.Distance(this.transform.position, _path[0].position);
		if(distance <= 0.2f)
		{
			_path.RemoveAt(0);

			if (_path.Count == 0)
			{
				_rb.velocity = Vector2.zero;
				IsActive = false;
				this.gameObject.SetActive(false);
			}
			else
				dir = (_path[0].position - this.transform.position).normalized;
		}
		else
		{
			_rb.velocity = (dir * _movementSpeed) * Time.fixedDeltaTime;
		}



	}


}
