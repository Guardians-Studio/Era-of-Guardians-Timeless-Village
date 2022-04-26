using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cineTuronTP : MonoBehaviour
{
    public float timer = 4f;
    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
            SceneManager.LoadScene("turon");
    }
}
