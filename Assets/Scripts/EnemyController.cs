using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
	[SerializeField] float _movementSpeed;
	[SerializeField] float _resistance;

	public float CurrentTotalResistance;
	[SerializeField]float _totalResistance;


	bool _moveTowardGoal => CurrentTotalResistance < _resistance;

	Rigidbody2D _rb;
	List<Transform> _path = new List<Transform>();
	Vector2 dir;

	public bool IsActive;
	Vector2 _portalPos;
	bool startGoBack;


	public List<Transform> HeroPos = new List<Transform>();
	public List<bool> IsPosTaken = new List<bool>();
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

	public void Init(Vector2 portalPos)
	{
		_portalPos = portalPos;
		IsActive = true;
		_path = PathManager.GetInstance.GetEnemyRandomizedPath();
		dir = (_path[0].position - this.transform.position).normalized;
	}

	private void Update()
	{
		if (_path.Count == 0)
			return;

		CurrentTotalResistance = Mathf.Clamp(_totalResistance, 0, _resistance * 2);
		float distance = Vector2.Distance(this.transform.position, _path[0].position);
		if (distance <= 0.2f)
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
			if (_moveTowardGoal)
			{
				if (CurrentTotalResistance == 0)
				{
					//dir = (_path[0].position - this.transform.position).normalized;
					_rb.position += (dir * _movementSpeed) * Time.deltaTime;
				}
				else
				{
					float percentage = Map(CurrentTotalResistance, 0, _resistance, 0, 1);
					if (percentage <= 1)
					{
						float speed = _movementSpeed - (_movementSpeed * percentage);
						//dir = (_path[0].position - this.transform.position).normalized;
						_rb.position += (dir * speed) * Time.deltaTime;


						if (startGoBack)
						{
							dir = (_path[0].position - this.transform.position).normalized;
							startGoBack = false;
						}

					}
					else
					{

					}
				}
			}
			else
			{
				if (!startGoBack)
				{
					startGoBack = true;
					dir = (_portalPos - (Vector2)this.transform.position).normalized;
				}
				float percentage =  Mathf.Clamp( Map(CurrentTotalResistance, 0, _resistance, 0, 1) - 1,0,1);
				float Speed = (percentage * _movementSpeed);
				Debug.Log("Speed: " + Speed + " percentage: " + percentage);
				dir = (this.transform.position - _path[0].position).normalized;
				_rb.position += (dir * Speed) * Time.deltaTime;

				float distanceToPortal = Vector2.Distance(this.transform.position, _portalPos);
				if (distanceToPortal <= 0.2f)
				{
					_rb.velocity = Vector2.zero;
					IsActive = false;
					this.gameObject.SetActive(false);
			
				}
			}
		}



	}


	public Transform GiveHeroPosition()
	{
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
