using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float speed = 1.0f;
    public float jump_force = 200.0f;
    public float max_speed = 5.0f;

    private bool facing_right = true;
    private bool on_air = false;

    private Rigidbody2D character_rb = null;

    // Start is called before the first frame update
    void Start()
    {
        character_rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Inputs

        if (Input.GetKey("d"))
        {
            character_rb.AddForce(new Vector2(speed, 0f));
            if (character_rb.velocity.x > max_speed)
            {
                character_rb.velocity = character_rb.velocity.normalized * max_speed;
            }
            if (facing_right == false)
            {
                Flip();
                facing_right = true;
            }
        }

        if (Input.GetKey("a"))
        {
            character_rb.AddForce(new Vector2(-speed, 0f));
            if (character_rb.velocity.x < -max_speed)
            {
                character_rb.velocity = character_rb.velocity.normalized * max_speed;
            }
            if (facing_right == true)
            {
                Flip();
                facing_right = false;
            }
        }

        if (Input.GetButtonDown("Jump") && on_air == false)
        {
            on_air = true;
            character_rb.AddForce(new Vector2(0f, jump_force));
        }


    }

    private void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            on_air = false;
        }
    }

}
