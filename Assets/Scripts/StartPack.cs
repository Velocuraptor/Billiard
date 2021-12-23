using System.Collections.Generic;
using UnityEngine;

public class StartPack : MonoBehaviour
{
    [SerializeField] private List<GameObject> _balls;
    public List<GameObject> Balls => _balls;
}