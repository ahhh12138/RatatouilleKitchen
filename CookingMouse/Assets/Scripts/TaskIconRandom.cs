using UnityEngine;
using UnityEngine.UI;

public class TaskIconRandom : MonoBehaviour
{
    public Sprite burgerIcon;
    public Sprite friesIcon;
    public Sprite drinkIcon;

    private Image currentIcon;
    private string currentTaskName;

    void Awake()
    {
        currentIcon = GetComponent<Image>();

        if (currentIcon == null)
            currentIcon = gameObject.AddComponent<Image>();

        RectTransform rt = currentIcon.rectTransform;
        rt.sizeDelta = new Vector2(100, 100);
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
    }

    // 只刷新自己这个任务
    public bool CheckAndSubmit(string foodName)
    {
        if (foodName == currentTaskName)
        {
            Debug.Log("✅ " + gameObject.name + " 提交成功！");
            RandomTask();
            return true;
        }
        else
        {
            Debug.Log("❌ " + gameObject.name + " 不匹配");
            return false;
        }
    }
}