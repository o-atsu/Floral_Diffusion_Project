using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseNwaySeveralTime : Attack
{
    private Rigidbody2D rb;
    float conv_rad = Mathf.PI / 180f;
    float conv_dierect = 180f / Mathf.PI;

    public int ways_num = 1;
    public int serial_num = 1;
    private float start_dir = 0f;
    public GameObject start_posi_object;
    private GameObject playe_ob;

    [SerializeField]
    private Barrage_generator[] generators;

    protected override IEnumerator shot()
    {
        yield return new WaitForSeconds(0.2f);

        while (true)
        {
            Chase();
            float set_dir = start_dir;

            for (int j = 0; j < serial_num; j++) {
                for (int i = 0; i < ways_num; i++)
                {
                    if (start_posi_object == null)
                    {
                        start_posi_object = this.gameObject;
                    }
                    Vector2 start_posi = new Vector2(start_posi_object.transform.position.x, start_posi_object.transform.position.y);
                    generators[0].SetPosition(start_posi);
                    generators[0].Set_Direct(set_dir);
                    generators[0].Generate();
                    set_dir += 360f / (float)ways_num;
                }
                yield return new WaitForSeconds(0.08f);
            }

            yield return new WaitForSeconds(interval);
        }
    }

    void Start()
    {
        playe_ob = GameObject.Find("Player");
        StartCoroutine("shot");
    }

    protected void Chase()
    {
        Vector2 this_position = new Vector2(this.transform.position.x, this.transform.position.y);
        Vector2 target_position = new Vector2(playe_ob.transform.position.x, playe_ob.transform.position.y);
        Vector2 dt = target_position - this_position;
        float target_direction = Mathf.Atan2(dt.y, dt.x);
        SetStartDirect(target_direction * conv_dierect);
    }

    public void SetStartDirect(float dir)
    {
        start_dir = dir;
    }
}
