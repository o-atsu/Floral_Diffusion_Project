using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

	/**** 弾生成時の初期化 ****/
	public virtual void Set_property(Vector2 position,float direction,float speed){
		return;
	}

	/**** 派生クラスで軌道を実装してください ****/


	void OnDisable(){
		transform.parent.GetComponent<Barrage_generator>().Ret_pool(this.gameObject);
	}

}
