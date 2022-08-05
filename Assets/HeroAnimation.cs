using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAnimation : MonoBehaviour
{
    [SerializeField]Animator _animator;

    public void SetAnimatorBool(string name,bool condition)
	{
		_animator.SetBool(name, condition);
	}



}
