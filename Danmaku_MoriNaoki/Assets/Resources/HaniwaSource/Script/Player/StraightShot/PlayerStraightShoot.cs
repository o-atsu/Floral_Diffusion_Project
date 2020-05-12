using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStraightShoot : Attack
{
    public Player_Straight_Barrage_generator[] player_generators;
    // Start is called before the first frame update
    void Start()
    {
        //player_generators[0].SetStatus(this.transform.position.x , this.transform.position.y, 90f, 10.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))StartCoroutine("shot");
    }

    protected override IEnumerator shot()
    {
        while (Input.GetKey(KeyCode.Z))
        {
            player_generators[0].Generate();
            yield return new WaitForSeconds(interval);
        }
        yield break;
    }

}
