using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Boss : MonoBehaviour
{
    [SerializeField]
    private bool isSpawnCoins;

    [SerializeField]
    private BossCoinsHolder coinsHolder;

    protected float xOriginalMin;
    protected float xOriginalMax;

    [SerializeField]
    protected float yOriginalMax;

    [SerializeField]
    protected float yOriginalMin;

    protected Vector3 respawnPosition;

    [SerializeField]
    protected GameObject damageSfxGameObject;

    protected SoundManager soundManager;




    public float YOriginalMax { get => yOriginalMax; set => yOriginalMax = value; }
    public float YOriginalMin { get => yOriginalMin; set => yOriginalMin = value; }
    public float XOriginalMax { get => xOriginalMax; set => xOriginalMax = value; }
    public float XOriginalMin { get => xOriginalMin; set => xOriginalMin = value; }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        if (isSpawnCoins)
        {
            //coinsHolder = new BossCoinsHolder();
            coinsHolder.Start(this);
        }
        respawnPosition = transform.position;
        Debug.Log("damage sound " + soundManager);

    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (isSpawnCoins)
        {
            coinsHolder.SpawnCoin();
        }
    }

    public void RemoveCoin(GameObject coin)
    {
        coinsHolder.RemoveCoin(coin);
    }

    public virtual void InitializeBoss()
    {

    }

    public virtual void TakeDamage()
    {
        soundManager.PlaySfxMusic(damageSfxGameObject.name);
    }
}
