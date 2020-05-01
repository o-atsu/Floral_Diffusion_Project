using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrage_generator : MonoBehaviour
{
	private float speed = 1f;
	private float interval = 1f;
	[SerializeField]
	private GameObject bullet;


	protected IEnumerator Shoot(){
		while(true){
			GameObject tmp = Instantiate(bullet, new Vector2(0f, 0f), Quaternion.identity);
			tmp.GetComponent<Rigidbody2D>().velocity = tmp.transform.up * speed;
			yield return new WaitForSeconds(interval);
		}
	}

	protected void OnEnable(){
		StartCoroutine("Shoot");
	}
	
		
}
