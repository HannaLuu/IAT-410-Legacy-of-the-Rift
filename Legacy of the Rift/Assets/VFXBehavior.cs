using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXBehavior : MonoBehaviour
{
    public int zLayer;
    void Start()
    {
        var initialPos = transform.position;
        transform.position = new Vector3(initialPos.x, initialPos.y, zLayer);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
