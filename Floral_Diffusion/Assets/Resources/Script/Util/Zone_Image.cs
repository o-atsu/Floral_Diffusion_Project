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

	// ゾーンのText
    private Text zoneText;

	void Start(){
		// ゾーンのText
        zoneText = GameObject.Find("Zone").GetComponent<Text>();

		img = GetComponent<Image>();
		Img_change();
		SceneManager.activeSceneChanged += SceneChanged;
	}

    private void OnDestroy()
    {
        SceneManager.activeSceneChanged -= SceneChanged;
    }

    void Img_change(){
		string sname = SceneManager.GetActiveScene().name;
		switch(sname){
			case "Zone_A":
				img.sprite = Zone_A;
				zoneText.text = "A";
				break;
			case "Zone_B":
				img.sprite = Zone_B;
				zoneText.text = "B";
				break;
			case "Zone_C":
				img.sprite = Zone_C;
				zoneText.text = "C";
				break;
			case "Zone_D":
				img.sprite = Zone_D;
				zoneText.text = "D";
				break;
			case "Zone_E":
				img.sprite = Zone_E;
				zoneText.text = "E";
				break;
			case "Result":
				img.sprite = null;
				zoneText.text = "RESULT";
				break;
		}
	}
	
	void SceneChanged(Scene thisScene, Scene nextScene){
		Img_change();
	}
}
