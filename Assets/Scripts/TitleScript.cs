using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScript : MonoBehaviour
{

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
        PlayerPrefs.SetInt("difficulty", 1);
    }

    public void HardSwitch()
    {
        PlayerPrefs.SetInt("difficulty", 2);
    }
}
