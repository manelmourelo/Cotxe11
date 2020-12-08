using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ChangeWorld : MonoBehaviour
{
    public GameObject PlayerDay = null;
    public GameObject PlayerNight = null;
    public GameObject CMCamera = null;
    public GameObject WinCollider = null;
    public AudioClip grab_audio = null;
    public AudioClip night_music = null;

    private CinemachineVirtualCamera CMVirtualCam;

    private bool both_players_in_scene = false;
    public Light level_light = null;
    public Vector4 night_light_color;

    public GameObject day_background = null;
    public GameObject night_background = null;

    // Start is called before the first frame update
    void Start()
    {
        CMVirtualCam = CMCamera.GetComponent<CinemachineVirtualCamera>();
        WinCollider.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (both_players_in_scene == true)
        {
            if (PlayerDay.GetComponent<CharacterController>().is_in_camera == true)
            {
                if (PlayerDay.GetComponent<CharacterController>().facing_right == true)
                {
                    PlayerDay.GetComponent<CharacterController>().facing_right = false;
                    PlayerDay.GetComponent<CharacterController>().Flip();
                }
                Vector3 movement = new Vector3(-1.0f, 0.0f, 0.0f);
                PlayerDay.transform.position += movement * PlayerDay.GetComponent<CharacterController>().speed * Time.deltaTime;
            }
            else
            {
                PlayerNight.gameObject.GetComponent<BoxCollider2D>().enabled = true;
                PlayerNight.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
                PlayerNight.GetComponent<GhostController>().other_player_is_in_camera = false;
                PlayerDay.SetActive(false);
                CMVirtualCam.m_Lens.OrthographicSize = 9.0f;
                transform.gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            night_background.GetComponent<SpriteRenderer>().enabled = true;
            foreach (Transform child in night_background.transform)
            {
                child.GetComponent<SpriteRenderer>().enabled = true;
            }
            day_background.SetActive(false);
            //PlayerDay.SetActive(false);
            PlayerDay.GetComponent<CharacterController>().other_player_is_in_camera = true;
            PlayerNight.SetActive(true);
            PlayerNight.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            PlayerNight.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
            WinCollider.SetActive(true);

            PlayerPrefs.SetFloat("FlyEnergy", PlayerDay.GetComponent<ProgressBar>().progress_bar.fillAmount);

            CMVirtualCam.Follow = PlayerNight.transform;
            //transform.gameObject.SetActive(false);
            transform.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            transform.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            CMCamera.GetComponent<AudioSource>().clip = night_music;
            CMCamera.GetComponent<AudioSource>().Play();

            PlayerNight.GetComponent<AudioSource>().clip = grab_audio;
            PlayerNight.GetComponent<AudioSource>().Play();
            level_light.color = night_light_color;

            both_players_in_scene = true;

        }
    }


}
