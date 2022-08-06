using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PopUpPoints : MonoBehaviour
{
	public TextMeshProUGUI amountText;
	public ParticleSystem vfx;
	public Animator animator;

	public bool IsActive;

	public void Init(string amount,Vector2 worldPos)
	{
		IsActive = true;
		amountText.text = "+" + amount;
		vfx.Play();
		animator.SetTrigger("Play");
		this.GetComponent<RectTransform>().position = new Vector3(worldPos.x,worldPos.y + 0.5f,0);
		
	}


	public void OnAnimatorSetActiveFalse()
	{
		IsActive = false;
	}

}
