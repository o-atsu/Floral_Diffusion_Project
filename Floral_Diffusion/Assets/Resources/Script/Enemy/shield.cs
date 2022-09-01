using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Enemy
{
	protected float LEFT = -6.44f;
	protected float RIGHT = 2.87f;
	protected float TOP = 5.12f;
	protected float BOTTOM = -5.12f;
	
	public override void Hit(int damage){
		Score.AddScore(damage); // 与えたダメージ分スコアを増加させる
		hp -= damage;
		if(hp<=0&&phase>=1){
			Score.AddScore(hp); // オーバーキルしたときの過剰なスコア増加分を削る
			StartCoroutine("Defeated");
			phase = 1;
		}
	}

	void OnEnable(){
		on_defeated.transform.parent = gameObject.transform;
		StartCoroutine("check_in_screen");
	}

	protected override IEnumerator Defeated(){
		on_defeated.transform.parent = null;
		on_defeated.SetActive(true);
		gameObject.SetActive(false);
		yield return null;
	}

	protected override IEnumerator entry(){
		yield return null;
	}

	protected IEnumerator check_in_screen(){
	yield return new WaitForSeconds(0.1f);
		while(true){
			Vector2 pos = new Vector2(transform.position.x, transform.position.y);
			if(pos.x < LEFT || RIGHT < pos.x || pos.y < BOTTOM || TOP < pos.y){
                break;
            }
			yield return null;
		}

        StartCoroutine("Defeated");
		phase = 1;
        yield break;
    }

}
