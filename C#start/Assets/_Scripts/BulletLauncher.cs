using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLauncher : MonoBehaviour
{
    [SerializeField]
    Bullet bulletPrefab;
    Bullet bullet;
    public void OnFireButtonPressed(Vector3 position)
    {
        Debug.Log("Fired"+position);
    }
    void Update()
    {
    }
}
