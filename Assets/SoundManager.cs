using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    Scene scene;
    AudioSource aud;

    // Update is called once per frame
    void Update()
    {
        scene = SceneManager.GetActiveScene();
        aud = GetComponent<AudioSource>();

        DontDestroyOnLoad(aud);

        if (scene.name == "S_Task")
        {
            aud.mute = true;
        }
        else
        {
            aud.mute = false;
        }
    }
}
