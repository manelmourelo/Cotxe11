using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{

    public Image progress_bar;

    public float max_orbs = 5;

    public float current_orbs = 0;

    public AudioClip grab_audio = null;

    private float timer = 0.0f;
    private bool has_collided = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        progress_bar.fillAmount = current_orbs/max_orbs;

        //if(Input.GetKeyDown("q")){
        //    current_orbs++;
        //}

        if (has_collided == true)
        {
            timer += Time.deltaTime;
            if (timer >= 0.2f)
            {
                has_collided = false;
                timer = 0.0f;
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Orb" && has_collided == false)
        {
            Destroy(collision.gameObject);
            current_orbs++;
            GetComponent<AudioSource>().clip = grab_audio;
            GetComponent<AudioSource>().Play();
            has_collided = true;
            timer = 0.0f;
        }
    }

}
