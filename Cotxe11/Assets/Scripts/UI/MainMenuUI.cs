using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{

    public GameObject main_menu_ui = null;
    public GameObject controlls_ui = null;
    public GameObject credits_ui = null;
    public AudioSource aud_source = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayButton()
    {
        aud_source.Play();
        SceneManager.LoadScene("Tutorial_level", LoadSceneMode.Single);
    }

    public void ControllsButton()
    {
        aud_source.Play();
        controlls_ui.SetActive(true);
        main_menu_ui.SetActive(false);
    }

    public void CreditsButton()
    {
        aud_source.Play();
        credits_ui.SetActive(true);
        main_menu_ui.SetActive(false);
    }

    public void ExitButton()
    {
        aud_source.Play();
        Application.Quit();
    }

    public void GoBack()
    {
        aud_source.Play();
        main_menu_ui.SetActive(true);
        transform.parent.gameObject.SetActive(false);
    }

}
