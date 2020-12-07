using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{

    public GameObject main_menu_ui = null;
    public GameObject controlls_ui = null;
    public GameObject credits_ui = null;

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
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }

    public void ControllsButton()
    {
        controlls_ui.SetActive(true);
        main_menu_ui.SetActive(false);
    }

    public void CreditsButton()
    {
        credits_ui.SetActive(true);
        main_menu_ui.SetActive(false);
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void GoBack()
    {
        main_menu_ui.SetActive(true);
        transform.parent.gameObject.SetActive(false);
    }

}
