using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BossCoinsHolder 
{
    [SerializeField]
    private int maxGreenCoins;

    [SerializeField]
    private int maxBlueCoins;

    [SerializeField]
    private float spawnDistance;

    [SerializeField]
    private float slowSpawnDuration;

    [SerializeField]
    private float CoinSpawnCoolDown;

    [SerializeField]
    private GameObject greenCoinPrefab;

    [SerializeField]
    private GameObject blueCoinPrefab;

    private Boss boss;

    private float xMin;
    private float xMax;
    private float yMin;
    private float yMax;

    private int greenCoinsNum = 0;

    private int blueCoinsNum = 0;

    private bool canSpawn = false;

    private List<GameObject> allColoresCoins;

    //private class CoinStatue
    //{
    //    public GameObject coin;
    //    public bool statue;
    //}

    //private List<CoinStatue> coinsStatue;

    private List<GameObject> greenCoinsList;

    private List<GameObject> blueCoinsList;

    // Start is called before the first frame update
    public void Start(Boss boss)
    {

        Debug.Log("set borders2");
        this.boss = boss;
        xMin = boss.XOriginalMin;
        xMax = boss.XOriginalMax;
        yMin = boss.YOriginalMin;
        yMax = boss.YOriginalMax;
        //coinsStatue = new List<CoinStatue>();
        allColoresCoins = new List<GameObject>();
        greenCoinsList = new List<GameObject>();
        blueCoinsList = new List<GameObject>();
        InitializeCoins();
    }

    // Update is called once per frame
    //void Update()
    //{
    //    SpawnCoin();
    //}

    public void SpawnCoin(bool isInitialized = false)
    {
        if(maxGreenCoins > 0)
        {
            SpawnCoinByColor("green", isInitialized);
        }
        if (maxBlueCoins > 0)
        {
            SpawnCoinByColor("blue", isInitialized);
        }
        //Vector3 coinPos = new Vector3(Random.Range(xMin, xMax), Random.Range(yMin, yMax), 0);
        //if (greenCoinsList.Count == 0)
        //{
        //    Debug.Log("coin range y " + yMin +" "+yMax );
        //    GameObject coin = (GameObject)MonoBehaviour.Instantiate(greenCoinPrefab, coinPos, Quaternion.identity);
        //    coin.GetComponent<Activator>().SetBossData(true, boss);
        //    if (!isInitialized)
        //    {
        //        coin.GetComponent<Activator>().ManageSlowSpawn(slowSpawnDuration);
        //    }
        //    greenCoinsList.Add(coin);

        //}
        //else if(greenCoinsList.Count < maxGreenCoins)
        //{
        //    Debug.Log("spawn coin"+ greenCoinsList.Count);
        //    bool goodDistance = true;
        //    for (int i = 0; i < greenCoinsList.Count; i++)
        //    {
        //        Vector3 pos = greenCoinsList[i].transform.position;
        //        float distance = Vector3.Distance(coinPos, pos);
        //        if(distance < spawnDistance)
        //        {
        //            goodDistance = false;

        //        }
        //    }
        //    if (goodDistance)
        //    {
        //        GameObject coin = (GameObject)MonoBehaviour.Instantiate(greenCoinPrefab, coinPos, Quaternion.identity);
        //        coin.GetComponent<Activator>().SetBossData(true, boss);
        //        if (!isInitialized)
        //        {
        //            coin.GetComponent<Activator>().ManageSlowSpawn(slowSpawnDuration);
        //        }
        //        greenCoinsList.Add(coin);
        //    }

        //}
    }

    public void SpawnCoinByColor(string color, bool isInitialized)
    {
        switch (color)
        {
            case "green":
                SpawnCoin(greenCoinsList, greenCoinPrefab, maxGreenCoins,isInitialized);
                break;
            case "blue":
                SpawnCoin(blueCoinsList, blueCoinPrefab, maxBlueCoins,isInitialized);
                break;
        }
    }

    public void SpawnCoin(List<GameObject> coinsList,GameObject coinPrefab,int maxColorCoin,bool isInitialized)
    {
        Vector3 coinPos = new Vector3(Random.Range(xMin, xMax), Random.Range(yMin, yMax), 0);
        if (coinsList.Count == 0)
        {
            Debug.Log("coin range y " + yMin + " " + yMax);
            GameObject coin = (GameObject)MonoBehaviour.Instantiate(coinPrefab, coinPos, Quaternion.identity);
            coin.GetComponent<Activator>().SetBossData(true, boss);
            if (!isInitialized)
            {
                coin.GetComponent<Activator>().ManageSlowSpawn(slowSpawnDuration);
            }
            coinsList.Add(coin);
            allColoresCoins.Add(coin);

        }
        else if (coinsList.Count < maxColorCoin)
        {
            Debug.Log("spawn coin" + coinsList.Count);
            bool goodDistance = true;
            for (int i = 0; i < coinsList.Count; i++)
            {
                Vector3 pos = coinsList[i].transform.position;
                float distance = Vector3.Distance(coinPos, pos);
                if (distance < spawnDistance)
                {
                    goodDistance = false;

                }
            }
            if (goodDistance)
            {
                GameObject coin = (GameObject)MonoBehaviour.Instantiate(coinPrefab, coinPos, Quaternion.identity);
                coin.GetComponent<Activator>().SetBossData(true, boss);
                if (!isInitialized)
                {
                    coin.GetComponent<Activator>().ManageSlowSpawn(slowSpawnDuration);
                }
                coinsList.Add(coin);
                allColoresCoins.Add(coin);
            }

        }
    }

    public void RemoveCoin(GameObject coin)
    {
        Debug.Log("green coins " + coin);
        if (coin.tag == "greenBossCoin")
        {
            greenCoinsList.Remove(coin);
            allColoresCoins.Remove(coin);
        }
        if (coin.tag == "blueBossCoin")
        {
            blueCoinsList.Remove(coin);
            allColoresCoins.Remove(coin);
        }
    }


    public void InitializeCoins()
    {
        for(int i = 0; i < maxGreenCoins; i++)
        {
            SpawnCoin(true);
        }
        for (int i = 0; i < maxBlueCoins; i++)
        {
            SpawnCoin(true);
        }
    }
}
