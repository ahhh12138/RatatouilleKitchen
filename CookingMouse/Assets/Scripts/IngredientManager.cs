using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class IngredientManager : MonoBehaviour
{
    [Header("食材生成位置（请手动拖空物体进去）")]
    public List<Transform> breadSpawnPoints = new List<Transform>();
    public List<Transform> potatoSpawnPoints = new List<Transform>();
    public List<Transform> meatSpawnPoints = new List<Transform>();
    public List<Transform> vegetableSpawnPoints = new List<Transform>();
    public List<Transform> juiceSpawnPoints = new List<Transform>();

    [Header("好食材预制体")]
    public GameObject bread;
    public GameObject potato;
    public GameObject meat;
    public GameObject vegetables;
    public GameObject drink;

    [Header("坏食材预制体")]
    public GameObject badBread;
    public GameObject badPotato;
    public GameObject badMeat;
    public GameObject badVegetables;
    public GameObject badDrink;

    [Header("对应按钮 —— 全部拖进来！")]
    public Button btnBread;
    public Button btnPotato;
    public Button btnMeat;
    public Button btnVegetables;
    public Button btnDrink;

    private float gameTime = 0;

    void Start()
    {
        // ======================
        // 自动绑定所有按钮！
        // 不管 Unity 抽什么风，全部强制生效
        // ======================
        if (btnBread != null)
            btnBread.onClick.AddListener(OnClickBread);

        if (btnPotato != null)
            btnPotato.onClick.AddListener(OnClickPotato);

        if (btnMeat != null)
            btnMeat.onClick.AddListener(OnClickMeat);

        if (btnVegetables != null)
            btnVegetables.onClick.AddListener(OnClickVegetables);

        if (btnDrink != null)
            btnDrink.onClick.AddListener(OnClickDrink);

        // 启动按钮检测
        InvokeRepeating("CheckAllButtons", 0f, 1f);
        Debug.Log("=== 食材管理器启动 ===");
    }

    void Update()
    {
        gameTime += Time.deltaTime;
    }

    void CheckAllButtons()
    {
        int breadCount = CountIngredients("bread");
        int potatoCount = CountIngredients("potato");
        int meatCount = CountIngredients("meat");
        int vegCount = CountIngredients("vegetables");
        int juiceCount = CountIngredients("juice");

        btnBread.interactable = (breadCount == 0);
        btnPotato.interactable = (potatoCount == 0);
        btnMeat.interactable = (meatCount == 0);
        btnVegetables.interactable = (vegCount == 0);
        btnDrink.interactable = (juiceCount == 0);

        Debug.Log($"[数量检测] bread:{breadCount} potato:{potatoCount} meat:{meatCount} veg:{vegCount} juice:{juiceCount}");
    }

    int CountIngredients(string tag)
    {
        return GameObject.FindGameObjectsWithTag(tag).Length;
    }

    int GetBadRate()
    {
        if (gameTime < 60) return Random.Range(5, 11);
        if (gameTime < 180) return Random.Range(15, 26);
        return Random.Range(30, 41);
    }

    void SpawnSingleIngredient(GameObject good, GameObject bad, Transform spawnPoint)
    {
        if (spawnPoint == null)
        {
            Debug.LogWarning("生成点为空");
            return;
        }

        int rate = Random.Range(0, 100);
        int badRate = GetBadRate();

        GameObject spawned = (rate < badRate) ? Instantiate(bad, spawnPoint.position, Quaternion.identity) 
                                             : Instantiate(good, spawnPoint.position, Quaternion.identity);

        if (spawned != null)
            Debug.Log("生成成功：" + spawned.name);
    }

    void SpawnBatchIngredients(GameObject good, GameObject bad, List<Transform> spawnPoints)
    {
        foreach (Transform t in spawnPoints)
            SpawnSingleIngredient(good, bad, t);
    }

    // 按钮点击方法
    public void OnClickBread()
    {
        Debug.Log("==== 【面包按钮】被点击 ====");
        SpawnBatchIngredients(bread, badBread, breadSpawnPoints);
    }

    public void OnClickPotato()
    {
        Debug.Log("==== 【薯条按钮】被点击 ====");
        SpawnBatchIngredients(potato, badPotato, potatoSpawnPoints);
    }

    public void OnClickMeat()
    {
        Debug.Log("==== 【肉按钮】被点击 ====");
        SpawnBatchIngredients(meat, badMeat, meatSpawnPoints);
    }

    public void OnClickVegetables()
    {
        Debug.Log("==== 【蔬菜按钮】被点击 ====");
        SpawnBatchIngredients(vegetables, badVegetables, vegetableSpawnPoints);
    }

    public void OnClickDrink()
    {
        Debug.Log("==== 【饮料按钮】被点击 ====");
        SpawnBatchIngredients(drink, badDrink, juiceSpawnPoints);
    }
}