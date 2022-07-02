using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    BulletLauncher launcherPrefab; 
    BulletLauncher launcher;
    void Start()
    {
        launcher = Instantiate(launcherPrefab);
        MouseController mouseController = gameObject.AddComponent<MouseController>();
        mouseController.FireButtonPressed += launcher.OnFireButtonPressed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
