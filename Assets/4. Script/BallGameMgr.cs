
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallGameMgr : MonoBehaviour
{
    private static BallGameMgr instance;
    public static BallGameMgr Instance
    { 
        get 
        { 
            return instance;
        }
    }
    [SerializeField] TMP_Text waveText;
    [SerializeField] TMP_Text waveTimeText;
    public int waveLevel;
    public int maxLevel;

    public TMP_Text letterText;
    
    
    public Heart heart;
    public float radius;
    
    public float waveTime;
    public float repeatWaveTime;
    public bool fali;

    public List<BallEnemy> ballEnemiesPoolings = new List<BallEnemy>();
    public List<Letter> letterPoolings = new List<Letter>();
    public BallEnemy[] ballEnemies;
    public WaveInfo[] waveInfos;
    private void Start()
    {
        waveLevel = 0;
        waveTime = repeatWaveTime;
        waveTimeText.text = waveTime.ToString();
        
        StartCoroutine(CoWaveTime());
        StartCoroutine(CoStartWave());
    }

    public IEnumerator CoWaveTime()
    {
        while (true)
        {
            if (waveTime <= 0)
            {
                Level();
            }
            if (waveLevel >= maxLevel)
            {
                DonDestory.Instance.gameClear = true;
                SceneManager.LoadScene("Go");
                break;
            }

            waveTime--;
            waveTimeText.text = waveTime.ToString();

           

            yield return new WaitForSeconds(1);
        }
    }

    public void Level()
    {
        waveLevel++;
        if (waveLevel >= maxLevel)
            return;
        waveText.text = "Wave" + " " + (waveLevel + 1).ToString();
        waveTime = repeatWaveTime;
        waveTimeText.text = waveTime.ToString();
        
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public BallEnemy GetBallEnemy(BallEnemyType type)
    {
        for (int i = 0; i < ballEnemiesPoolings.Count; i++)
        {
            if (!ballEnemiesPoolings[i].gameObject.activeSelf && ballEnemiesPoolings[i].ballEnemyType == type)
            {
                ballEnemiesPoolings[i].gameObject.SetActive(true);
                return ballEnemiesPoolings[i];
            }
        }

        for (int i = 0; i < ballEnemies.Length; i++)
        {
            if (ballEnemies[i].ballEnemyType == type)
            {
                BallEnemy ballEnemy = Instantiate(ballEnemies[i]);
                ballEnemiesPoolings.Add(ballEnemy);
                return ballEnemy;
            }
        }
        return null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }


    public IEnumerator CoStartWave()
    {
        Debug.Log("Starting wave...");
        float timePassed = 0;
        while (0 < waveInfos[waveLevel].spawnTime)
        {
            for (int i = 0; i < waveInfos[waveLevel].monsteSpawns.Length; i++)
            {
                float random = Random.Range(0f, 100f);
                if (random <= waveInfos[waveLevel].monsteSpawns[i].chance)
                {
                    Debug.Log("Spawning Enemy");
                    float randomAngle = Random.Range(0f, 360f);
                    Vector2 randomDirection = new Vector2(Mathf.Cos(randomAngle * Mathf.Deg2Rad), Mathf.Sin(randomAngle * Mathf.Deg2Rad));
                    Vector2 randomPosition = (Vector2)transform.position + randomDirection * radius;

                    BallEnemy ballEnemy = GetBallEnemy(waveInfos[waveLevel].monsteSpawns[i].type);
                    ballEnemy.transform.position = randomPosition;
                }
            }
            yield return new WaitForSeconds(waveInfos[waveLevel].spawnTime);
            timePassed += waveInfos[waveLevel].spawnTime;
        }
        

       
    }

}

[System.Serializable]
public class WaveInfo
{
    public BallEnemySpawn[] monsteSpawns;
    public float spawnTime;
}

[System.Serializable]
public class BallEnemySpawn
{
    public BallEnemyType type;
    public float chance;
}

public enum BallEnemyType
{
    Closer
}

