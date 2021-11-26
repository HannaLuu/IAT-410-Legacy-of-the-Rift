using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapSprite : MonoBehaviour
{
    public GameObject parent;

    void Update() {
        var parentPos = parent.transform.position;
        transform.position = new Vector3(parentPos.x, parentPos.y, 10);
    }
}
