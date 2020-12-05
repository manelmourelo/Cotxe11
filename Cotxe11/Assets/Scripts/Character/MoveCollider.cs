using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCollider : MonoBehaviour
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
        Player.GetComponent<CharacterController>().can_move = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Player.GetComponent<CharacterController>().can_move = true;
    }

}
