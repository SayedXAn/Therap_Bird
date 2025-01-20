using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayController : MonoBehaviour
{
    Display[] displays;
    void Start()
    {
        displays = Display.displays;

        for (int i = 0; i < displays.Length; i++)
        {
            displays[i].Activate();
        }
    }
}
