using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioButtons : MonoBehaviour
{
    public AudioClip buttonClip;

    public void buttonSound()
    {
        AudioManager.Instance.PlaySFX(buttonClip);
    }
}
