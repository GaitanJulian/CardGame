using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SceneRestart : MonoBehaviour
{
    private Vector2 repeatingPos;

    private float maxValue = -32.21f;

    private void Start()
    {
        repeatingPos = new Vector2(-10.88f, 4.55f);
    }

    private void Update()
    {
        if(transform.position.x <= maxValue)
        {
            transform.position = repeatingPos;
        }
    }


}
