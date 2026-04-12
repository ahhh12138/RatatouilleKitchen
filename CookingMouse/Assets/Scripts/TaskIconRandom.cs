using UnityEngine;
using UnityEngine.UI;

public class TaskIconRandom : MonoBehaviour
{
    public Sprite burgerIcon;
    public Sprite friesIcon;
    public Sprite drinkIcon;

    private Image currentIcon;
    private string currentTaskName;

    //倒计时
    public float taskTime=20f;
    public float currentTime;
    private bool isCounting=false;
    public Text TimeText;

    void Awake()
    {
        currentIcon = GetComponent<Image>();

        if (currentIcon == null)
            currentIcon = gameObject.AddComponent<Image>();

        RectTransform rt = currentIcon.rectTransform;
        rt.sizeDelta = new Vector2(100, 100);
        currentIcon.preserveAspect = true;
    }

    void Start()
    {
        RandomTask();
    }

    public void RandomTask()
    {
        if (currentIcon == null) return;

        int r = Random.Range(0, 3);

        switch (r)
        {
            case 0:
                currentIcon.sprite = burgerIcon;
                currentTaskName = "Burger";
                break;
            case 1:
                currentIcon.sprite = friesIcon;
                currentTaskName = "Fries";
                break;
            case 2:
                currentIcon.sprite = drinkIcon;
                currentTaskName = "juice";
                break;
        }

        Debug.Log("【当前任务" + gameObject.name + "需要】：" + currentTaskName);
        RestartCountdown();
    }

    // 只刷新自己这个任务
    public bool CheckAndSubmit(string foodName)
    {
        if (foodName == currentTaskName)
        {
            Debug.Log("✅ " + gameObject.name + " 提交成功！");
            GameTimeManager.instance.AddGlobalTime();
            Debug.Log(GameTimeManager.instance.currentGlobalTime);
            RandomTask();
            return true;
        }
        else
        {
            Debug.Log("❌ " + gameObject.name + " 不匹配");
            return false;
        }
    }

    public void Update()
    {
        if (!isCounting) return;
        //Debug.Log($"deltaTime: {Time.deltaTime}, currentTime: {currentTime}");
        currentTime -= Time.deltaTime;
        if (TimeText != null)
        {
            TimeText.text = Mathf.Ceil(currentTime).ToString();
        }
        if (currentTime <= 0)
        {
            Debug.Log("提交失败");
            Debug.Log(GameTimeManager.instance.currentGlobalTime);
            OnTimeOut();
        }
    }
    void RestartCountdown()
    {
        currentTime = taskTime;
        isCounting = true;

        if (TimeText != null)
            TimeText.text = Mathf.Ceil(taskTime).ToString();
    }
    void OnTimeOut()
    {
        isCounting = false;
        Debug.Log("任务超时：" + currentTaskName);
        GameTimeManager.instance.SubstractGlobalTime();
        RandomTask();
    }
}