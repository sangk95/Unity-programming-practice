using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
    [SerializeField]
    int scorePerMissile = 50;
    [SerializeField]
    int scorePerBuilding = 5000;
    [SerializeField]
    UIRoot uIRoot;
    public Action<bool, int> GameEnded;

    bool isAllBuildingDestroyed = false;

    MissileManager missileManager;
    TimeManager timeManager;
    MouseController mouseController;
    ScoreManager scoreManager;
    void Start()
    {
        launcher = Instantiate(launcherPrefab);
        launcher.transform.position = launcherLocator.position;
        mouseController = gameObject.AddComponent<MouseController>();
        buildingManager = new BuildingManager(buildingPrefab, buildingLocators, new Factory(effectPrefab, 2));
        timeManager = gameObject.AddComponent<TimeManager>();
        missileManager = gameObject.AddComponent<MissileManager>();
        missileManager.Initialize(new Factory(missilePrefab), buildingManager, maxMissileCount, missileSpawnInterval);
        scoreManager = new ScoreManager(scorePerMissile, scorePerBuilding);

        BindEvents();

        timeManager.StartGame(1f);
    }

    void BindEvents() 
    {
        mouseController.FireButtonPressed += launcher.OnFireButtonPressed;
        timeManager.GameStarted += buildingManager.OnGameStarted;
        timeManager.GameStarted += launcher.OnGameStarted;
        timeManager.GameStarted += missileManager.OnGameStarted;
        timeManager.GameStarted += uIRoot.OnGameStarted;
        missileManager.MissileDestroyed += scoreManager.OnMissileDestroyed;
        missileManager.AllMissilesDestroyed += this.OnAllMissileDestroyed;
        scoreManager.ScoreChanged += uIRoot.OnScoreChanged;
        buildingManager.AllBuildingsDestroyed += this.OnAllBuildingDestroyed;

        this.GameEnded += launcher.OnGameEnded;
        this.GameEnded += missileManager.OnGameEnded;
        this.GameEnded += scoreManager.OnGameEnded;
        this.GameEnded += uIRoot.OnGameEnded;
    }
    void UnBindEvents()
    {
        mouseController.FireButtonPressed -= launcher.OnFireButtonPressed;
        timeManager.GameStarted -= buildingManager.OnGameStarted;
        timeManager.GameStarted -= launcher.OnGameStarted;
        timeManager.GameStarted -= missileManager.OnGameStarted;
        timeManager.GameStarted -= uIRoot.OnGameStarted;
        missileManager.MissileDestroyed -= scoreManager.OnMissileDestroyed;
        missileManager.AllMissilesDestroyed -= this.OnAllMissileDestroyed;
        scoreManager.ScoreChanged -= uIRoot.OnScoreChanged;
        buildingManager.AllBuildingsDestroyed -= this.OnAllBuildingDestroyed;

        this.GameEnded -= launcher.OnGameEnded;
        this.GameEnded -= missileManager.OnGameEnded;
        this.GameEnded -= scoreManager.OnGameEnded;
        this.GameEnded -= uIRoot.OnGameEnded;
    }

    void OnDestroy()
    {
        UnBindEvents();   
    }

    void OnAllBuildingDestroyed()
    {
        isAllBuildingDestroyed = true;
        GameEnded?.Invoke(false, buildingManager.BuildingCount);
        AudioManager.instance.PlaySound(SoundId.GameEnd); 
    }
    void OnAllMissileDestroyed()
    {
        StartCoroutine(DelayedGameEnded()); 
    }
    IEnumerator DelayedGameEnded()
    {
        yield return null;
        if(!isAllBuildingDestroyed)
        {
            GameEnded?.Invoke(true, buildingManager.BuildingCount);
            AudioManager.instance.PlaySound(SoundId.GameEnd); 
        }
    }

}
