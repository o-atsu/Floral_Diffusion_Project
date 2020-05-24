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
        phase = 4;
        this.transform.position = new Vector3(-2f, 3f, 0f);
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
		while(phase==4){
			
		}

        StartCoroutine("move3");

        yield break;

	}

    private IEnumerator move3()
    {

        while (phase == 3)
        {

        }

        StartCoroutine("move3");

        yield break;
    }



    void Update(){
		anim.SetFloat("velocity_x", rb.velocity.x);
	}
}
