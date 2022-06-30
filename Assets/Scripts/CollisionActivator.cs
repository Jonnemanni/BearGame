using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionActivator : MonoBehaviour
{
    // The object we activate on collision with this trigger collider.
    [SerializeField] private GameObject activeObject;


    // Method that is called by a player entering this trigger, it activates another object and deactivates this one.
    private void OnTriggerEnter2D(Collider2D other) {
        activeObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
