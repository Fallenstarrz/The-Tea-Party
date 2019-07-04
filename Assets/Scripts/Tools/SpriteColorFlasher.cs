using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteColorFlasher : MonoBehaviour
{
    private SpriteRenderer mySpRender;
    private float r, g, b, a;

    [SerializeField]
    private float minOpacity;
    [SerializeField]
    private float maxOpacity;

    [SerializeField]
    private float flashSpeed;

    private bool goingUp;

    // Use this for initialization
    void Start()
    {
        mySpRender = GetComponent<SpriteRenderer>();
        r = mySpRender.color.r;
        g = mySpRender.color.g;
        b = mySpRender.color.b;
        a = mySpRender.color.a;
    }

    // Update is called once per frame
    void Update()
    {
        modulateOpacity();
    }

    private void modulateOpacity()
    {
        if (a <= minOpacity)
        {
            goingUp = true;
        }
        else if (a >= maxOpacity)
        {
            goingUp = false;
        }
        if (goingUp == true)
        {
            a += flashSpeed * Time.deltaTime;
        }
        else
        {
            a -= flashSpeed * Time.deltaTime;
        }
        mySpRender.color = new Color(r, g, b, a);
    }
}
