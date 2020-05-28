using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Enemy
{
	public override void Hit(int damage){
		Score.AddScore(damage); // 与えたダメージ分スコアを増加させる
		hp -= damage;
		if(hp<=0&&phase>=1){
			Score.AddScore(hp); // オーバーキルしたときの過剰なスコア増加分を削る
			Defeated();
			phase = 1;
		}
	}

	protected override IEnumerator entry(){
		yield return null;
	}

}
