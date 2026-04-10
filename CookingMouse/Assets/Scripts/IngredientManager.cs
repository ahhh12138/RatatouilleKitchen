using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class IngredientManager : MonoBehaviour
{
    [Header("食材生成位置（自动找场景里现有食材）")]
    public List<Vector3> breadSpawnPoints = new List<Vector3>();
    public List<Vector3> potatoSpawnPoints = new List<Vector3>();
    public List<Vector3> meatSpawnPoints = new List<Vector3>();
    public List<Vector3> vegetableSpawnPoints = new List<Vector3>();
    public List<Vector3> juiceSpawnPoints = new List<Vector3>();

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

    [Header("对应按钮")]
    public Button btnBread;
    public Button btnPotato;
    public Button btnMeat;
    public Button btnVegetables;
    public Button btnDrink;

    private float gameTime = 0;
    private const int MaxCount = 3;

    void Start()
    {
        RecordExistIngredientPositions();
        InvokeRepeating("CheckAllButtons", 0, 1f);
    }

    void Update()
    {
        gameTime += Time.deltaTime;
    }

    void RecordExistIngredientPositions()
    {
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("bread"))
            breadSpawnPoints.Add(obj.transform.position);

        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("potato"))
            potatoSpawnPoints.Add(obj.transform.position);

        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("meat"))
            meatSpawnPoints.Add(obj.transform.position);

        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("vegetables"))
            vegetableSpawnPoints.Add(obj.transform.position);

        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("juice"))
            juiceSpawnPoints.Add(obj.transform.position);
    }

    void CheckAllButtons()
    {
        btnBread.interactable = CountIngredients("bread") == 0;
        btnPotato.interactable = CountIngredients("potato") == 0;
        btnMeat.interactable = CountIngredients("meat") == 0;
        btnVegetables.interactable = CountIngredients("vegetables") == 0;
        btnDrink.interactable = CountIngredients("juice") == 0;
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

    void SpawnSingleIngredient(GameObject good, GameObject bad, Vector3 spawnPos)
    {
        int rate = Random.Range(0, 100);
        int badRate = GetBadRate();

        if (rate < badRate)
            Instantiate(bad, spawnPos, Quaternion.identity);
        else
            Instantiate(good, spawnPos, Quaternion.identity);
    }

    void SpawnBatchIngredients(GameObject good, GameObject bad, List<Vector3>spawnPoints)
    {
        foreach (Vector3 t in spawnPoints)
        {
            SpawnSingleIngredient(good, bad, t);
        }
    }

    // 按钮点击
    public void OnClickBread()
    {
        Debug.Log("Click: Bread");
        CleanSpawnPoints(breadSpawnPoints);
        SpawnBatchIngredients(bread, badBread, breadSpawnPoints);
    }

    public void OnClickPotato()
    {
        Debug.Log("Click: Potato");
        CleanSpawnPoints(potatoSpawnPoints);
        SpawnBatchIngredients(potato, badPotato, potatoSpawnPoints);
    }

    public void OnClickMeat()
    {
        Debug.Log("Click: Meat");
        CleanSpawnPoints(meatSpawnPoints);
        SpawnBatchIngredients(meat, badMeat, meatSpawnPoints);
    }

    public void OnClickVegetables()
    {
        Debug.Log("Click: Vegetables");
        CleanSpawnPoints(vegetableSpawnPoints);
        SpawnBatchIngredients(vegetables, badVegetables, vegetableSpawnPoints);
    }

    public void OnClickDrink()
    {
        Debug.Log("Click: Drink");
        CleanSpawnPoints(juiceSpawnPoints);
        SpawnBatchIngredients(drink, badDrink, juiceSpawnPoints);
    }

    private void CleanSpawnPoints(List<Vector3> spawnPoints)
    {
        for (int i = spawnPoints.Count - 1; i >= 0; i--)
        {
            if (spawnPoints[i] == null)
                spawnPoints.RemoveAt(i);
        }
    }
}