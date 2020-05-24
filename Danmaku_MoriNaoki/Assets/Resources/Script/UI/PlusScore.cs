using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // 追加

public class PlusScore : MonoBehaviour
{

    // プラススコアを表示するText
    private Text plusScoreText;

    // プラススコア
    private int plusScore;

    // Start is called before the first frame update
    void Start(){

        // プラススコアを表示するText
        this.plusScoreText = this.GetComponent<Text>();

        // プラススコアを0に戻す
        plusScore = 0;

    }

    // 表示更新
    public void PlusScoreRewrite(int add){

        // スコアの増加分を受け取る
        plusScore += add;

        // プラススコアを表示する
        plusScoreText.text = "+ " + plusScore.ToString();

    }

}
