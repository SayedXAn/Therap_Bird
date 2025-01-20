using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScreenMove : MonoBehaviour
{
    public float offsetZ;
    public void ScreenMovement()
    {
        Tween s = transform.DOMoveZ(offsetZ, 2f, true);
        offsetZ += 275f;
    }
}
