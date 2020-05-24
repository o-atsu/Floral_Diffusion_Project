using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetLocalPosition : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnale()
    {
        this.transform.localPosition = Vector3.zero;
    }

    

    // Update is called once per frame
    void Update()
    {
        this.transform.localPosition = Vector3.zero;
    }
}
