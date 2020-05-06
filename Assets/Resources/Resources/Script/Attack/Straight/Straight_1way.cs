using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Straight_1way : Attack
{
	protected override IEnumerator shot(){
		while(true){
			foreach(Barrage_generator i in generators) i.Generate();
			yield return new WaitForSeconds(interval);
		}
	}

	void Start(){
		StartCoroutine("shot");
	} 
}
