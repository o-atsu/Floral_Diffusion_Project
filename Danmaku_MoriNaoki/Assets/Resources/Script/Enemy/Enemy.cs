using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Enemy : MonoBehaviour
{
	[SerializeField]
	protected int MAX_HP;
	[SerializeField]
	protected int phase;

	protected int hp;

	/**** 倒されるときの処理 ****/
	protected virtual void Defeated(){
		phase--;
		if(phase == 0){
			gameObject.SetActive(false);
			return;
		}
		hp = MAX_HP;
	} 

	/**** 動きのコルーチン ****/
	protected virtual IEnumerator move(){yield return null;}
	
	void OnEnable(){
		StartCoroutine("move");
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

	public float Get_percent(){
		return 100f * hp / MAX_HP;
	}
	public int Get_phase(){
		return phase;
	}

}
