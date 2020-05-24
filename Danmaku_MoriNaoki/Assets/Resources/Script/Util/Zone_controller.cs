using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Zone_controller : MonoBehaviour
{
	public string next_scene;
	public GameObject next = null;
	public GameObject quit = null;
	
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

		if(Input.GetKeyDown(KeyCode.Q)) StartCoroutine("to_quit");
	}

	private IEnumerator to_quit(){
		quit.SetActive(true);
		float time = 0;
		while(time < 3f){
			int text_time = (int)(4 - time);
			if(!Input.GetKey(KeyCode.Q)){
				quit.SetActive(false);
				break;
			}else{
				time += Time.deltaTime;
				quit.GetComponent<Text>().text = "Quit to Title:" + text_time.ToString();
				
				yield return null;
			}
		}
		if(time >= 3f) SceneManager.LoadScene("Title");
	}
}
