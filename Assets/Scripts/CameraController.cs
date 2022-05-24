using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // The player variable will be set on this.
    private Transform player;
    // A number of floats to set the limits of how far the camera can go.
    [SerializeField] private float upClamp;
    [SerializeField] private float downClamp;
    [SerializeField] private float leftClamp;
    [SerializeField] private float rightClamp;

    // Getting the player object on start, so it doesn't have to be set later.
    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }


    // Update is called once per frame
    private void Update()
    {
        transform.position = new Vector3(
            Mathf.Clamp(player.position.x,leftClamp, rightClamp),
            Mathf.Clamp(player.position.y,downClamp, upClamp),
            -10);
    }
}
