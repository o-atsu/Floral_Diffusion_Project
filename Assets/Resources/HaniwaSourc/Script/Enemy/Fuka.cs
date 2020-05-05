using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuka : Enemy
{
	private Rigidbody2D rb;
	private SpriteRenderer sr;
	[SerializeField]
	private Sprite left;
	[SerializeField]
	private Sprite right;

	void Awake(){
		rb = GetComponent<Rigidbody2D>();
		sr = GetComponent<SpriteRenderer>();
	}

	public override void Defeated(){
		gameObject.SetActive(false);
		Debug.Log("Fuka:Defeated!");
	}

	protected override IEnumerator move(){
		while(true){
			rb.velocity = new Vector3(2f, -0.6f, 0f);
			yield return new WaitForSeconds(3f);
			rb.velocity = new Vector3(-2f, 0.6f, 0f);
			yield return new WaitForSeconds(3f);
		}
	}

}
