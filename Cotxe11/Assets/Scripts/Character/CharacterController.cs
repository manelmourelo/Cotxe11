using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float speed = 1.0f;
    public float jump_force = 200.0f;
    public float max_speed = 5.0f;
    public float bounce_multiplier = 3.0f;
    public float climb_speed = 2.0f;


    private bool facing_right = true;
    private bool on_air = false;
    public bool can_move = true;
    public bool can_climb = false;

    private int current_jumps = 0;

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
            if (can_move == true) {
                Vector3 movement = new Vector3(1.0f, 0.0f, 0.0f);
                transform.position += movement * speed * Time.deltaTime;
            }
        }

        if (Input.GetKey("a"))
        {
            if (facing_right == true)
            {
                Flip();
                facing_right = false;
            }
            if (can_move == true) {
                Vector3 movement = new Vector3(-1.0f, 0.0f, 0.0f);
                transform.position += movement * speed * Time.deltaTime;
            }
        }

        if (Input.GetButtonDown("Jump") && current_jumps < 2 && !can_climb)
        {
            current_jumps++;
            on_air = true;
            character_rb.velocity = new Vector2(0.0f,0.0f);
            character_rb.AddForce(Vector2.up * jump_force, ForceMode2D.Impulse);
        }

        if (can_climb)
        {
            if (Input.GetKey("space"))
            {
                Vector3 movement = new Vector3(0.0f, 1.0f, 0.0f);
                transform.position += (movement * climb_speed * Time.deltaTime);
            }

            if (Input.GetKey("s"))
            {
                Vector3 movement = new Vector3(0.0f, -1.0f, 0.0f);
                transform.position += movement * climb_speed * Time.deltaTime;
            }
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
            current_jumps = 0;
            on_air = false;
        }


        if (collision.gameObject.tag == "Bounce")
        {
            on_air = true;
            character_rb.velocity = new Vector2(0.0f, 0.0f);
            character_rb.AddForce(Vector2.up * jump_force * bounce_multiplier, ForceMode2D.Impulse);
        }
    }

}
