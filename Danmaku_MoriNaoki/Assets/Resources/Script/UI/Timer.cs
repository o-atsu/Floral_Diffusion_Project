using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // 追加

public class Timer : MonoBehaviour
{

    // タイムを表示するText
    private Text timeText;

    // タイム
    private static int time;
    private int minute;
    private int second;
    private int csecond;
    private string show;

    // 計測フラグ（外部から変更する）
    public static bool timeFlag = false;

    // 表示タイム
    private static int showTime;

    // タイム増加演出のフレーム数
    private static int showFrame = 14;

    // 1F毎に増加させる表示タイム
    private static int addPerFrame;

    // PlusTimeコンポーネント
    private PlusTime pt;

    // Resultコンポーネント
    private Result r;

    // ゾーンのText
    private Text zoneText;

    // アーカイブ中フラグ
    private bool archiveNow;

    // Start is called before the first frame update
    void Start(){

        // タイムを表示するText
        this.timeText = this.GetComponent<Text>();

        // PlusTimeコンポーネントを取得
        pt = GameObject.Find("PlusTime").GetComponent<PlusTime>();

        // Resultコンポーネントを取得
        r = GameObject.Find("Result").GetComponent<Result>();

        // 初期化
        time = 0;
        archiveNow = false;

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

        if(archiveNow==true){

            // 表示タイムを減少させる
            showTime += System.Math.Max(showTime*-1, addPerFrame);

            // アーカイブが完了したらフラグをfalseにする
            if(showTime<=0){
                archiveNow = false;
            }

        } else {

            // 表示タイムを増加させる
            showTime = time;

        }

        // 表示文字列を作成
        show = "";

        minute = showTime / 3600;
        if(minute<10){
            show += "0";
        }
        show += minute.ToString() + ":";

        second = (showTime%3600) / 60;
        if(second<10){
            show += "0";
        }
        show += second.ToString() + ":";

        csecond = (showTime%60) * 5 / 3;
        if(csecond<10){
            show += "0";
        }
        show += csecond.ToString();

        // タイムを表示する
        timeText.text = show;

    }

    // ZONE終了時に経過タイムをResultとPlusTimeに移す
    public void Archive(){
        archiveNow = true;
        zoneText = GameObject.Find("Zone").GetComponent<Text>();
        r.CountTime(zoneText.text, time);
        pt.PlusTimeRewrite(time);
        time = 0;
        addPerFrame = System.Math.Min(showTime*-1/showFrame+1, -1);
    }

    public int GetTimeCount(){
        return time;
    }

}
