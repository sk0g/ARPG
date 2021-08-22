﻿using UnityEngine;

namespace MoreMountains.NiceVibrations
{
public class BallDemoManager : DemoManager
{
    [Header("Ball")] public Vector2 Gravity = new Vector2(0, -30f);

    protected virtual void Start()
    {
        Physics2D.gravity = Gravity;
    }
}
}