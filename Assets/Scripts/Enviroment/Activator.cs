using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activator : MonoBehaviour
{
    private GameObject mapManager;
    private SoundManager soundManager;

    [SerializeField]
    private string colorType;

    private bool isBossCoin;

    private Boss boss;

    // Start is called before the first frame update
    void Start()
    {
        mapManager = GameObject.Find("MapManager");
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && gameObject.GetComponent<SpriteRenderer>().color.a == 1)
        {
            mapManager.GetComponent<MapManager>().EnableDisableColor(colorType);
            soundManager.PlaySfxMusic("coin");

            if (isBossCoin)
            {
                boss.RemoveCoin(gameObject);
                Destroy(gameObject);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }

    public List<GameObject> getColoredObjects()
    {
        List<GameObject> coloredObjects = null;
        switch (colorType)
        {
            case "green":
                coloredObjects = mapManager.GetComponent<MapManager>().Greens;
                break;
        }
        return coloredObjects;
    }

    public bool getColorActiveStatus()
    {
        switch (colorType)
        {
            case "green":
                return mapManager.GetComponent<MapManager>().GreenActive;
                break;
        }
        return false;
    }

    public void SetBossData(bool isBossCoin, Boss boss)
    {
        this.isBossCoin = isBossCoin;
        this.boss = boss;
    }

    public void ManageSlowSpawn(float slowSpawnDuration)
    {
        StartCoroutine(CoinSlowSpawn(slowSpawnDuration));
    }


    private IEnumerator CoinSlowSpawn(float slowSpawnDuration)
    {
        Color color = new Color(255f, 255f, 255f, 0.5f);
        gameObject.GetComponent<SpriteRenderer>().color = color;
        yield return new WaitForSeconds(slowSpawnDuration);
        color = new Color(255f, 255f, 255f, 1f);
        gameObject.GetComponent<SpriteRenderer>().color = color;
    }
}
