﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mimo : Enemy
{
	private Rigidbody2D rb;
	private Animator anim;

	void Awake(){
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
	}

	protected override IEnumerator Defeated(){
		if(phase == 0) yield break;

		attack_each_phase[attack_each_phase.Length - phase].SetActive(false);
		phase--;
		if(phase == 0){
			on_defeated.transform.parent = null;
			on_defeated.SetActive(true);
			yield return new WaitForSeconds(0.2f);
			gameObject.SetActive(false);
			Debug.Log("Mimo:Defeated!");
			yield break;
		}
		hp = MAX_HP;
		attack_each_phase[attack_each_phase.Length - phase].SetActive(true);
		if(phase == 2 || phase == 1) yield return StartCoroutine("stay");
		else attack_each_phase[attack_each_phase.Length - phase].SetActive(true);
	}

	private IEnumerator stay(){
		if(phase == 2) StopCoroutine("move");
		if(phase == 1) {
			StopCoroutine("kabedon");
			StartCoroutine("cut_in");
		}
		rb.velocity = new Vector3(0f, 0f, 0f);
		float tmp = 0;
		while(tmp < 1){
			transform.position = Vector3.Slerp(transform.position, new Vector3(-1.81f, 2.62f, 0f), tmp);
			tmp += 0.3f * Time.deltaTime;
			yield return null;
		}
		if(phase == 2){
		StartCoroutine("kabedon");
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
		float time;
		while(true){
			time = 0f;
			while(time < 1.15f){//RIGHT
				rb.velocity = new Vector3(6.35f * time, 0f, 0f);
				time += Time.deltaTime;
				yield return null;
			}
			rb.velocity = new Vector3(-3.1f, 0f, 0f);
			yield return new WaitForSeconds(2.45f);

			time = 0f;
			while(time < 1.15f){//LEFT
				rb.velocity = new Vector3(-6.35f * time, 0f, 0f);
				time += Time.deltaTime;
				yield return null;
			}
			rb.velocity = new Vector3(3.1f, 0f, 0f);
			yield return new WaitForSeconds(2.45f);

			time = 0f;
			while(time < 0.75f){//TOP
				rb.velocity = new Vector3(0f, 6.4f * time, 0f);
				time += Time.deltaTime;
				yield return null;
			}
			rb.velocity = new Vector3(0f, -1.5f, 0f);
			yield return new WaitForSeconds(2.2f);
		}
	}

	void FixedUpdate(){
		anim.SetFloat("velocity_x", rb.velocity.x);

	}

}
