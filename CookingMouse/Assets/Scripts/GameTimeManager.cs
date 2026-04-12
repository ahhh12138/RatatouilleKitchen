using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimeManager : MonoBehaviour
{
    public static GameTimeManager instance;
    public float currentGlobalTime;
    public Text globalTimeText;
    public float timePenalty = 5f;
    
    public void Awake()
    {
        if (instance == null) instance = this;
        //else Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        currentGlobalTime = 60f;
    }

    // Update is called once per frame
    void Update()
    {
        currentGlobalTime -= Time.deltaTime;
        currentGlobalTime=Mathf.Max(currentGlobalTime, 0);

        if (globalTimeText != null)
            globalTimeText.text = Mathf.Ceil(currentGlobalTime).ToString();
            //globalTimeText.text = currentGlobalTime.ToString("F5");

        if (currentGlobalTime <= 0)
        {
            Time.timeScale = 0f;
            Debug.Log("时间暂停");
        }
        Debug.Log($"实例ID: {GetInstanceID()}, currentGlobalTime: {currentGlobalTime:F5}, 暂停状态: {currentGlobalTime <= 0}");
    }

    public void SubstractGlobalTime()
    {
        currentGlobalTime -= timePenalty;
    }

    public void AddGlobalTime()
    {
        currentGlobalTime += 5f;
    }
}
