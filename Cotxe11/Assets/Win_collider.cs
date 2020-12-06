using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win_collider : MonoBehaviour
{
   public string next_level = null;
   public void Charge_level ()
    {
        SceneManager.LoadScene(next_level, LoadSceneMode.Single);
    }
}
