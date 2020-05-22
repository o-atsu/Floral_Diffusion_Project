using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class All_field_generator : Barrage_generator
{
	public enum FROM{
		TOP,
		BOTTOM,
		LEFT,
		RIGHT
	}
	public FROM From;

	[SerializeField]
	private float offset = 0;

    protected override void Bullet_init(GameObject obj){

		float rand = Random.Range(-6.0f, 6.0f);
		Vector2 pos = new Vector2(0f, 0f);
		switch(From){
			case FROM.TOP:
				pos = new Vector2(rand, position.y + offset - transform.position.y);
				break;
			case FROM.BOTTOM:
				pos = new Vector2(rand, position.y - offset - transform.position.y);
				break;
			case FROM.LEFT:
				pos = new Vector2(position.x + offset - transform.position.x, rand);
				break;
			case FROM.RIGHT:
				pos = new Vector2(position.x - offset - transform.position.x, rand);
				break;
		}
		obj.GetComponent<Bullet>().Set_property(pos, direction, speed);
	}
}
