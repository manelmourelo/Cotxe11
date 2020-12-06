using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlyEnergy : MonoBehaviour
{

    public Image progress_bar;

    private bool is_flying = false;
    private float timer = 0.0f;
    private float prev_time = 0.0f;
    public float current_energy_lose = 0.1f;
    public float max_energy_lose = 0.001f;

    public float energy_lose = 0.1f;
    public float energy_lose_increment = 0.1f;
    public float time2increase = 5.0f;
    public bool enough_energy = true;

    // Start is called before the first frame update
    void Start()
    {
        current_energy_lose = energy_lose;
        if (progress_bar.fillAmount >= 0.0 + current_energy_lose)
        {
            enough_energy = true;
        }
        else
        {
            enough_energy = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (is_flying == true)
        {
            timer += Time.deltaTime;
            if (timer >= prev_time + time2increase && current_energy_lose < max_energy_lose)
            {
                current_energy_lose += energy_lose_increment;
                if (current_energy_lose > max_energy_lose)
                {
                    current_energy_lose = max_energy_lose;
                }
            }
            if (progress_bar.fillAmount >= 0.0f + current_energy_lose)
            {
                progress_bar.fillAmount -= current_energy_lose;
            }
            else
            {
                enough_energy = false;
            }
        }
    }

    public void BeginFly()
    {
        timer = 0.0f;
        prev_time = 0.0f;
        current_energy_lose = energy_lose;
        is_flying = true;
    }

    public void StopFly()
    {
        timer = 0.0f;
        prev_time = 0.0f;
        current_energy_lose = energy_lose;
        is_flying = false;
    }

}
