using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FluffGenerator : MonoBehaviour
{
    public GameObject fluff_object;
    public GameObject camvas_object;
    public int generate_num;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < generate_num; i++)
        {
            GameObject fluff = Instantiate(fluff_object);
            fluff.transform.parent = camvas_object.transform;
            fluff.transform.SetSiblingIndex(1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
