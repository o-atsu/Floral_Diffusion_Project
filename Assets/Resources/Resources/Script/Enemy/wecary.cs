using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wecary : Enemy
{
	private Rigidbody2D rb;

	void Awake(){
		rb = GetComponent<Rigidbody2D>();
	}

	protected override void Defeated(){
		Debug.Log("wecary:Defeated!");
	}

	protected override IEnumerator move(){
		while(true){
			rb.velocity = new Vector3(2f, 0f, 0f);
			yield return new WaitForSeconds(3f);
			rb.velocity = new Vector3(-2f, 0f, 0f);
			yield return new WaitForSeconds(3f);
		}
	}

}
