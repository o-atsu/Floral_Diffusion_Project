using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Houi_generator : Barrage_generator
{
	protected override void Bullet_init(GameObject obj){
		obj.GetComponent<Bullet>().Set_property(position, direction, speed);
	}

	public void SetStatus(Vector3 pos, float rad, int num, int i){
		float d = (360f / num * i);
		float drad = d * Mathf.Deg2Rad;
		position = new Vector2(-transform.position.x + pos.x + rad*Mathf.Cos(drad),-transform.position.y + pos.y + rad*Mathf.Sin(drad));
		direction = d + 180f;
	}
	public void SetStatus(float rad, int num, int i){
		float d = (360f / num * i);
		float drad = d * Mathf.Deg2Rad;
		position += new Vector2(-transform.position.x + rad*Mathf.Cos(drad), -transform.position.y + rad*Mathf.Sin(drad));
		direction = d + 180f;
	}
}
