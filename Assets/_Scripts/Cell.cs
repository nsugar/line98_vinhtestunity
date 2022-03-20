using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : Shape
{
    public Ball ball;

    void Start()
    {

    }

    void Update()
    {

    }

    public void AttachBall(Ball ball)
    {
        this.ball = ball;
    }

    public bool HasBall()
    {
        if (ball != null)
        {
            return true;
        }

        return false;
    }
}
