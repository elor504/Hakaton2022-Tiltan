using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
	[SerializeField] Animator _animator;


	public void SetAnimationBool(string id,bool condition)
	{
		_animator.SetBool(id, condition);
	}



}
