using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid_generator : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject grid_object = null;
    public int size_x = 0;
    public int size_y = 0;
    private Transform temp_transform;

    void Start()
    {
        GameObject emptyGO = new GameObject();
        temp_transform = emptyGO.transform;

        for (int j = 0; j< size_x; j++)
        {
            for (int i = 0; i < size_y; i++)
            {
                temp_transform.position = new Vector3(i, j, 0);
                Instantiate(grid_object, temp_transform.position, Quaternion.identity);
            }
        }
    }

}
