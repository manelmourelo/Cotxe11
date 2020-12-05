using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float speed = 1.0f;
    public float jump_force = 200.0f;
    public float max_speed = 5.0f;

    private bool facing_right = true;
    public bool on_air = false;
    public bool can_move = true;

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
            if (facing_right == false)
            {
                Flip();
                facing_right = true;
            }
            character_rb.velocity = new Vector2(speed, character_rb.velocity.y);
        }

        if (Input.GetKey("a"))
        {
            if (facing_right == true)
            {
                Flip();
                facing_right = false;
            }
            character_rb.velocity = new Vector2(-speed, character_rb.velocity.y);
        }
        if (Input.GetKeyUp("a") || Input.GetKeyUp("d"))
        {
            character_rb.velocity = new Vector2(0.0f, character_rb.velocity.y);
        }

    if (Input.GetButtonDown("Jump") && on_air == false)
        {
            on_air = true;
            character_rb.AddForce(Vector2.up * jump_force, ForceMode2D.Impulse);
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
