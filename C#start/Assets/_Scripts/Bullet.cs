using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bullet : RecycleObject
{
    [SerializeField]
    float speed = 5f;
    Vector3 targetPosition;
    bool isActivated = false;
    public Action<Bullet> Destroyed;
    void Start()
    {
        
    }
    public void Activate(Vector3 startPosition, Vector3 targetPosition) 
    {
        transform.position = startPosition;
        this.targetPosition = targetPosition;
        Vector3 dir = (targetPosition - startPosition).normalized;
        transform.rotation = Quaternion.LookRotation(transform.forward, dir);
        isActivated = true;
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
