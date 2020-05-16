using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Zone_controller : MonoBehaviour
{
	public string next_scene;
	public GameObject next = null;
	
	private Enemy enemy;
	private bool defeated = false;

	void Awake(){
		enemy = GameObject.FindWithTag("Enemy").GetComponent<Enemy>();
	}

	public IEnumerator LoadScene(){
		next.SetActive(true);
		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(next_scene);

		asyncLoad.allowSceneActivation = false;
		while(true){
			if(Input.GetKeyDown(KeyCode.Z)){
				asyncLoad.allowSceneActivation = true;
				break;
			}
			yield return null;
		}
	}

	void Update(){
		if(defeated) return;

		if(enemy.Get_phase() == 0){
			StartCoroutine("LoadScene");
			defeated = true;
			return;
		}

	}
}
