using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Barrage_generator : MonoBehaviour
{
	[SerializeField]
	protected GameObject bullet;
	[SerializeField]
	protected Vector2 position = new Vector2(0f, 0f);
	[SerializeField]
	protected float direction = 0f;
	[SerializeField]
	protected float speed = 1f;
	[SerializeField]
	private int POOL_SIZE = 50;//初期生成するオブジェクト数

	/**** Bulletの初期化 ****/
	protected abstract void Bullet_init(ref GameObject obj);


	private Queue<GameObject> pool = new Queue<GameObject>();
	
	~Barrage_generator(){
		pool.Clear();
	}
	

	private void Create_pool(){//プールの生成
		for(int i = 0;i < POOL_SIZE;i++){
			GameObject tmp = Instantiate(bullet, new Vector2(0f, 0f), Quaternion.identity, transform);
			tmp.SetActive(false);
		}
	} 

	protected void Awake(){
		Create_pool();
	}


	public void Generate(){//弾の生成
		GameObject tmp;
		if(pool.Count > 0){
			tmp = pool.Dequeue();
			tmp.SetActive(true);
			Bullet_init(ref tmp);
		}else{
			tmp = Instantiate(bullet, new Vector2(0f, 0f), Quaternion.identity, transform);
			Bullet_init(ref tmp);
		}
	}

	public void Ret_pool(GameObject obj){
		pool.Enqueue(obj);
	}
}
