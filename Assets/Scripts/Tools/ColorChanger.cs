using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ColorChanger : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer[] myRenderers;
    [SerializeField]
    private Color myNewColor;

    private void Update()
    {
        changeColor();
    }

    private void changeColor()
    {
        for (int i = 0; i < myRenderers.Length; i++)
        {
            myRenderers[i].color = myNewColor;
        }
    }

}
