using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterController : MonoBehaviour
{
    public float speed = 1.0f;
    public float jump_force = 200.0f;
    public float max_speed = 5.0f;
    public float climb_speed = 2.0f;
    public float climb_horizontal_impulse = 0.1f;


    public bool facing_right = true;
    public bool on_air = false;
    public bool can_move = true;
    public bool can_climb = false;
    private bool is_dead = false;

    public int current_jumps = 0;

    public AudioClip jump_audio = null;
    public AudioClip bounce_audio = null;
    public AudioClip bounce_big_audio = null;
    public AudioClip land_audio = null;
    public AudioClip double_jump_audio = null;
    public AudioClip death_audio = null;

    private Rigidbody2D character_rb = null;
    private Vector2 default_gravity = Vector2.zero;

    public bool is_in_camera = true;
    public bool other_player_is_in_camera = false;

    private Animator character_animator = null;
    public bool has_landed = false;
    public float timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        character_rb = GetComponent<Rigidbody2D>();
        default_gravity = Physics2D.gravity;
        character_animator = transform.gameObject.GetComponent<Animator>();
        Flip();
    }

    // Update is called once per frame
    void Update()
    {

        if (has_landed == true)
        {
            timer += Time.deltaTime;
            if (timer >= 0.05f)
            {
                timer = 0.0f;
                has_landed = false;
                character_animator.SetBool("landed", true);
            }
        }

        //Inputs

        if (other_player_is_in_camera == false && Time.timeScale == 1) {
            if (is_dead == true)
            {
                if (GetComponent<AudioSource>().isPlaying == false)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
            }
            else
            {
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

                if (Input.GetButtonDown("Jump") && current_jumps < 2 && !can_climb)
                {
                    character_animator.SetBool("jump", true);
                    character_animator.SetBool("landed", false);
                    on_air = true;
                    character_rb.velocity = new Vector2(0.0f, 0.0f);
                    character_rb.AddForce(Vector2.up * jump_force, ForceMode2D.Impulse);
                    if (current_jumps == 0)
                    {
                        GetComponent<AudioSource>().clip = jump_audio;
                        GetComponent<AudioSource>().Play();
                    }
                    else
                    {
                        GetComponent<AudioSource>().clip = double_jump_audio;
                        GetComponent<AudioSource>().Play();
                    }
                    current_jumps++;
                }

                if (can_climb)
                {

                    current_jumps = 1;
                    on_air = true;

                    if (Input.GetKey("w"))
                    {
                        Vector3 movement = new Vector3(0.0f, 1.0f, 0.0f);
                        transform.position += (movement * climb_speed * Time.deltaTime);
                    }

                    if (Input.GetKey("s"))
                    {
                        Vector3 movement = new Vector3(0.0f, -1.0f, 0.0f);
                        transform.position += movement * climb_speed * Time.deltaTime;
                    }

                    if ((Input.GetKeyDown("d") && facing_right) || (Input.GetKeyDown("a") && !facing_right))
                    {

                        character_rb.velocity = new Vector2(0.0f, 0.0f);

                        if (facing_right)
                        {
                            character_rb.AddForce(new Vector2(-climb_horizontal_impulse, 1f) * jump_force, ForceMode2D.Impulse);
                            facing_right = false;
                            Flip();
                        }

                        else
                        {
                            character_rb.AddForce(new Vector2(climb_horizontal_impulse, 1f) * jump_force, ForceMode2D.Impulse);
                            facing_right = true;
                            Flip();
                        }
                    }
                }
            }
        }
    }

    public void Flip()
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
            has_landed = true;
            timer = 0.0f;
            character_animator.SetBool("jump", false);
            GetComponent<AudioSource>().clip = land_audio;
            GetComponent<AudioSource>().Play();
        }


        if (collision.gameObject.tag == "Bounce")
        {
            on_air = true;
            character_rb.velocity = new Vector2(0.0f, 0.0f);

            //Get the bounce multiplier from the Bounce platform object
            BouncePlatform bounce_platform = collision.gameObject.GetComponent<BouncePlatform>();
            float bounce_multiplier = 3.0f;
            bounce_multiplier = bounce_platform.bounce_multiplier;

            character_rb.AddForce(Vector2.up * jump_force * bounce_multiplier, ForceMode2D.Impulse);
            if (bounce_multiplier == 1.25f)
            {
                GetComponent<AudioSource>().clip = bounce_audio;
            }
            else
            {
                GetComponent<AudioSource>().clip = bounce_big_audio;
            }
            GetComponent<AudioSource>().Play();

            //Restore double jump
            current_jumps = 1;

            bounce_platform.MakeBounceAnimation();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Death")
        {
            GetComponent<AudioSource>().clip = death_audio;
            GetComponent<AudioSource>().Play();
            Physics2D.gravity = default_gravity;
            is_dead = true;
        }
    }

    private void OnBecameInvisible()
    {
        is_in_camera = false;
    }

}
