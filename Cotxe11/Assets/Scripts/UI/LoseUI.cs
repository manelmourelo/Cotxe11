﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseUI : MonoBehaviour
{

    public GameObject Ghost = null;
    public GameObject plant = null;
    public AudioSource camera_audio = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RestartDay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void RestartNight()
    {
        Ghost.transform.position = plant.transform.position;
        Ghost.GetComponent<FlyEnergy>().progress_bar.fillAmount = PlayerPrefs.GetFloat("FlyEnergy");
        Ghost.GetComponent<FlyEnergy>().enough_energy = true;
        transform.parent.gameObject.SetActive(false);
        camera_audio.Play();
    }

}
