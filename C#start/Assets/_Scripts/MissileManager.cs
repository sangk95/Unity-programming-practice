using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileManager : MonoBehaviour
{
    Factory missileFactory;
    BuildingManager buildingManager; 
    bool isInitialized = false;
    int maxMissileCount = 20;
    int currentMissileCount;
    float missileSpawnInterval = 0.5f;
    Coroutine spawningMissile; 
    public void Initialize(Factory missileFactory, BuildingManager buildingManager, int maxMissileCount, float missileSpawnInterval)
    {
        if(isInitialized)
            return;
        this.missileFactory = missileFactory;
        this.buildingManager = buildingManager;
        this.maxMissileCount = maxMissileCount;
        this.missileSpawnInterval = missileSpawnInterval; 

        Debug.Assert(this.missileFactory != null, "missile factory is null!");
        Debug.Assert(this.buildingManager != null, "building manager is null!"); 

        isInitialized = true;
    }
    public void OnGameStarted()
    {
        currentMissileCount = 0;
        spawningMissile = StartCoroutine(AutoSpawnMissile());
    }
    IEnumerator AutoSpawnMissile()
    {
        while(currentMissileCount < maxMissileCount) 
        {
            yield return new WaitForSeconds(missileSpawnInterval); 
            if (!buildingManager.HasBuilding) 
            {
                Debug.LogWarning("all buildings are destroyed!");
                yield break;
            }

            SpawnMissile();
        }
    }

    void SpawnMissile() 
    {
        Debug.Assert(this.missileFactory != null, "missile factory is null!");
        Debug.Assert(this.buildingManager != null, "building manager is null!");

        RecycleObject missile = missileFactory.Get();
        missile.Activate(GetMissileSpawnPosition(), buildingManager.GetRandomBuildingPosition()); 

        missile.Destroyed += this.OnMissileDestroyed;

        currentMissileCount++;
    }
    void OnMissileDestroyed(RecycleObject missile) 
    {
        missile.Destroyed -= this.OnMissileDestroyed;
        missileFactory.Restore(missile);
    }

    Vector3 GetMissileSpawnPosition() 
    {
        Vector3 spawnPosition = Vector3.zero;
        spawnPosition.x = Random.Range(0f, 1f);
        spawnPosition.y = 1f; 

        spawnPosition = Camera.main.ViewportToWorldPoint(spawnPosition);
        
        spawnPosition.z = 0f;
        return spawnPosition;
    }
}
