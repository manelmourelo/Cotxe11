using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float speed = 1.0f;
    public float jump_force = 200.0f;

    private bool facing_right = true;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //Inputs

        if (Input.GetKey("d"))
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(speed, 0f));
            if (facing_right == false)
            {
                Flip();
                facing_right = true;
            }
        }

        if (Input.GetKey("a"))
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(-speed, 0f));
            if (facing_right == true)
            {
                Flip();
                facing_right = false;
            }
        }

        if (Input.GetButtonDown("Jump"))
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jump_force));
        }


    }

    private void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

}
