using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Zone_Image : MonoBehaviour
{
	public Sprite Zone_A;
	public Sprite Zone_B;
	public Sprite Zone_C;
	public Sprite Zone_D;
	public Sprite Zone_E;

	private Image img;

	void Start(){
		img = GetComponent<Image>();
		Img_change();
		SceneManager.sceneLoaded += SceneLoaded;
	}

	void Img_change(){
		string sname = SceneManager.GetActiveScene().name;
		switch(sname){
			case "Zone_A":
				img.sprite = Zone_A;
				break;
			case "Zone_B":
				img.sprite = Zone_B;
				break;
			case "Zone_C":
				img.sprite = Zone_C;
				break;
			case "Zone_D":
				img.sprite = Zone_D;
				break;
			case "Zone_E":
				img.sprite = Zone_E;
				break;
		}
	}
	
	void SceneLoaded(Scene nextScene, LoadSceneMode mode){
		Img_change();
	}
}
