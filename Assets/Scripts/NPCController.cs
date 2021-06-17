using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Narrate;
using Cinemachine;

public class NPCController : MonoBehaviour
{
    public Narration initialSpeech;
    public Narration repetitiveSpeech;
    public Narration powerupSpeech; //from playerController
    public Narration notEnoughSpeech; //from playerController

    public bool playedInitialSpeech;

    public GameObject pressSpacePrompt;
    bool nearPlayer;

    public string name;
    bool showCutscene = false;

    public GameObject cutscene1, cutscene2;



    // Update is called once per frame
    void Update()
    {

        ////testing
        //PlatformScript.gotMitz = true;
        //PlayerController.gotMitz = true;
        //ReticleScript.gotMitz = true;

        if (nearPlayer & Input.GetKeyDown(KeyCode.E))
        {
            if (!playedInitialSpeech)
            {
                NarrationManager.instance.PlayNarration(initialSpeech);
                playedInitialSpeech = true;
            }
            else if (playedInitialSpeech)
            {
                NarrationManager.instance.PlayNarration(repetitiveSpeech);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            pressSpacePrompt.SetActive(true);
            nearPlayer = true;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            pressSpacePrompt.SetActive(false);
            nearPlayer = false;

            if (name == "priest1" && playedInitialSpeech && !showCutscene)
            {
                StartCoroutine(Cutscene());
            }
        }
        
    }

    IEnumerator Cutscene()
    {
        showCutscene = true;

        cutscene1.SetActive(true);
        yield return new WaitForSeconds(5f);
        cutscene1.SetActive(false);

        cutscene2.SetActive(true);
        yield return new WaitForSeconds(5f);
        cutscene2.SetActive(false);

        PlatformScript.gotMitz = true;
        PlayerController.gotMitz = true;
        ReticleScript.gotMitz = true;
    }

}
