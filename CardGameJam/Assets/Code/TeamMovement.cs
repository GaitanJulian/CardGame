using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamMovement : MonoBehaviour
{
    private HeroAttk pjAttk;

    Rigidbody2D pjRb;

    [SerializeField] private float velocityX = 1f;

    private void Start()
    {
        pjAttk = GameObject.Find("Hero").GetComponent<HeroAttk>();

        pjRb = GetComponent<Rigidbody2D>();


    }

    private void Update()
    {
        pjRb.velocity =  new Vector2 (velocityX, 0.0f);

        if (pjAttk.isFighting == true)
        {
            pjRb.velocity = Vector2.zero;
        }
    }
}
