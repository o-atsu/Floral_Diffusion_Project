using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Title : SceneChange
{
    int main_step=0;
    int option_step=-1;

    int BGM_volume;
    int SE_volume;

    public AudioMixer am;

    public RectTransform arrow_rect;
    public RectTransform option_arrow_rect;
    public GameObject OptionUI;

    public Text bgm_volume_text;
    public Text center_bgm_volume_text;
    public Text se_volume_text;
    public Text center_se_volume_text;
    public Text high_score_value_text;
    public Text center_high_score_value_text;

    public AudioSource check_volume_se_source;
    public AudioSource scene_change_se;
    public AudioSource dicide_se_source;
    public AudioSource cancel_change_se;
    public AudioSource move_arrow_se;

    private bool changing_scene = false;

    void Start(){
        OptionUI.SetActive(false);
        //PlayerPrefs.SetInt("highScore", 0);
        int high_score = PlayerPrefs.GetInt("highScore");
        high_score_value_text.text = high_score.ToString();
        center_high_score_value_text.text = high_score.ToString();

        BGM_volume = PlayerPrefs.GetInt("BGM_vol");
        if (BGM_volume < 1 || 10 < BGM_volume) BGM_volume = 5;
        BGM_Volume_Change();
        SE_volume = PlayerPrefs.GetInt("SE_vol");
        if (SE_volume < 1 || 10 < SE_volume) SE_volume = 5;
        SE_Volume_Change();

	}

    private void OnDestroy()
    {
        PlayerPrefs.SetInt("BGM_vol", BGM_volume);
        PlayerPrefs.SetInt("SE_vol", SE_volume);
    }

    private void Update()
    {

        if (changing_scene == false)
        {
            if (option_step != -1)
            {
                OptionSetting();
            }
            else
            {
                MainProcessing();
            }
        }

    }


    private void MainProcessing()
    {
        if (main_step < 2 && Input.GetKeyDown(KeyCode.DownArrow))
        {
            main_step++;
            move_arrow_se.Play();
        }

        if (main_step > 0 && Input.GetKeyDown(KeyCode.UpArrow))
        {
            main_step--;
            move_arrow_se.Play();
        }

            switch (main_step)
        {

            case 0:
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    scene_change_se.Play();
                    FadeManager.Instance.LoadScene("Zone_A", 0.9f);
                    changing_scene = true;
                }
                break;

            case 1:
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    option_step = 0;
                    OptionUI.SetActive(true);
                    dicide_se_source.Play();
                }
                break;

            case 2:
                if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.X))
                {
                    #if UNITY_EDITOR
                        UnityEditor.EditorApplication.isPlaying = false;
                    #else
                        Application.Quit();
                    #endif
                    
                    cancel_change_se.Play();
                }
                break;
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            main_step = 2;
            cancel_change_se.Play();
        }

        SetPositionTitleArrow();
    }

    private void SetPositionTitleArrow()
    {
        arrow_rect.anchoredPosition = new Vector3(300f, (float)main_step * 100f * -1f);
    }

    private void OptionSetting()
    {

        if (option_step < 1 && Input.GetKeyDown(KeyCode.DownArrow))
        {
            option_step++;
            if (option_step == 0) check_volume_se_source.Stop();
            if (option_step == 1) check_volume_se_source.Play();
            move_arrow_se.Play();
        }

        if (option_step > 0 && Input.GetKeyDown(KeyCode.UpArrow))
        {
            option_step--;
            if (option_step == 0) check_volume_se_source.Stop();
            if (option_step == 1) check_volume_se_source.Play();
            move_arrow_se.Play();
        }

        switch (option_step)
        {

            case 0:
                BGM_Volume_Setting();
                break;

            case 1:
                SE_Volume_Setting();
                break;
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            if (option_step == 1) check_volume_se_source.Stop();
            option_step = -1;
            OptionUI.SetActive(false);
            cancel_change_se.Play();
        }

        SetPositionOptionArrow();

    }

    private void SetPositionOptionArrow()
    {
        option_arrow_rect.anchoredPosition = new Vector3(-300f, (float)option_step * 100f * -1f);
    }

    private void BGM_Volume_Setting()
    {

        if (BGM_volume > 1 && Input.GetKeyDown(KeyCode.LeftArrow)) BGM_volume--;
        if (BGM_volume < 10 && Input.GetKeyDown(KeyCode.RightArrow)) BGM_volume++;
        BGM_Volume_Change();
    }

     private void BGM_Volume_Change()
    {
        float set_volume_bgm = (float)BGM_volume * 4f - 21f;
        am.SetFloat("BGM_Vol", set_volume_bgm);
        string S = "<< ";
        S += BGM_volume.ToString();
        S += " >>";
        bgm_volume_text.text = S;
        center_bgm_volume_text.text = S;
    }

    private void SE_Volume_Setting()
    {

        if (SE_volume > 1 && Input.GetKeyDown(KeyCode.LeftArrow)) SE_volume--;
        if (SE_volume < 10 && Input.GetKeyDown(KeyCode.RightArrow)) SE_volume++;
        SE_Volume_Change();
    }

    private void SE_Volume_Change()
    {
        float set_volume_se = (float)SE_volume * 4f - 21f;
        am.SetFloat("SE_Vol", set_volume_se);
        string S = "<< ";
        S += SE_volume.ToString();
        S += " >>";
        se_volume_text.text = S;
        center_se_volume_text.text = S;
    }

}
