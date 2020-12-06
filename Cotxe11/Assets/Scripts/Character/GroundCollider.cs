using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCollider : MonoBehaviour
{

    private GameObject Player = null;

    // Start is called before the first frame update
    void Start()
    {
        Player = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            Player.GetComponent<CharacterController>().on_air = false;
            Player.GetComponent<CharacterController>().current_jumps = 0;
        }
    }

}
