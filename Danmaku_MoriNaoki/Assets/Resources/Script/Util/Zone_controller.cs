using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Zone_controller : MonoBehaviour
{
	public string next_scene;
	
	private Enemy enemy;
	private bool defeated = false;
	private Text nextt;
	private Text quitt;

	void Start(){
		enemy = GameObject.FindWithTag("Enemy").GetComponent<Enemy>();
		nextt = GameObject.Find("Next_Zone").GetComponent<Text>();
		quitt = GameObject.Find("Quit").GetComponent<Text>();
	}

	public IEnumerator LoadScene(){
		nextt.text = "Press Z to the Next Zone";
		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(next_scene);

		asyncLoad.allowSceneActivation = false;
		while(true){
			if(Input.GetKeyDown(KeyCode.Z)){
				nextt.text = "";
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
		float time = 0;
		while(time < 3f){
			int text_time = (int)(4 - time);
			if(!Input.GetKey(KeyCode.Q)){
				quitt.text = "";
				break;
			}else{
				time += Time.deltaTime;
				quitt.text = "Quit to Title:" + text_time.ToString();
				
				yield return null;
			}
		}
		if(time >= 3f) SceneManager.LoadScene("Title");
	}
}
