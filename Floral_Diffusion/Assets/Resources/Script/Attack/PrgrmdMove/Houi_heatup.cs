using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Houi_heatup : Houi
{
	private Enemy enemy;

	void Awake(){
		enemy = GameObject.FindWithTag("Enemy").GetComponent<Enemy>();
	}

    void FixedUpdate(){
		if(enemy.Get_percent() < 50f){
			interval = 0.3f;
		}
	}
}
