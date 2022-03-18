using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {

    }

    public void SetBallColor(GameObject go, string hexColor)
    {
        Color c;
        if (ColorUtility.TryParseHtmlString(hexColor, out c))
        {
            GetComponent<Renderer>().material.color = c;
        }
    }
}
