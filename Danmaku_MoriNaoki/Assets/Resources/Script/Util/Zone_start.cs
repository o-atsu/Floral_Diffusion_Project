using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Zone_start : MonoBehaviour
{
	private Text text_comp;
	private Animator anim;

	// プレイヤーコントロールのコンポーネント
    // private Player_controll pc;

    private static Dictionary<string, string> enemys = new Dictionary<string, string>(){// 各ゾーンボスの名前
		{"Zone_A", "Fuka"},
		{"Zone_B", "Eno"},
		{"Zone_D", "Mimo"},
		{"Zone_E", "Liche"}
	};

	void Start(){
		text_comp = GetComponent<Text>();
		anim = GetComponent<Animator>();
		SceneManager.activeSceneChanged += SceneChanged;
	}

	/*

	// Start is called before the first frame update
	void Start(){

		// プレイヤーコントロールのコンポーネントを取得
        // pc = GameObject.FindWithTag("Player").GetComponent<Player_controll>();
		// Debug.Log(pc);

	}

	*/

	void SceneChanged(Scene thisScene, Scene nextScene){
		string sname = SceneManager.GetActiveScene().name;
		if(sname!="Title"&&sname!="Result"){
			// pc.SetActionFlag(true);
			StartCoroutine("pop");
		}
	}

	private IEnumerator pop(){
		anim.SetBool("started", true);
		string sname = SceneManager.GetActiveScene().name;
		text_comp.text = sname;
		yield return new WaitForSeconds(0.7f);
		text_comp.text = "vs " + enemys[sname];
		yield return new WaitForSeconds(0.7f);
		text_comp.text = "Shot!";
		yield return new WaitForSeconds(0.6f);
		text_comp.text = "";
		anim.SetBool("started", false);
	}
}
