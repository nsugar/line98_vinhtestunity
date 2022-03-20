using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : Shape
{
    private string[,] ballHexColorArr = new string[6, 2] {
        {"#ff0000", "Red" },
        {"#0000ff", "Blue" },
        {"#008000", "Green" },
        {"#00ffff", "Cyan" },
        {"#ffff00", "Yellow" },
        {"#8b4513", "Brown" }
    };

    void Start()
    {

    }

    void Update()
    {

    }

    public void RandomColor()
    {
        int ranNum = Random.Range(0, 6);
        string color = ballHexColorArr[ranNum, 0];
        base.SetColor(color);
        base.SetName(ballHexColorArr[ranNum, 1] + " Ball");
    }
}
