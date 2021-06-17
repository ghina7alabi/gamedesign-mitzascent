using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScript : MonoBehaviour
{
    public static bool isEasy;

    private void Start()
    {
        isEasy = true;
    }

    public void PlayButton()
    {
        SceneManager.LoadScene("NewScene", LoadSceneMode.Single);
    }

    public void MenuButton()
    {
        SceneManager.LoadScene("TitleScene", LoadSceneMode.Single);
    }

    public void CreditsButton()
    {
        SceneManager.LoadScene("CreditsScene", LoadSceneMode.Single);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void EasySwitch()
    {
        isEasy = true;
    }

    public void HardSwitch()
    {
        isEasy = false;
    }
}
