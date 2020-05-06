using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Chase_Shot : Attack
{
    public Player_ChaseBullet_Generator[] player_generators;
    // Start is called before the first frame update
    void Start()
    {
        player_generators[0].SetStatus(0, 0, 90f, 10.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z)) StartCoroutine("shot");
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
