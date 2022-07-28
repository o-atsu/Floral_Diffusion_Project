using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // 追加
using UnityEngine.SceneManagement; // 追加

public class Result : MonoBehaviour
{

    // リザルトを表示するText
    private Text leftResultText;
    private Text resultText;

    // リザルト文
    private string show;

    // トータルスコア
    private int totalScore;

    // PlusScoreのGameObject
    // private GameObject psgo;

    // PlusTimeのGameObject
    // private GameObject ptgo;

    // FluffGenerateObのGameObjectを取得
    private GameObject fgogo;

    // ゾーンのText
    private Text zoneText;

    // 各ゾーンのスコア
    public static Dictionary<string, int> score = new Dictionary<string, int>();

    // 各ゾーンのタイム
    public static Dictionary<string, float> time = new Dictionary<string, float>();

    // 各ゾーンのミス数とボム数
    public static Dictionary<string, int> missCounter = new Dictionary<string, int>();
    public static Dictionary<string, int> bombCounter = new Dictionary<string, int>();

    // ライフ
    public static int resultLife;

    // ボム
    public static int resultBomb;

    // タイムの表示文字列
    private string timeString;

    // ランク
    private string rank;

    // 現在処理中のゾーン
    private string nowZone;

    // AudioSourceコンポーネント
    private AudioSource zbas;
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip showHead;
    [SerializeField]
    private AudioClip showClear;
    [SerializeField]
    private AudioClip showRank;
    [SerializeField]
    private AudioClip pushKey;

    // Start is called before the first frame update
    void Start(){

        // ゾーンのText
        zoneText = GameObject.Find("Zone").GetComponent<Text>();

        if(zoneText.text=="RESULT"){

            // リザルトを表示するText
            leftResultText = GameObject.Find("LeftResult").GetComponent<Text>();
            this.resultText = this.GetComponent<Text>();

            // PlusScoreのGameObjectを取得
            // psgo = GameObject.Find("PlusScore");

            // PlusTimeのGameObjectを取得
            // ptgo = GameObject.Find("PlusTime");

            // FluffGenerateObのGameObjectを取得
            fgogo = GameObject.Find("FluffGenerateOb");

            // AudioSourceコンポーネントを取得
            zbas = GameObject.Find("ZoneBackground").GetComponent<AudioSource>();
            audioSource = this.GetComponent<AudioSource>();

            // リザルトを表示する
            WriteResult();

        }

    }

    // スコアのカウント
    public void CountScore(string zone, int add){
        score[zone] = add;
    }

    // タイムのカウント
    public void CountTime(string zone, float add){
        time[zone] = add;
    }

    // ミス数とボム数のカウント
    public void CountMissAndBomb(string zone, int miss, int bomb){
        missCounter[zone] = miss;
        bombCounter[zone] = bomb;
    }

    // ライフとボムのカウント
    public void CountLifeAndBomb(int life, int bomb){
        resultLife = life;
        resultBomb = bomb;
    }

    // タイムの表示文字列の生成
    public string TimeToString(float timeFloat){
        timeString = "";
        if((int)(timeFloat/60f)<10){
            timeString += "0";
        }
        timeString += ((int)(timeFloat/60f)).ToString() + ":";
        if((int)((timeFloat%60f)/1f)<10){
            timeString += "0";
        }
        timeString += ((int)((timeFloat%60f)/1f)).ToString() + ":";
        if((int)((timeFloat%1f)/0.01f)<10){
            timeString += "0";
        }
        timeString += ((int)((timeFloat%1f)/0.01f)).ToString();
        return timeString;
    }

    // リザルトを表示する
    private void WriteResult(){

        // プラススコアとプラスタイムを非表示にする
        // psgo.SetActive(false);
        // ptgo.SetActive(false);
        // Debug.Log("false!");

        // リザルトを表示する演出の開始
        StartCoroutine("ShowResult");

    }

