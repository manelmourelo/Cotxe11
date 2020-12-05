using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCollider : MonoBehaviour
{

    private GameObject Player = null;
    private Vector2 default_gravity;

    // Start is called before the first frame update
    void Start()
    {
        Player = transform.parent.gameObject;
        default_gravity = Physics2D.gravity;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Climb")
        {
            Player.GetComponent<CharacterController>().can_climb = true;
            Physics2D.gravity = new Vector2 (0,0);
            Player.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);
        }

        Player.GetComponent<CharacterController>().can_move = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Climb")
        {
            Player.GetComponent<CharacterController>().can_climb = false;
            Physics2D.gravity = default_gravity;
        }

        Player.GetComponent<CharacterController>().can_move = true;
    }

}
