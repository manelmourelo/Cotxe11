using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuUI : MonoBehaviour
{

    public AudioSource aud_source = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Continue()
    {
        aud_source.Play();
        Time.timeScale = 1;
        transform.parent.gameObject.SetActive(false);
    }

    public void RestartLevel()
    {
        aud_source.Play();
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitGame()
    {
        aud_source.Play();
        Application.Quit();
    }

}
