using System;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    private bool _isMobilePlatform;

    public delegate void OnTap(Vector2 tapPoint);

    public event Action DropEvent;
    public event OnTap TapEvent;

    private void Update()
    {
        //TapEvent?.Invoke(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        if (Input.GetMouseButton(0))
        {
            TapEvent?.Invoke(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
        else if (Input.GetMouseButtonUp(0))
        {
            DropEvent?.Invoke();
        }
    }
}
