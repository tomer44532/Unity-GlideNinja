using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private Player player;

    [SerializeField]
    private Image castingBar;

    [SerializeField]
    private Text castTime;

    [SerializeField]
    private CanvasGroup canvasGroup;

    private float progress;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        castTime.text = player.Stats.DashCD.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void DashCastingBar()
    {
        StartCoroutine(Progress());
    }

    private IEnumerator Progress()
    {
        float dashCd = player.Stats.DashCD;
        float timePassed = Time.deltaTime;
        float rate = 1.0f / dashCd;
        progress = 0.0f;
        while (progress <= 1.0f)
        {
           // if (player.Stats.Dashing)
            //{
                castingBar.fillAmount = Mathf.Lerp(0, 1, progress);
                progress += rate * Time.deltaTime;
                timePassed += Time.deltaTime;
                castTime.text = (dashCd - timePassed).ToString("F2");
                if (dashCd - timePassed <= 0)
                {
                    castTime.text = dashCd.ToString();

                }
            //}


            yield return null;
        }

    }

    public void ResetDashUI()
    {
        progress = 1;
        castingBar.fillAmount = 1;
        castTime.text = player.Stats.DashCD.ToString();

    }
}
