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
            if (Player.GetComponent<CharacterController>() != null) {
                Player.GetComponent<CharacterController>().can_climb = true;
                Player.GetComponent<CharacterController>().can_move = false;
                Physics2D.gravity = new Vector2(0, 0);
                Player.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);
            }
        }

        if (collision.gameObject.tag == "Wall") 
        {
            if (Player.GetComponent<CharacterController>() != null) {
                Player.GetComponent<CharacterController>().can_move = false;
            }
            else if (Player.GetComponent<GhostController>() != null)
            {
                Player.GetComponent<GhostController>().can_move = false;
            }
        }
    } 

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Climb")
        {
            if (Player.GetComponent<CharacterController>() != null)
            {
                Player.GetComponent<CharacterController>().can_climb = false;
                Player.GetComponent<CharacterController>().can_move = true;
                Physics2D.gravity = default_gravity;
            }
        }

        if (collision.gameObject.tag == "Wall")
        {
            if (Player.GetComponent<CharacterController>() != null)
            {
                Player.GetComponent<CharacterController>().can_move = true;
            }
            else if (Player.GetComponent<GhostController>() != null)
            {
                Player.GetComponent<GhostController>().can_move = true;
            }
        }
    }

}
