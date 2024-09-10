using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FallHeartMgr : MonoBehaviour
{
    public GameObject objectToSpawn;
    public GameObject objectToSpawn1;
    public float spawnDuration;
    public float spawnHeight;
    public float spawnTime;
    private float elapsedTime;
    public TMP_Text countText;
    private float time;
    float minusTime;

    public List<Image> hpImages = new List<Image>();
    private static FallHeartMgr instance;
    public static FallHeartMgr Instance
    {
        get
        {
            return instance;
        }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        InvokeRepeating("SpawnObject", 0, spawnTime);
        minusTime = spawnDuration;
        StartCoroutine(CoCount());
    }

    IEnumerator CoCount()
    {
        while (true)
        {
            
            if (time >= spawnDuration)
            {
                DonDestory.Instance.gameClear = true;
                SceneManager.LoadScene("Go");
            }
            
            time++;
            if(minusTime > 0)
            {
                minusTime--;
                countText.text = minusTime.ToString();
            }
            
            yield return new WaitForSeconds(1);
        }
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
