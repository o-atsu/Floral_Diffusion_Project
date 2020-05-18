using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // 追加

public class Bomb : MonoBehaviour
{

    // ボムを表示するText
    private Text bombText;

    // プレイヤーコントロールのコンポーネント
    private Player_controll pc;

    // ボム
    private int bomb;
    private string show;

    // 表示ボム
    private int showBomb;

    // Start is called before the first frame update
    void Start(){

        // ボムを表示するText
        this.bombText = this.GetComponent<Text>();

        // プレイヤーコントロールのコンポーネントを取得
        pc = GameObject.FindWithTag("Player").GetComponent<Player_controll>();

        // ボムを取得する
        bomb = pc.GetBombCount();

        // 表示を更新する
        show = "";
        if(bomb>=1){
            for(int i=1; i<=bomb; i++){
                show += "★";
            }
        }
        bombText.text = show;
        showBomb = bomb;

    }

    // Update is called once per frame
    void Update(){

        // ボムを取得する
        bomb = pc.GetBombCount();

        // 必要に応じて表示を更新する
        if(showBomb!=bomb){
            show = "";
            if(bomb>=1){
                for(int i=1; i<=bomb; i++){
                    show += "★";
                }
            }
            bombText.text = show;
            showBomb = bomb;
        }

    }

}
