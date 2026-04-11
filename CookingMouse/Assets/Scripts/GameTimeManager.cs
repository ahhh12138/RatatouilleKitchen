using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimeManager : MonoBehaviour
{
    public static GameTimeManager instance;
    public float globalTime = 60f;
    private float currentGlobalTime;
    public Text globalTimeText;
    public float timePenalty = 5f;
    
    public void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        currentGlobalTime = globalTime;
    }

    // Update is called once per frame
    void Update()
    {
        currentGlobalTime -= Time.deltaTime;
        currentGlobalTime=Mathf.Max(currentGlobalTime, 0);

        if (globalTimeText != null)
            globalTimeText.text = Mathf.Ceil(currentGlobalTime).ToString();

        if (currentGlobalTime <= 0)
        {
            Time.timeScale = 0f;
        }
    }

    public void SubstractGlobalTime()
    {
        currentGlobalTime -= timePenalty;
    }
}
