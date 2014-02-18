using UnityEngine;
using System.Collections;
using Com.Nravo.Framework;

public class SoundManagerExample : MonoBehaviour
{
    public AudioClip someClip;

    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 200, 70), "Play Sound"))
        {
            SoundManager.Instance.Play(someClip);
        }


        if (GUI.Button(new Rect(10, 100, 200, 70), "Toggle Music"))
        {
            SoundManager.Instance.ToggleMusic();
        }

        if (GUI.Button(new Rect(10, 200, 200, 70), "Toggle Sound"))
        {
            SoundManager.Instance.ToggleSound();
        }
    }
}
