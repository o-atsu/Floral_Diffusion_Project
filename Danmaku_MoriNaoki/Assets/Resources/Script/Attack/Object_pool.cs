using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_pool : MonoBehaviour
{
	[SerializeField]
	private int POOL_SIZE = 50;//初期生成するオブジェクト数
	[SerializeField]
	private GameObject bullet;

	private Queue<GameObject> pool = new Queue<GameObject>();
	
	~Object_pool(){
		pool.Clear();
	}
	

	public void Create_pool(){//プールの生成
		for(int i = 0;i < POOL_SIZE;i++){
			GameObject tmp = Instantiate(bullet, new Vector2(0f, 0f), Quaternion.identity, transform);
			tmp.SetActive(false);
		}
	} 

	protected void Awake(){
		Create_pool();
	}

	public GameObject Get_obj(){
		GameObject tmp;
		if(0 < pool.Count){
			tmp = pool.Dequeue();
			tmp.SetActive(true);
		}
		else{
			tmp = Instantiate(bullet, new Vector2(0f, 0f), Quaternion.identity, transform);
		}
		return tmp;
	}
	public void Ret_pool(GameObject obj){
		pool.Enqueue(obj);
	}


    public void AllBulletSetActiveFalse()
    {
        foreach (Transform child in transform)
        {
            if(child.gameObject.activeInHierarchy==true) child.gameObject.SetActive(false);
        }
    }

}
