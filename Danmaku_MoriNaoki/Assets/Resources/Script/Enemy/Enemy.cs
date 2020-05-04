using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Enemy : MonoBehaviour
{
	public int hp;
	[SerializeField]
	private Attack[] attacks;

	/**** 倒されるときの処理 ****/
	public virtual void Defeated(){
		Destroy(gameObject);
	} 

	/**** 動きのコルーチン ****/
	protected abstract IEnumerator move();
	
	void OnEnable(){
		StartCoroutine("move");
	}

}
