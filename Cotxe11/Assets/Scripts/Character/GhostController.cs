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
    public GameObject WinUI = null;

    public AudioClip land_audio = null;
    public AudioClip death_audio = null;
    public AudioClip glide_audio = null;

    public bool is_dead = false;

    public bool other_player_is_in_camera = true;
    private Animator ghost_animator = null;

    // Start is called before the first frame update
    void Start()
    {
        character_rb = GetComponent<Rigidbody2D>();
        ghost_animator = GetComponent<Animator>();
        Flip();
    }

    // Update is called once per frame
    void Update()
    {
        //Inputs
        if (other_player_is_in_camera == false && Time.timeScale == 1) {
            if (loseUI.activeSelf == false && WinUI.activeSelf == false) {
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
                    GetComponent<AudioSource>().clip = glide_audio;
                    GetComponent<AudioSource>().Play();
                }
                if (Input.GetKey("space") && transform.gameObject.GetComponent<FlyEnergy>().enough_energy == true)
                {
                    ghost_animator.SetBool("is_flying", true);
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
                    ghost_animator.SetBool("is_flying", false);
                    transform.gameObject.GetComponent<FlyEnergy>().StopFly();
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Bounce")
        {
            on_air = false;
            if (transform.gameObject.GetComponent<FlyEnergy>().enough_energy == false)
            {
                loseUI.SetActive(true);
            }
            else
            {
                if (is_dead == false) {
                    GetComponent<AudioSource>().clip = land_audio;
                    GetComponent<AudioSource>().Play();
                }
            }
        }

        //Probably will not need
        // if (collision.gameObject.tag == "Bounce")
        // {   
        // 
        //
        //     on_air = true;
        //     character_rb.velocity = new Vector2(0.0f, 0.0f);
        //     character_rb.AddForce(Vector2.up * jump_force * bounce_multiplier, ForceMode2D.Impulse);
        // }
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
            GetComponent<AudioSource>().clip = death_audio;
            GetComponent<AudioSource>().Play();
            is_dead = true;
            loseUI.SetActive(true);
        }

        if (collision.gameObject.tag == "Win")
        {
            WinUI.SetActive(true);
        }
    }

}
