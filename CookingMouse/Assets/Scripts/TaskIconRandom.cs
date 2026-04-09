using UnityEngine;
using UnityEngine.UI;

public class TaskIconRandom : MonoBehaviour
{
    public Sprite burgerIcon;
    public Sprite friesIcon;
    public Sprite drinkIcon;

    private Image currentIcon;
    private string currentTaskName;

    void Start()
    {
        // 安全获取：先找身上有没有 Image
        currentIcon = GetComponent<Image>();

        // 没有就自动添加（安全版，不会崩溃）
        if (currentIcon == null)
            currentIcon = gameObject.AddComponent<Image>();

        // 强制设置正确的UI大小
        RectTransform rt = currentIcon.rectTransform;
        rt.sizeDelta = new Vector2(100, 100);

        // 初始化显示
        RandomTask();
    }

    public void RandomTask()
    {
        // 防崩溃：如果没有图片，直接返回
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
                currentTaskName = "Drink";
                break;
        }

        Debug.Log("成功刷新");
    }

    public void OnDropFood(string foodName)
    {
        // 防错
        if (currentIcon == null) return;

        if (foodName == currentTaskName)
        {
            Debug.Log("提交成功！");
            RandomTask(); // 这里一定会刷新
        }
        else
        {
            Debug.Log("提交失败：食物不匹配");
        }
    }
}