using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsScript : MonoBehaviour
{
    public void PlayClick()
    {
        AudioManager.Instance.PlaySFXSound(AudioManager.Instance.clickSound);
    }
}
