﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	//画面のそれぞれの端、この外に出ると自身を消す
	protected float LEFT = -7.44f;
	protected float RIGHT = 3.87f;
	protected float TOP = 6.12f;
	protected float BOTTOM = -6.12f;

	/**** 弾生成時の初期化 ****/
	public virtual void Set_property(Vector2 position,float direction,float speed){
		return;
	}

	/**** 派生クラスで軌道を実装してください ****/


	void OnEnable(){
		StartCoroutine("check_in_screen");
	}

	void OnDisable(){
		transform.parent.GetComponent<Object_pool>().Ret_pool(this.gameObject);
	}

	//画面外かどうか判定
	protected IEnumerator check_in_screen(){
		while(true){
			yield return null;
			Vector2 pos = new Vector2(transform.position.x, transform.position.y);
			if(pos.x < LEFT || RIGHT < pos.x || pos.y < BOTTOM || TOP < pos.y){
                break;
            }
		}


        gameObject.SetActive(false);
        yield break;
    }

	void OnTriggerEnter2D(Collider2D other){
        if (this.gameObject.tag == "Enemy_Bullet")
        {
            if (other.gameObject.tag == "Player")
            {
                this.gameObject.SetActive(false);
            }
        }
	}
}
