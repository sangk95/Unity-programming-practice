using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEffect : RecycleObject
{
    [SerializeField]
    float effectTime = 0.5f;
    float elapsedTime = 0f;

    void Update()
    {
        if(!isActivated)
            return;

        elapsedTime += Time.deltaTime;
        if(elapsedTime >= effectTime)
        {
            elapsedTime = 0f;
            isActivated = false;
            Destroyed?.Invoke(this);
        }
    } 
}
