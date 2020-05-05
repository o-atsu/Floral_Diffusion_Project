using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Enemy : MonoBehaviour
{
	public int hp;

	/**** 倒されるときの処理 ****/
	public virtual void Defeated(){
		gameObject.SetActive(false);
	} 

	/**** 動きのコルーチン ****/
	protected virtual IEnumerator move(){yield return null;}
	
	void OnEnable(){
		StartCoroutine("move");
	}

}
