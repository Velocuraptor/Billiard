﻿using System.Collections.Generic;
using UnityEngine;

public class StartPack : MonoBehaviour
{
    [SerializeField] private List<Ball> _balls;
    public List<Ball> Balls => _balls;
}