﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalLevelController : MonoBehaviour
{
    public Dialog dialog;
    public Dialog dyingDialog;
    public AudioClip sadBossDeath;
    public AudioClip bossMusic;
    bool endingTriggered;
    AudioSource audio;
    // Start is called before the first frame update
    void Start()
    { 
        endingTriggered = false;
        Player.Instance.currentLevel = "FinalLevel";
        FindObjectOfType<DialogManager>().StartDialog(this.dialog);
        audio = GetComponent<AudioSource>();
        audio.clip = bossMusic;
        audio.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.Instance.transform.position.y < -5f)
            Player.Instance.TakeDamage(100);

        CreditsManager.gameFinished = Hippity.HippityIsDead;

        if (Hippity.HippityIsDead && !endingTriggered) 
        {
            endingTriggered = true;
            StartCoroutine(AfterBossFight());
        }
    }

    IEnumerator AfterBossFight() 
    {
        FindObjectOfType<DialogManager>().StartDialog(this.dyingDialog);
        yield return new WaitForSeconds(10f);
        //AudioSource audio = GetComponent<AudioSource>();
        audio.clip = sadBossDeath;
        audio.Play();
        yield return new WaitForSeconds(10f);
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("Credits");
    }

}
