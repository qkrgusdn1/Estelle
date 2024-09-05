using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallHeartMgr : MonoBehaviour
{
    public GameObject objectToSpawn;
    public GameObject objectToSpawn1;
    public float spawnDuration;
    public float spawnHeight;
    public float spawnTime;
    private float elapsedTime;
    void Start()
    {
        InvokeRepeating("SpawnObject", 0, spawnTime);
    }

    void SpawnObject()
    {
        Vector3 cameraBottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 cameraTopRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));

        float randomX = Random.Range(cameraBottomLeft.x, cameraTopRight.x);
        Vector3 spawnPosition = new Vector3(randomX, cameraTopRight.y + spawnHeight, 0);

        float t = Mathf.Clamp01(elapsedTime / spawnDuration);
        float probabilityObjectToSpawn1 = Mathf.Lerp(0.1f, 0.9f, t);

        GameObject objectToInstantiate = Random.value < probabilityObjectToSpawn1 ? objectToSpawn1 : objectToSpawn;
        Instantiate(objectToInstantiate, spawnPosition, Quaternion.identity);
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
    }
}
