using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bullet : RecycleObject
{
    [SerializeField]
    float speed = 5f;
    void Start()
    {
        
    }

    bool IsArrivedToTarget()
    {
        float distance = Vector3.Distance(transform.position, targetPosition);
        return distance < 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isActivated)
            return;
        transform.position += transform.up * speed * Time.deltaTime;

        if(IsArrivedToTarget())
        {
            isActivated = false;
            if(Destroyed != null)
            {
                Destroyed(this);
            }
        }
    }
}