    // リザルトを表示する演出
    private IEnumerator ShowResult(){

        // リザルト文の初期化
        leftResultText.text = "";
        show = "";

        // BGM開始と綿毛の演出
        if(resultLife>=1){
            zbas.Play();
        } else {
            fgogo.SetActive(false);
        }

        // ウェイト
        yield return new WaitForSeconds(0.75f);

        // ゾーンA
        nowZone = "A";
        leftResultText.text += "<size=20>ZONE </size>" + nowZone + "\n";
        if(time.ContainsKey(nowZone)==true){
            Timer.AddTime(time[nowZone]);
            leftResultText.text += "<size=20>" + TimeToString(time[nowZone]) + "</size>\n\n";
        } else {
            leftResultText.text += "<size=20>00:00:00</size>\n\n";
        }
        if(score.ContainsKey(nowZone)==true){
            show += Score.AddScore(score[nowZone]).ToString() + "\n";
        } else {
            show += "0\n";
        }
        if(missCounter.ContainsKey(nowZone)==true){
            if(missCounter[nowZone]<=0&&bombCounter[nowZone]<=0){
                show += "<size=20>Excellent!</size>";
            } else {
                show += "<size=20>" + missCounter[nowZone] + "</size><size=15> MISS</size>";
            }
        } else {
            show += "<size=20>0</size><size=15> MISS</size>";
        }
        if(bombCounter.ContainsKey(nowZone)==true){
            if(missCounter[nowZone]<=0&&bombCounter[nowZone]<=0){
                show += "\n\n";
            } else {
                show += "<size=20> " + bombCounter[nowZone] + "</size><size=15> BOMB</size>\n\n";
            }
        } else {
            show += "<size=20> 0</size><size=15> BOMB</size>\n\n";
        }
        resultText.text = show;
        audioSource.PlayOneShot(showHead);

        // ウェイト
        yield return new WaitForSeconds(0.75f);

        // ゾーンB
        nowZone = "B";
        leftResultText.text += "<size=20>ZONE </size>" + nowZone + "\n";
        if(time.ContainsKey(nowZone)==true){
            Timer.AddTime(time[nowZone]);
            leftResultText.text += "<size=20>" + TimeToString(time[nowZone]) + "</size>\n\n";
        } else {
            leftResultText.text += "<size=20>00:00:00</size>\n\n";
        }
        if(score.ContainsKey(nowZone)==true){
            show += Score.AddScore(score[nowZone]).ToString() + "\n";
        } else {
            show += "0\n";
        }
        if(missCounter.ContainsKey(nowZone)==true){
            if(missCounter[nowZone]<=0&&bombCounter[nowZone]<=0){
                show += "<size=20>Excellent!</size>";
            } else {
                show += "<size=20>" + missCounter[nowZone] + "</size><size=15> MISS</size>";
            }
        } else {
            show += "<size=20>0</size><size=15> MISS</size>";
        }
        if(bombCounter.ContainsKey(nowZone)==true){
            if(missCounter[nowZone]<=0&&bombCounter[nowZone]<=0){
                show += "\n\n";
            } else {
                show += "<size=20> " + bombCounter[nowZone] + "</size><size=15> BOMB</size>\n\n";
            }
        } else {
            show += "<size=20> 0</size><size=15> BOMB</size>\n\n";
        }
        resultText.text = show;
        audioSource.PlayOneShot(showHead);

        // ウェイト
        yield return new WaitForSeconds(0.75f);

        /*

        // ゾーンC
        nowZone = "C";
        leftResultText.text += "<size=20>ZONE </size>" + nowZone + "\n";
        if(time.ContainsKey(nowZone)==true){
            Timer.AddTime(time[nowZone]);
            leftResultText.text += "<size=20>" + TimeToString(time[nowZone]) + "</size>\n\n";
        } else {
            leftResultText.text += "<size=20>00:00:00</size>\n\n";
        }
        if(score.ContainsKey(nowZone)==true){
            show += Score.AddScore(score[nowZone]).ToString() + "\n";
        } else {
            show += "0\n";
        }
        if(missCounter.ContainsKey(nowZone)==true){
            if(missCounter[nowZone]<=0&&bombCounter[nowZone]<=0){
                show += "<size=20>Excellent!</size>";
            } else {
                show += "<size=20>" + missCounter[nowZone] + "</size><size=15> MISS</size>";
            }
        } else {
            show += "<size=20>0</size><size=15> MISS</size>";
        }
        if(bombCounter.ContainsKey(nowZone)==true){
            if(missCounter[nowZone]<=0&&bombCounter[nowZone]<=0){
                show += "\n\n";
            } else {
                show += "<size=20> " + bombCounter[nowZone] + "</size><size=15> BOMB</size>\n\n";
            }
        } else {
            show += "<size=20> 0</size><size=15> BOMB</size>\n\n";
        }
        resultText.text = show;
        audioSource.PlayOneShot(showHead);

        // ウェイト
        yield return new WaitForSeconds(0.75f);

        */

        // ゾーンD
        nowZone = "D";
        leftResultText.text += "<size=20>ZONE </size>" + nowZone + "\n";
        if(time.ContainsKey(nowZone)==true){
            Timer.AddTime(time[nowZone]);
            leftResultText.text += "<size=20>" + TimeToString(time[nowZone]) + "</size>\n\n";
        } else {
            leftResultText.text += "<size=20>00:00:00</size>\n\n";
        }
        if(score.ContainsKey(nowZone)==true){
            show += Score.AddScore(score[nowZone]).ToString() + "\n";
        } else {
            show += "0\n";
        }
        if(missCounter.ContainsKey(nowZone)==true){
            if(missCounter[nowZone]<=0&&bombCounter[nowZone]<=0){
                show += "<size=20>Excellent!</size>";
            } else {
                show += "<size=20>" + missCounter[nowZone] + "</size><size=15> MISS</size>";
            }
        } else {
            show += "<size=20>0</size><size=15> MISS</size>";
        }
        if(bombCounter.ContainsKey(nowZone)==true){
            if(missCounter[nowZone]<=0&&bombCounter[nowZone]<=0){
                show += "\n\n";
            } else {
                show += "<size=20> " + bombCounter[nowZone] + "</size><size=15> BOMB</size>\n\n";
            }
        } else {
            show += "<size=20> 0</size><size=15> BOMB</size>\n\n";
        }
        resultText.text = show;
        audioSource.PlayOneShot(showHead);

        // ウェイト
        yield return new WaitForSeconds(0.75f);

        // ゾーンE
        nowZone = "E";
        leftResultText.text += "<size=20>ZONE </size>" + nowZone + "\n";
        if(time.ContainsKey(nowZone)==true){
            Timer.AddTime(time[nowZone]);
            leftResultText.text += "<size=20>" + TimeToString(time[nowZone]) + "</size>\n\n";
        } else {
            leftResultText.text += "<size=20>00:00:00</size>\n\n";
        }
        if(score.ContainsKey(nowZone)==true){
            show += Score.AddScore(score[nowZone]).ToString() + "\n";
        } else {
            show += "0\n";
        }
        if(missCounter.ContainsKey(nowZone)==true){
            if(missCounter[nowZone]<=0&&bombCounter[nowZone]<=0){
                show += "<size=20>Excellent!</size>";
            } else {
                show += "<size=20>" + missCounter[nowZone] + "</size><size=15> MISS</size>";
            }
        } else {
            show += "<size=20>0</size><size=15> MISS</size>";
        }
        if(bombCounter.ContainsKey(nowZone)==true){
            if(missCounter[nowZone]<=0&&bombCounter[nowZone]<=0){
                show += "\n\n";
            } else {
                show += "<size=20> " + bombCounter[nowZone] + "</size><size=15> BOMB</size>\n\n";
            }
        } else {
            show += "<size=20> 0</size><size=15> BOMB</size>\n\n";
        }
        resultText.text = show;
        audioSource.PlayOneShot(showHead);

        // ウェイト
        yield return new WaitForSeconds(0.75f);

        // ライフボーナス
        leftResultText.text += "LIFE BONUS\n\n";
        show += Score.AddScore(resultLife*50000).ToString() + "\n\n";
        resultText.text = show;
        audioSource.PlayOneShot(showHead);

        // ウェイト
        yield return new WaitForSeconds(0.75f);

        // ボムボーナス
        leftResultText.text += "BOMB BONUS\n\n";
        show += Score.AddScore(resultBomb*25000).ToString() + "\n\n";
        resultText.text = show;
        audioSource.PlayOneShot(showHead);

        // ウェイト
        yield return new WaitForSeconds(0.75f);

        // ランク
        leftResultText.text += "RANK<size=45> </size>\n\n";
        totalScore = Score.GetScoreCount();
        if(totalScore>=1000000){
            rank = "SSS";
        } else if(totalScore>=900000){
            rank = "SS";
        } else if(totalScore>=800000){
            rank = "S";
        } else if(totalScore>=600000){
            rank = "A";
        } else if(totalScore>=400000){
            rank = "B";
        } else if(totalScore>=200000){
            rank = "C";
        } else if(totalScore>=100000){
            rank = "D";
        } else if(totalScore>=50000){
            rank = "E";
        } else if(totalScore>=25000){
            rank = "F";
        } else if(totalScore>=12500){
            rank = "G";
        } else {
            rank += "H";
        }
        show += "<size=45>" + rank + "</size>\n\n";
        resultText.text = show;
        if(resultLife>=1){
            audioSource.PlayOneShot(showClear);
        }
        audioSource.PlayOneShot(showRank);

        // 処理落ち率
        leftResultText.text += "<color=grey><size=20>PROCESSING OMISSION RATE</size></color>\n\n";
        show += "<color=grey><size=20>" + Fps.GetProcessingReport().ToString() + "</size><size=15>%</size></color>";
        resultText.text = show;

        // プレイ回数とプレイ時間とトータルスコアとハイスコアのロードとセーブ
        if(PlayerPrefs.HasKey("play")==true){
            PlayerPrefs.SetInt("play", PlayerPrefs.GetInt("play")+1);
        } else {
            PlayerPrefs.SetInt("play", 1);
        }
        if(PlayerPrefs.HasKey("playTime")==true){
            PlayerPrefs.SetFloat("playTime", PlayerPrefs.GetFloat("playTime")+Timer.GetTimeCount());
        } else {
            PlayerPrefs.SetFloat("playTime", Timer.GetTimeCount());
        }
        if(PlayerPrefs.HasKey("totalScore")==true){
            PlayerPrefs.SetInt("totalScore", PlayerPrefs.GetInt("totalScore")+totalScore);
        } else {
            PlayerPrefs.SetInt("totalScore", totalScore);
        }
        if(PlayerPrefs.HasKey("highScore")==true){
            if(totalScore>PlayerPrefs.GetInt("highScore")){
                PlayerPrefs.SetInt("highScore", totalScore);
            }
        } else {
            PlayerPrefs.SetInt("highScore", totalScore);
        }
        PlayerPrefs.Save();

        // タイトルへの遷移
        leftResultText.text += "        <color=orange>Press Z to Title</color>";
        while(true){
            if(Input.GetKeyDown(KeyCode.Z)){
                audioSource.PlayOneShot(pushKey);
                leftResultText.text = "";
                resultText.text = "";
                yield return new WaitForSeconds(1f);
                break;
            }
            yield return null;
        }
        SceneManager.LoadScene("Title");
        // FadeManager.Instance.LoadScene("Title", 1f);

        // 表示処理完了
        yield break;

    }

    // リセット
    public static void ResetResult(){
        score.Clear();
        time.Clear();
        missCounter.Clear();
        bombCounter.Clear();
        resultLife = 0;
        resultBomb = 0;
    }

}
