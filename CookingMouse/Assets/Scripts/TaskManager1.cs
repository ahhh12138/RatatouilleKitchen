/*using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskManager : MonoBehaviour
{
    public static TaskManager instance;

    [Header("任务栏父物体")]
    public Transform taskParent;

    [Header("任务图标预制体")]
    public GameObject taskItemPrefab;

    [Header("任务设置")]
    public int maxTaskCount = 3;
    public float refreshInterval = 8f;

    [System.Serializable]
    public class TaskType
    {
        public string foodName;
        public Sprite icon;
        public int score;
        public int timeAdd;
    }

    [Header("任务内容")]
    public TaskType burgerTask;
    public TaskType friesTask;
    public TaskType drinkTask;

    private List<TaskType> currentTasks = new List<TaskType>();

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Debug.Log("任务系统启动");
        RefreshTasks();
        InvokeRepeating(nameof(AutoRefreshTasks), refreshInterval, refreshInterval);
    }

    void AutoRefreshTasks()
    {
        RefreshTasks();
    }

    public void RefreshTasks()
    {
        ClearAllTasks();
        int count = Random.Range(1, maxTaskCount + 1);

        for (int i = 0; i < count; i++)
        {
            AddRandomTask();
        }
    }

    void AddRandomTask()
    {
        int rand = Random.Range(0, 3);
        TaskType task = null;

        if (rand == 0) task = burgerTask;
        else if (rand == 1) task = friesTask;
        else task = drinkTask;

        if (task != null && task.icon != null)
        {
            currentTasks.Add(task);
            CreateTaskUI(task);
        }
    }

    void CreateTaskUI(TaskType task)
    {
        if (taskItemPrefab == null || taskParent == null)
            return;

        GameObject go = Instantiate(taskItemPrefab, taskParent);

        // ↓↓↓ 强制锁在任务栏上，绝对跑不出去 ↓↓↓
        RectTransform rt = go.GetComponent<RectTransform>();
        rt.anchorMin = new Vector2(0, 0.5f);
        rt.anchorMax = new Vector2(0, 0.5f);
        rt.pivot = new Vector2(0.5f, 0.5f);
        rt.anchoredPosition = new Vector2(80 + 110 * currentTasks.Count, 0);
        rt.sizeDelta = new Vector2(100, 100);

        Image img = go.GetComponent<Image>();
        if (img != null)
        {
            img.sprite = task.icon;
            img.color = Color.white;
        }

        TaskItem item = go.GetComponent<TaskItem>();
        if (item != null)
            item.InitTask(task);
    }

    void ClearAllTasks()
    {
        currentTasks.Clear();
        foreach (Transform t in taskParent)
        {
            Destroy(t.gameObject);
        }
    }

    public void SubmitTask(TaskType task)
    {
        Debug.Log("完成任务：" + task.foodName);
        RefreshTasks();
    }
}
*/