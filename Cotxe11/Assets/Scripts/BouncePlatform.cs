using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePlatform : MonoBehaviour
{
    public float bounce_multiplier = 3.0f;
    private Animator animator;

    float timer = 0.0f;
    float time = 0.4f;

    bool animation_is_playing = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = transform.parent.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer >= time)
        {
            animator.SetBool("bouncing", false);
            timer = 0.0f;
            animation_is_playing = false;
        }

        if(animation_is_playing)
        timer += Time.deltaTime;

    }

    public void MakeBounceAnimation()
    {
        animator.SetBool("bouncing", true);
        animation_is_playing = true;
    }

}
