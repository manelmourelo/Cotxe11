using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ChangeWorld : MonoBehaviour
{
    public GameObject PlayerDay = null;
    public GameObject PlayerNight = null;
    public GameObject CMCamera = null;

    private CinemachineVirtualCamera CMVirtualCam;

    // Start is called before the first frame update
    void Start()
    {
        CMVirtualCam = CMCamera.GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerDay")
        {
            PlayerDay.SetActive(false);
            PlayerNight.SetActive(true);

            CMVirtualCam.Follow = PlayerNight.transform;
        }
    }


}
