using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    /*
        IntroScene=0, 
        Koti=1, 
        Tori=2, 
        Kahvila=3, 
        Tykki=4,
        Loppu=5,
    */
    [SerializeField] private int index;

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
