using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{
    public enum Scene {
        IntroScene=0, 
        Koti=1, 
        Tori=2, 
        Kahvila=3, 
        Tykki=4, 
        LoppuNäyttö=5, 
    }
    public static void load(int index) {
        Scene scene = (Scene)index;
        SceneManager.LoadScene(scene.ToString());
    }
}
