using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Enemy : MonoBehaviour
{
	[SerializeField]
	protected int MAX_HP;
	[SerializeField]
	protected int scorePerPhase;
	[SerializeField]
	protected int phase;
	[SerializeField]
	protected GameObject on_defeated = null;

	protected int hp;

	/**** 倒されるときの処理 ****/
	protected virtual void Defeated(){
		EndPhase.WriteGrade(scorePerPhase);
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
	void OnDisable(){
		if(on_defeated != null){
			Instantiate(on_defeated, transform.position, Quaternion.identity);
		}
	}

	public int Get_MAX_HP(){
		return MAX_HP;
	}

	public void Hit(int damage){
		Score.AddScore(damage); // 与えたダメージ分スコアを増加させる
		hp -= damage;
		if(hp <= 0){
			Score.AddScore(hp); // オーバーキルしたときの過剰なスコア増加分を削る
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
