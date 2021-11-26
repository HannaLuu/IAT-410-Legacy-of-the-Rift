using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCamera : MonoBehaviour {
    public Transform player;
    private void FixedUpdate() {
        var pos = transform.position;
        transform.position = new Vector3(player.position.x, pos.y, pos.z);
    }
}
