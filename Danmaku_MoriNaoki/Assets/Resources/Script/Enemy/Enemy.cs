using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Enemy : MonoBehaviour
{
	[SerializeField]
	private int MAX_HP;
	[SerializeField]
	private int phase;

	private int hp;

	/**** 倒されるときの処理 ****/
	protected virtual void Defeated(){
		gameObject.SetActive(false);
	} 

	/**** 動きのコルーチン ****/
	protected virtual IEnumerator move(){yield return null;}
	
	void OnEnable(){
		StartCoroutine("move");
	}

	void Awake(){
		hp = MAX_HP;
	}

	public int Get_MAX_HP(){
		return MAX_HP;
	}

	public void Hit(int damage){
		hp -= damage;
		if(hp <= 0){
			Defeated();
		}
	}

}
