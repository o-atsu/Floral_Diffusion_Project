using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defeated_Effect : MonoBehaviour
{
    private GameObject camera = null;
	private float mag = 0.3f;
	private float duration = 0.6f;

	void OnEnable(){
		camera = GameObject.Find("Main Camera");
		StartCoroutine("Cam_quake");
	}

	private IEnumerator Cam_quake(){
		float time = 0;
		Vector3 pos = camera.transform.position;
		while(time < duration){
			float x = pos.x + Random.Range(-1f, 1f) * mag;
			float y = pos.y + Random.Range(-1f, 1f) * mag;

			Vector3 temp = new Vector3(x, y, pos.z);
			camera.transform.position = (temp);
			time += Time.deltaTime;
			yield return null;
		}
		camera.transform.position = pos;
		yield return new WaitForSeconds(1f);
		gameObject.SetActive(false);
	}
}
