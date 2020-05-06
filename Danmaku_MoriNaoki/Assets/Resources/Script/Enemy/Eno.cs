using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eno : Enemy
{
	private Rigidbody2D rb;
	private Animator anim;

	void Awake(){
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
	}

	protected override void Defeated(){
		phase--;
		if(phase == 0){
			gameObject.SetActive(false);
			Debug.Log("Eno:Defeated!");
			return;
		}
		hp = MAX_HP;
	}

	protected override IEnumerator move(){
		while(true){
			rb.velocity = new Vector3(2f, -0.6f, 0f);
			yield return new WaitForSeconds(3f);
			rb.velocity = new Vector3(-2f, 0.6f, 0f);
			yield return new WaitForSeconds(3f);
		}
	}

	void Update(){
		anim.SetFloat("velocity_x", rb.velocity.x);
	}
}
