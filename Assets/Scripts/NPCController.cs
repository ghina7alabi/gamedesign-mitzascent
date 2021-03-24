using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Narrate;

public class NPCController : MonoBehaviour
{
    public Narration initialSpeech;
    public Narration repetitiveSpeech;
    public Narration powerupSpeech; //from playerController
    public Narration notEnoughSpeech; //from playerController

    public bool playedInitialSpeech;

    public GameObject pressSpacePrompt;
    bool nearPlayer;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (nearPlayer & Input.GetKeyDown(KeyCode.Return))
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
        }
    }

}
