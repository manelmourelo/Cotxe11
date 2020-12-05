using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Remember_me : MonoBehaviour
{
    // Start is called before the first frame update

    public bool pressed = false;
    public Material pressed_mat = null;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(pressed && Input.GetKeyDown("p"))
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.green;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            pressed = true;
        }
    }
}
