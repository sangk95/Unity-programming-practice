using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class KeyController : MonoBehaviour, IGameController
{
    public Action FireButtonPressed;
    
    void Update() 
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(FireButtonPressed != null)
            {
                FireButtonPressed();
            }
        }
    }
}
