using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // 追加

public class EndPhase : MonoBehaviour
{

    // 評価を表示するText
    private Text gradeText;

    // 評価文
    private string show;

    // ミス数のカウンター
    private static int missCounter;

    // ボム数のカウンター
    private static int bombCounter;

    // Start is called before the first frame update
    void Start(){

        // 評価を表示するText
        this.gradeText = this.GetComponent<Text>();

        // 初期化
        gradeText.text = "";
        show = "";
        missCounter = 0;
        bombCounter = 0;

    }

    // ミス数のカウント
    public static void CountMiss(){
        missCounter += 1;
    }

    // ボム数のカウント
    public static void CountBomb(){
        bombCounter += 1;
    }

    // 評価文を表示する
    public void WriteGrade(int add){

        // 評価文の初期化
        show = "";

        // スコア増加量の計算と評価文の作成
        if(missCounter<=0&&bombCounter<=0){

            // ノーミスノーボム
            add = System.Math.Max(add, 1);
            show += "Excellent!\n<size=45>+ " + Score.AddScore(add).ToString() + "</size>";

        } else {

            // ミス数またはボム数が1以上
            add *= 12-(missCounter+bombCounter);
            add /= 20;
            add = System.Math.Max(add, 1);
            show += missCounter.ToString() + "<size=45> MISS</size> " + bombCounter.ToString() + "<size=45> BOMB\n+ " + Score.AddScore(add).ToString() + "</size>";

        }

        // スコアの増加と評価文の表示
        gradeText.text = show;

        // ミス数とボム数の保存
        EndZone.CountMiss(missCounter);
        EndZone.CountBomb(bombCounter);

        // カウンターの初期化
        missCounter = 0;
        bombCounter = 0;

        // 評価文を表示する演出の開始
        StartCoroutine("ShakeGrade");

    }

    // 評価文を表示する演出
    private IEnumerator ShakeGrade(){

        for(int i=1; i<=2; i++){
            this.gameObject.transform.position += new Vector3(80f, 0f, 0f);
            yield return new WaitForEndOfFrame();
            this.gameObject.transform.position += new Vector3(-80f, 0f, 0f);
            yield return new WaitForEndOfFrame();
            this.gameObject.transform.position += new Vector3(-80f, 0f, 0f);
            yield return new WaitForEndOfFrame();
            this.gameObject.transform.position += new Vector3(80f, 0f, 0f);
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(5f);

        gradeText.text = "";
        yield break;

    }

}
