using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{

    public Image progress_bar;

    public float max_orbs = 5;

    public float current_orbs = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        progress_bar.fillAmount = current_orbs/max_orbs;

        if(Input.GetKeyDown("q")){
            current_orbs++;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Orb")
        {
            Destroy(collision.gameObject);
            current_orbs++;
        }
    }

}
