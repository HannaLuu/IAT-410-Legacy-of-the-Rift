using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBackground : MonoBehaviour {
    public Transform player;

    private SpriteRenderer _renderer;
    // Start is called before the first frame update
    void Start() {
        _renderer = GetComponent<SpriteRenderer>();
       _renderer.enabled = false;
        PlayerHealth.OnDeath += (sender, args) => {
            _renderer.enabled = true;
        };
    }

    // Update is called once per frame
    void Update() {
        Vector3 playerPos = player.position;
        transform.position = new Vector3(playerPos.x, playerPos.y, playerPos.z + 1);
    }
}
