/*using UnityEngine;
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
        currentIcon = GetComponent<Image>();
        if (currentIcon == null) currentIcon = gameObject.AddComponent<Image>();

        RectTransform rt = currentIcon.rectTransform;
        rt.sizeDelta = new Vector2(100, 100);

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
                currentTaskName = "juice"; // 绝对正确
                break;
        }

        // 🔥 关键：看控制台！这里会打印当前任务是什么！
        Debug.Log("当前任务是：" + currentTaskName);
    }

    public void OnDropFood(string foodName)
    {
        Debug.Log("你提交的是：" + foodName);

        if (foodName == currentTaskName)
        {
            Debug.Log("提交成功！");
            RandomTask();
        }
        else
        {
            Debug.Log("提交失败：食物不匹配");
        }
    }
}
*/