using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{

    private float lenght = 0.0f;
    private float start_pos = 0.0f;
    private float start_pos_y = 0.0f;
    public bool has2move_in_y = false;
    public GameObject camera = null;
    public float parallax_amount = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        start_pos = transform.position.x;
        start_pos_y = transform.position.y;
        lenght = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = camera.transform.position.x * parallax_amount;
        float new_y = transform.position.y;
        if (has2move_in_y == false)
        {
            new_y = start_pos_y;
        }
        transform.position = new Vector3(start_pos + distance, new_y, transform.position.z);
    }
}
