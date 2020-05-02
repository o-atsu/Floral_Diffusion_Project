using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Straight_1way : Attack
{
	protected override IEnumerator shot(){
		while(true){
			generators[0].Generate();
			yield return new WaitForSeconds(interval);
		}
	}

	void Start(){
		StartCoroutine("shot");
	} 
}
