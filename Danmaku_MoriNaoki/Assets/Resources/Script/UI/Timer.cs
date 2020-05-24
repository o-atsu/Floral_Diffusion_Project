using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // 追加

public class Timer : MonoBehaviour
{

    // タイムを表示するText
    private Text timeText;

    // タイム
    private int time;
    private int minute;
    private int second;
    private int csecond;
    private string show;

    // 計測フラグ（外部から変更する）
    public static bool timeFlag = false;

    // PlusTimeコンポーネント
    private PlusTime pt;

    // Start is called before the first frame update
    void Start(){

        // タイムを表示するText
        this.timeText = this.GetComponent<Text>();

        // PlusTimeコンポーネントを取得
        pt = GameObject.Find("PlusTime").GetComponent<PlusTime>();

        // タイムを0に戻す
        time = 0;

        // ＝＝＝＝＝制作段階のテストプレイ用：ゲーム開始と同時に計測開始＝＝＝＝＝
        Debug.Log("ForTestPlay >> timeFlag = true");
        timeFlag = true;

    }

    // Update is called once per frame
    void Update(){

        // 時間経過
        if(timeFlag==true){
            time += 1;
        }

        // 表示文字列を作成
        show = "";

        minute = time / 3600;
        if(minute<10){
            show += "0";
        }
        show += minute.ToString() + ":";

        second = (time%3600) / 60;
        if(second<10){
            show += "0";
        }
        show += second.ToString() + ":";

        csecond = (time%60) * 5 / 3;
        if(csecond<10){
            show += "0";
        }
        show += csecond.ToString();

        // タイムを表示する
        timeText.text = show;

    }

    // ZONE終了時に経過タイムをPlusTimeに移す
    public void Archive(){
        pt.PlusTimeRewrite(time);
        time = 0;
    }

}
