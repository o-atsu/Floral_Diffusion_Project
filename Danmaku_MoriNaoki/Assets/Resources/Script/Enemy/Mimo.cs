using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mimo : Enemy
{
	private Rigidbody2D rb;
	private Animator anim;

	public GameObject[] attack_each_phase;

	void Awake(){
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
	}

	protected override void Defeated(){
		if(phase == 0) return;

		attack_each_phase[attack_each_phase.Length - phase].SetActive(false);
		phase--;
		if(phase == 0){
			gameObject.SetActive(false);
			Debug.Log("Mimo:Defeated!");
			return;
		}
		hp = MAX_HP;
		attack_each_phase[attack_each_phase.Length - phase].SetActive(true);
		if(phase == 2) StartCoroutine("phase_change");
		else attack_each_phase[attack_each_phase.Length - phase].SetActive(true);
	}

	private IEnumerator phase_change(){
		StopCoroutine("move");
		rb.velocity = new Vector3(0f, 0f, 0f);
		float tmp = 0;
		while(tmp < 1){
			transform.position = Vector3.Slerp(transform.position, new Vector3(-2.01f, 2.62f, 0f), tmp);
			tmp += 0.3f * Time.deltaTime;
			yield return null;
		}
		attack_each_phase[attack_each_phase.Length - phase].SetActive(true);
	}

	protected override IEnumerator move(){
		while(true){
			int rev = 1;
			yield return new WaitForSeconds(5.6f);
			if(Random.Range(0, 2) == 1){
				rev *= -1;
			}
			rb.velocity = new Vector3(rev * 1.5f, 1.0f, 0f);
			yield return new WaitForSeconds(7.0f);
			rev *= -1;
			rb.velocity = new Vector3(rev * 1.5f, -1.0f, 0f);
		}
	}

	private IEnumerator kabedon(){
		rb.velocity = new Vector3(0f, 0f, 0f);
//		rb.velocity
	}

	void FixedUpdate(){
		anim.SetFloat("velocity_x", rb.velocity.x);

	}

}
