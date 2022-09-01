﻿
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

	protected AudioSource audiosource;

	public Object_pool pool;

	/**** Bulletの初期化 ****/
	protected abstract void Bullet_init(GameObject obj);

	void Awake(){
        
        if (pool == null) pool = GetComponent<Object_pool>();
	}

    private void Start()
    {
        if (GetComponent<AudioSource>())
        {
            audiosource = GetComponent<AudioSource>();
        }
    }

    public void Generate(){//弾の生成
		GameObject tmp = pool.Get_obj();
		tmp.SetActive(true);
		Bullet_init(tmp);
		if(audiosource != null){
			audiosource.Play();
		}
	}

    public void Set_Direct(float dir)
    {
        direction = dir;
    }

    public void SetPosition(Vector2 v2)
    {
        position = v2;
    }

    

}
