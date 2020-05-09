using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuka : Enemy
{
	private Rigidbody2D rb;
	private Animator anim;

	public GameObject[] attack_each_phase;

	void Awake(){
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
	}

	protected override void Defeated(){
		attack_each_phase[attack_each_phase.Length - phase].SetActive(false);
		phase--;
		if(phase == 0){
			gameObject.SetActive(false);
			Debug.Log("Fuka:Defeated!");
			return;
		}
		hp = MAX_HP;
		attack_each_phase[attack_each_phase.Length - phase].SetActive(true);
		StopCoroutine("move");
		transform.position = new Vector3(-2.01f, 2.62f, 0f);
	}

	protected override IEnumerator move(){
		while(true){
			rb.velocity = new Vector3(2f, -0.6f, 0f);
			yield return new WaitForSeconds(3f);
			rb.velocity = new Vector3(-2f, 0.6f, 0f);
			yield return new WaitForSeconds(3f);
		}
	}

	void FixedUpdate(){
		anim.SetFloat("velocity_x", rb.velocity.x);
	}
}
