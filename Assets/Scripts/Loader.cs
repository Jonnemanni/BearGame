using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{
    public enum Scene {
        SampleScene=1, TestScene=2
    }
    public static void load(int index) {
        Scene scene = (Scene)index;
        SceneManager.LoadScene(scene.ToString());
    }
}
