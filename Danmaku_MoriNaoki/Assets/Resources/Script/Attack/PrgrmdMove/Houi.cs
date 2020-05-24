using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Houi : Attack
{
    [SerializeField]
	private Houi_generator generator;
	[SerializeField]
	private float radius = 3f;
	[SerializeField]
	private int num = 1;
	[SerializeField]
	private bool player_houi = true;

	private GameObject player;

	void OnEnable(){
		player = GameObject.FindWithTag("Player");
	}

	protected override IEnumerator shot(){
		yield return new WaitForSeconds(1f);

		while(true){
			if(player_houi){
				for(int i = 0;i < num;i++){
					generator.SetStatus(player.transform.position, radius, num, i);
					generator.Generate();
				}
			}else{
				for(int i = 0;i <= num;i++){
					generator.SetStatus(radius, num, i);
					generator.Generate();
				}
			}
			yield return new WaitForSeconds(interval);
		}
	}

	void Start(){
		StartCoroutine("shot");
	}
}
