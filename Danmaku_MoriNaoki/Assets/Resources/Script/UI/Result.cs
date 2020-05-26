using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // 追加

public class Result : MonoBehaviour
{

    // リザルトを表示するText
    private Text resultText;

    // 各ゾーンのスコア
    public static Dictionary<string, int> score = new Dictionary<string, int>();

    // 各ゾーンのタイム
    public static Dictionary<string, int> time = new Dictionary<string, int>();

    // 各ゾーンのミス数とボム数
    public static Dictionary<string, int> missCounter = new Dictionary<string, int>();
    public static Dictionary<string, int> bombCounter = new Dictionary<string, int>();

    // Start is called before the first frame update
    void Start(){

        // GameObjectが消えないようにする
        DontDestroyOnLoad(this);

        // リザルトを表示するText
        this.resultText = this.GetComponent<Text>();

        // 初期化
        score.Add("A", 0);
        score.Add("B", 0);
        score.Add("C", 0);
        score.Add("D", 0);
        score.Add("E", 0);
        time.Add("A", 0);
        time.Add("B", 0);
        time.Add("C", 0);
        time.Add("D", 0);
        time.Add("E", 0);
        missCounter.Add("A", 0);
        missCounter.Add("B", 0);
        missCounter.Add("C", 0);
        missCounter.Add("D", 0);
        missCounter.Add("E", 0);
        bombCounter.Add("A", 0);
        bombCounter.Add("B", 0);
        bombCounter.Add("C", 0);
        bombCounter.Add("D", 0);
        bombCounter.Add("E", 0);

    }

    // Update is called once per frame
    void Update(){

    }

    // スコアのカウント
    public void CountScore(string zone, int add){
        score[zone] = add;
    }

    // タイムのカウント
    public void CountTime(string zone, int add){
        time[zone] = add;
    }

    // ミス数とボム数のカウント
    public void CountMissAndBomb(string zone, int miss, int bomb){
        missCounter[zone] = miss;
        bombCounter[zone] = bomb;
    }

}
