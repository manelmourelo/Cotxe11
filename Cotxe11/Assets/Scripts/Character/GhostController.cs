using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{

    public float speed = 1.0f;
    public float jump_force = 200.0f;
    public float max_speed = 5.0f;
    public float bounce_multiplier = 3.0f;


    private bool facing_right = true;
    private bool on_air = false;
    public bool can_move = true;

    private Rigidbody2D character_rb = null;

    public GameObject loseUI = null;

    // Start is called before the first frame update
    void Start()
    {
        character_rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Inputs
        if (loseUI.activeSelf == false) {
            if (Input.GetKey("d"))
            {
                if (facing_right == false)
                {
                    Flip();
                    facing_right = true;
                }
                if (can_move == true)
                {
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
                if (can_move == true)
                {
                    Vector3 movement = new Vector3(-1.0f, 0.0f, 0.0f);
                    transform.position += movement * speed * Time.deltaTime;
                }
            }

            if (Input.GetKeyDown("space") && transform.gameObject.GetComponent<FlyEnergy>().enough_energy == true)
            {
                transform.gameObject.GetComponent<FlyEnergy>().BeginFly();
            }
            if (Input.GetKey("space") && transform.gameObject.GetComponent<FlyEnergy>().enough_energy == true)
            {
                on_air = true;
                character_rb.velocity = new Vector2(0.0f, 0.0f);
                character_rb.AddForce(Vector2.up * jump_force, ForceMode2D.Impulse);
            }
            else if (Input.GetKey("space") && transform.gameObject.GetComponent<FlyEnergy>().enough_energy == false)
            {
                transform.gameObject.GetComponent<FlyEnergy>().StopFly();
            }
            if (Input.GetKeyUp("space"))
            {
                transform.gameObject.GetComponent<FlyEnergy>().StopFly();
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            on_air = false;
            if (transform.gameObject.GetComponent<FlyEnergy>().enough_energy == false)
            {
                loseUI.SetActive(true);
            }
        }


        if (collision.gameObject.tag == "Bounce")
        {
            on_air = true;
            character_rb.velocity = new Vector2(0.0f, 0.0f);
            character_rb.AddForce(Vector2.up * jump_force * bounce_multiplier, ForceMode2D.Impulse);
        }
    }

    private void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Death")
        {
            loseUI.SetActive(true);
        }
    }

}
