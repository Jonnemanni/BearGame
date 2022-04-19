using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // The player variable will be set on this.
    private Transform player;

    // Getting the player object on start, so it doesn't have to be set later.
    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }


    // Update is called once per frame
    private void Update()
    {
        transform.position = new Vector3(player.position.x, player.position.y, -10);
    }
}
