using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // 追加

public class Timer : MonoBehaviour
{

    // タイムを表示するText
    private Text timeText;

    // タイム
    private static float time;
    private int minute;
    private int second;
    private int csecond;
    private string show;

    // 計測フラグ（外部から変更する）
    public static bool timeFlag = false;

    // 表示タイム
    private static float showTime;

    // タイム増加演出のフレーム数
    private static int showFrame = 14;

    // 1F毎に増加させる表示タイム
    private static float addPerFrame;

    // PlusTimeコンポーネント
    private PlusTime pt;

    // Resultコンポーネント
    private Result r;

    // ゾーンのText
    private Text zoneText;

    // アーカイブ中フラグ
    private bool archiveNow;

    // リザルト中フラグ
    private static bool resultNow;

    // Start is called before the first frame update
    void Start(){

        // タイムを表示するText
        this.timeText = this.GetComponent<Text>();

        // PlusTimeコンポーネントを取得
        pt = GameObject.Find("PlusTime").GetComponent<PlusTime>();

        // Resultコンポーネントを取得
        r = GameObject.Find("Result").GetComponent<Result>();

        // 初期化
        time = 0f;
        archiveNow = false;
        resultNow = false;

        // ＝＝＝＝＝制作段階のテストプレイ用：ゲーム開始と同時に計測開始＝＝＝＝＝
        // Debug.Log("ForTestPlay >> timeFlag = true");
        // timeFlag = true;

    }

    // Update is called once per frame
    void Update(){

        // 時間経過
        if(timeFlag==true){
            time += Time.deltaTime;
        }

        if(archiveNow==true){

            // 表示タイムを減少させる
            showTime += System.Math.Max(showTime*-1f, addPerFrame);

            // アーカイブが完了したらフラグをfalseにする
            if(showTime<=0f){
                archiveNow = false;
            }

        } else if(resultNow==true){

            // 表示タイムを増加させる
            showTime += System.Math.Min(time-showTime, addPerFrame);

            // リザルトが完了したらフラグをfalseにする
            if(showTime==time){
                resultNow = false;
            }

        } else {

            // 表示タイムを増加させる
            showTime = time;

        }

        // 表示文字列を作成
        show = "";

        minute = (int)(showTime/60f);
        if(minute<10){
            show += "0";
        }
        show += minute.ToString() + ":";

        second = (int)((showTime%60f)/1f);
        if(second<10){
            show += "0";
        }
        show += second.ToString() + ":";

        csecond = (int)((showTime%1f)/0.01f);
        if(csecond<10){
            show += "0";
        }
        show += csecond.ToString();

        // タイムを表示する
        timeText.text = show;

    }

    /*

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

    */

    // ZONE終了時に経過タイムをResultとPlusTimeに移す
    public void Archive(){
        archiveNow = true;
        zoneText = GameObject.Find("Zone").GetComponent<Text>();
        r.CountTime(zoneText.text, (float)((int)(time/0.01f))/100f);
        pt.PlusTimeRewrite((float)((int)(time/0.01f))/100f);
        time = 0f;
        addPerFrame = System.Math.Min(showTime*-1f/showFrame-0.01f, -0.01f);
    }

    // タイムの増加
    public static void AddTime(float add){
        resultNow = true;
        time += add;
        addPerFrame = System.Math.Max((time-showTime)/showFrame+0.01f, 0.01f);
    }

    public static float GetTimeCount(){
        return time;
    }

}
