using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombShot : Attack
{
    private const float shot_time=4.0f;
    public float shot_time_count;

    public AudioClip sound1;
    public AudioClip sound2;
    AudioSource audioSource;
    private const float sound1_time=10.0f;
    float sound1_time_count=0f;

    public BombBulletGenerator[] bomb_generators;
    // Start is called before the first frame update
    void Start()
    {
        shot_time_count = 0f;
        audioSource = this.gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.localPosition = new Vector3(0f, 0f, 0f);
        GameObject player_ob = GameObject.FindGameObjectWithTag("Player");
        Player_controll player_controll = player_ob.GetComponent<Player_controll>();

        if (shot_time_count >= 0f) shot_time_count -= Time.deltaTime;
        if (sound1_time_count > 0f)
        {
            sound1_time_count -= Time.deltaTime;
            audioSource.volume = sound1_time_count / 20f;
        }
        else audioSource.Stop();

        if (Input.GetKeyDown(KeyCode.X) && shot_time_count <=0f && player_controll.GetBombCount()>0)
        {
            player_controll.DecreaseBomb();
            shot_time_count = shot_time;
            StartCoroutine("shot");
            audioSource.PlayOneShot(sound2);
            audioSource.PlayOneShot(sound1);
            sound1_time_count = sound1_time;
        }


    }

    protected override IEnumerator shot()
    {
        while (shot_time_count>=0f)
        {
            bomb_generators[0].Generate();
            bomb_generators[1].Generate();
            bomb_generators[2].Generate();
            bomb_generators[3].Generate();


            yield return new WaitForSeconds(interval);
        }


        yield break;
    }


}
