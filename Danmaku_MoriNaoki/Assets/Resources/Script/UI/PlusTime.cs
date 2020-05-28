using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // 追加

public class PlusTime : MonoBehaviour
{

    // プラスタイムを表示するText
    private Text plusTimeText;

    // プラスタイム
    public static float plusTime = 0f;
    private int minute;
    private int second;
    private int csecond;
    private string show;

    // 表示プラスタイム
    private static float showPlusTime;

    // タイム増加演出のフレーム数
    private static int showFrame = 14;

    // 1F毎に増加させる表示タイム
    private static float addPerFrame;

    // Start is called before the first frame update
    void Start(){

        // プラスタイムを表示するText
        this.plusTimeText = this.GetComponent<Text>();

        // シーン切り替え時の表示更新処理
        PlusTimeRewrite(0f);

    }

    // 表示更新
    public void PlusTimeRewrite(float add){
        plusTime += add;
        addPerFrame = System.Math.Max((plusTime-showPlusTime)/showFrame+0.01f, 0.01f);
        StartCoroutine("ShowPlusTime");
    }

    // 表示更新の演出
    private IEnumerator ShowPlusTime(){

        while(true){

            // 表示プラスタイムを増加させる
            showPlusTime += System.Math.Min(plusTime-showPlusTime, addPerFrame);

            // 表示文字列を作成
            show = "+ ";

            minute = (int)(showPlusTime/60f);
            if(minute<10){
                show += "0";
            }
            show += minute.ToString() + ":";

            second = (int)((showPlusTime%60f)/1f);
            if(second<10){
                show += "0";
            }
            show += second.ToString() + ":";

            csecond = (int)((showPlusTime%1f)/0.01f);
            if(csecond<10){
                show += "0";
            }
            show += csecond.ToString();

            // プラスタイムを表示する
            plusTimeText.text = show;

            // 更新完了判定
            if(showPlusTime==plusTime){
                yield break;
            } else {
                yield return new WaitForEndOfFrame();
            }

        }

    }

    /*

    // 表示更新の演出
    private IEnumerator ShowPlusTime(){

        while(true){

            // 表示プラスタイムを増加させる
            showPlusTime += System.Math.Min(plusTime-showPlusTime, addPerFrame);

            // 表示文字列を作成
            show = "+ ";

            minute = showPlusTime / 3600;
            if(minute<10){
                show += "0";
            }
            show += minute.ToString() + ":";

            second = (showPlusTime%3600) / 60;
            if(second<10){
                show += "0";
            }
            show += second.ToString() + ":";

            csecond = (showPlusTime%60) * 5 / 3;
            if(csecond<10){
                show += "0";
            }
            show += csecond.ToString();

            // プラスタイムを表示する
            plusTimeText.text = show;

            // 更新完了判定
            if(showPlusTime==plusTime){
                yield break;
            } else {
                yield return new WaitForEndOfFrame();
            }

        }

    }

    */

}
