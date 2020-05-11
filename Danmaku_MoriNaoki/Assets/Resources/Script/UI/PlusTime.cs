using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // 追加

public class PlusTime : MonoBehaviour
{

    // プラスタイムを表示するText
    public Text plusTimeText;

    // プラスタイム
    public static int plusTime;
    private static int minute;
    private static int second;
    private static int csecond;
    private static string show;

    // Start is called before the first frame update
    void Start(){

        // プラスタイムを表示するText
        this.plusTimeText = this.GetComponent<Text>();

        // プラスタイムを0に戻す
        plusTime = 0;

    }

    // 表示更新
    public static void PlusTimeRewrite(){

        // 表示文字列を作成
        show = "+ ";

        minute = plusTime / 3600;
        if(minute<10){
            show += "0";
        }
        show += minute.ToString() + ":";

        second = plusTime / 60 - minute * 60;
        if(second<10){
            show += "0";
        }
        show += second.ToString() + ":";

        csecond = plusTime * 5 / 3 - minute * 3600 - second * 60;
        if(csecond<10){
            show += "0";
        }
        show += csecond.ToString();

        // プラスタイムを表示する
        plusTimeText.text = show;

    }

}
