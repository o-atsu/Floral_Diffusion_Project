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
		StartCoroutine("start_wait");
	}

	protected override void Defeated(){
		if(phase == 0) return;

		attack_each_phase[attack_each_phase.Length - phase].SetActive(false);
		phase--;
		if(phase == 0){
			gameObject.SetActive(false);
			Debug.Log("Fuka:Defeated!");
			return;
		}
		hp = MAX_HP;
		StopCoroutine("move");
		StartCoroutine("phase_change");
	}

	protected override IEnumerator move(){
		while(true){
			rb.velocity = new Vector3(2f, -0.6f, 0f);
			yield return new WaitForSeconds(3f);
			rb.velocity = new Vector3(-2f, 0.6f, 0f);
			yield return new WaitForSeconds(3f);
		}
	}
	private IEnumerator phase_change(){
		rb.velocity = new Vector3(0f, 0f, 0f);
		float tmp = 0;
		while(tmp < 1){
			transform.position = Vector3.Slerp(transform.position, new Vector3(-2.01f, 2.62f, 0f), tmp);
			tmp += 0.4f * Time.deltaTime;
			yield return null;
		}
		attack_each_phase[attack_each_phase.Length - phase].SetActive(true);
	}

	void FixedUpdate(){
		anim.SetFloat("velocity_x", rb.velocity.x);
	}

	IEnumerator start_wait(){
		yield return new WaitForSeconds(0.8f);
		attack_each_phase[0].SetActive(true);
	}
}
