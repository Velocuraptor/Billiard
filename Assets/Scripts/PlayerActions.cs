using System;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    public static bool IsAction;


    public delegate void OnTap(Vector2 tapPoint);
    public event Action DropEvent;
    public event OnTap TapEvent;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            IsAction = true;
            TapEvent?.Invoke(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
        else if (Input.GetMouseButtonUp(0))
        {
            IsAction = false;
            DropEvent?.Invoke();
        }
    }
}
