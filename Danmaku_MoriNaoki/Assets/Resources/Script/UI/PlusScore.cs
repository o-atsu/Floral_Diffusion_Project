using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // 追加

public class PlusScore : MonoBehaviour
{

    // プラススコアを表示するText
    private Text plusScoreText;

    // プラススコア
    public static int plusScore = 0;

    // 表示プラススコア
    private static int showPlusScore;

    // プラススコア増加演出のフレーム数
    private static int showFrame = 14;

    // 1F毎に増加させる表示プラススコア
    private static int addPerFrame;

    // Start is called before the first frame update
    void Start(){

        // プラススコアを表示するText
        this.plusScoreText = this.GetComponent<Text>();

        // シーン切り替え時の表示更新処理
        PlusScoreRewrite(0);

    }

    // 表示更新
    public void PlusScoreRewrite(int add){
        plusScore += add;
        addPerFrame = System.Math.Max((plusScore-showPlusScore)/showFrame+1, 1);
        StartCoroutine("ShowPlusScore");
    }

    // 表示更新の演出
    private IEnumerator ShowPlusScore(){

        while(true){

            // 表示プラススコアを増加させる
            showPlusScore += System.Math.Min(plusScore-showPlusScore, addPerFrame);

            // プラススコアを表示する
            plusScoreText.text = "+ " + showPlusScore.ToString();

            // 更新完了判定
            if(showPlusScore==plusScore){
                yield break;
            } else {
                yield return new WaitForEndOfFrame();
            }

        }

    }

}
