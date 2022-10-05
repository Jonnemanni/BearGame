using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject controls;
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void ShowControls()
    {
        mainMenu.SetActive(false);
        controls.SetActive(true);
    }
    public void HideControls()
    {
        mainMenu.SetActive(true);
        controls.SetActive(false);
    }
}
