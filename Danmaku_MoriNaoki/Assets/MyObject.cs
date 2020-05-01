using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Transform m = this.transform;
        Debug.Log(m);
        Vector3 p = m.position;
        p.x = 1.0f;
        p.y = 1.0f;
        p.z = 1.0f;
        p.position = p;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
