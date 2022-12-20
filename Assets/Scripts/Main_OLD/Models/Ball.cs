using System;
using System.Collections.Generic;
using UnityEngine;

public class Ball
{
    public Sprite ballSprite;
    public float ballMassa;
    public Enums.BallType ballTypeEnum;

    public Ball(float massa, Enums.BallType type)
    {
        ballTypeEnum = type;
        ballMassa = massa;
        ballSprite = Resources.Load<Sprite>("Sprites/" + ballTypeEnum);
    }
}