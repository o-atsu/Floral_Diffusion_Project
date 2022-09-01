using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // 追加
using UnityEngine.SceneManagement; // 追加

public class Life : MonoBehaviour
{

    // ライフを表示するText
    private Text lifeText;

    // プレイヤーコントロールのコンポーネント
    private Player_controll pc;

    // ライフ
    public static int life;
    private string show;

    // 表示ライフ
    public static int showLife;

    private string sname;

    // Start is called before the first frame update
    void Start(){

        // ライフを表示するText
        this.lifeText = this.GetComponent<Text>();

        // ゾーンならプレイヤーコントロールのコンポーネントを取得してライフを取得する
        sname = SceneManager.GetActiveScene().name;
		if(sname!="Title"&&sname!="Result"){
			pc = GameObject.FindWithTag("Player").GetComponent<Player_controll>();
            life = pc.GetLifeCount();
		}

        // 表示を更新する
        show = "";
        if(life>=1){
            for(int i=1; i<=life; i++){
                show += "★";
            }
        }
        lifeText.text = show;
        showLife = life;

    }

    // Update is called once per frame
    void Update(){

        // ゾーンでないなら処理中断
        if(sname=="Title"||sname=="Result"){
            return;
        }

        // ライフを取得する
        life = pc.GetLifeCount();

        // 必要に応じて表示を更新する
        if(showLife!=life){
            show = "";
            if(life>=1){
                for(int i=1; i<=life; i++){
                    show += "★";
                }
            }
            lifeText.text = show;
            showLife = life;
        }

    }

}
