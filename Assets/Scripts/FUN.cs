using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FUN : MonoBehaviour
{
    
    public Transform target;
    public Transform ball;
    public float speed = 100;

    void Awake()
    {
        
    }

    void Update()
    {
        ball.position = new Vector3 (target.position.x*speed, transform.position.y, target.position.z*speed);
        
    }
}
