using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    BulletLauncher launcherPrefab; 
    BulletLauncher launcher;
    [SerializeField]
    Transform launcherLocator;
    [SerializeField]
    Building buildingPrefab;
    [SerializeField]
    Transform[] buildingLocators;
    BuildingManager buildingManager;
    [SerializeField]
    Missile missilePrefab;
    [SerializeField]
    DestroyEffect effectPrefab; 
    [SerializeField]
    int maxMissileCount = 20;
    [SerializeField]
    float missileSpawnInterval = 0.5f;
    MissileManager missileManager;
    TimeManager timeManager;
    MouseController mouseController;
    void Start()
    {
        launcher = Instantiate(launcherPrefab);
        launcher.transform.position = launcherLocator.position;
        mouseController = gameObject.AddComponent<MouseController>();
        buildingManager = new BuildingManager(buildingPrefab, buildingLocators, new Factory(effectPrefab, 2));
        timeManager = gameObject.AddComponent<TimeManager>();
        missileManager = gameObject.AddComponent<MissileManager>();
        missileManager.Initialize(new Factory(missilePrefab), buildingManager, maxMissileCount, missileSpawnInterval);

        BindEvents();

        timeManager.StartGame(1f);
    }

    void BindEvents() 
    {
        mouseController.FireButtonPressed += launcher.OnFireButtonPressed;
        timeManager.GameStarted += buildingManager.OnGameStarted;
        timeManager.GameStarted += launcher.OnGameStarted;
        timeManager.GameStarted += missileManager.OnGameStarted;
    }
    void UnBindEvents()
    {
        mouseController.FireButtonPressed -= launcher.OnFireButtonPressed;
        timeManager.GameStarted -= buildingManager.OnGameStarted;
        timeManager.GameStarted -= launcher.OnGameStarted;
        timeManager.GameStarted -= missileManager.OnGameStarted;
    }

    void OnDestroy()
    {
        UnBindEvents();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
