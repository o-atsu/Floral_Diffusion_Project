using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantRandNway : Attack
{

    public int ways_num;
    public float generate_wait_time = 0f;
    private float generate_wait_count = 0f;

    [SerializeField]
    private Barrage_generator[] generators;

    protected override IEnumerator shot()
    {
        yield return new WaitForSeconds(0.2f);
        while (true)
        {
            if (generate_wait_count > 0f)
            {
                generate_wait_count -= Time.deltaTime;
                yield return new WaitForSeconds(Time.deltaTime);
            }
            else
            {

                float set_dir = Random.RandomRange(0f, 1f) * 360f;
                for (int i = 0; i < ways_num; i++)
                {
                    Vector2 start_position = new Vector2(this.transform.position.x, this.transform.position.y);
                    generators[0].SetPosition(start_position);
                    generators[0].Set_Direct(set_dir);
                    generators[0].Generate();
                    set_dir += 360f / (float)ways_num;
                }
                yield return new WaitForSeconds(interval);

            }
        }
    }

    void OnEnable()
    {
        generate_wait_count = generate_wait_time;
        StartCoroutine("shot");
    }
    private void OnDisable()
    {
        StopCoroutine("shot");
    }
}
