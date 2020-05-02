using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Enemy : MonoBehaviour
{
	[SerializeField]
	private int hp;
	[SerializeField]
	private Attack[] attacks;

	/**** 倒されるときの処理 ****/
	protected abstract void Defeated(); 

	/**** 動きのコルーチン ****/
	protected abstract IEnumerator move();
	
	//弾が当たった時にHPを減らす、HPが0以下なら倒される
	private void Hit(){
		Debug.Log("Hit");

		hp--;
		if(hp <= 0){
			Defeated();
		}
	}
	
	void OnTriggerEnter2D(Collider2D other){
		Hit();
	}

	void OnEnable(){
		StartCoroutine("move");
	}

}
