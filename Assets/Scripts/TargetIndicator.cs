using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetIndicator : MonoBehaviour
{
    Vector3 offset;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        offset = transform.parent.position;
        offset += new Vector3(0, transform.parent.GetComponentInParent<Enemy>().pivotPoint.transform.position.y / 0.45f, 0);
        transform.position = offset;
    }
}
