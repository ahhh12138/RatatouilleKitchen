using UnityEngine;

// 这个脚本专门处理：食材拖进薯条合成区 的功能
// 完全不影响你原来的垃圾桶、拖拽、汉堡逻辑
public class DragToFriesArea : MonoBehaviour
{
    private Ingredient originalScript;
    private bool isInFriesArea = false;

    void Awake()
    {
        // 获取你原来的脚本
        originalScript = GetComponent<Ingredient>();
    }

    void OnMouseUp()
    {
        // 如果已经扔进垃圾桶 → 不执行
        if (originalScript.isInCan) return;

        // 如果鼠标抬起时在薯条合成区
        if (isInFriesArea)
        {
            // 通知薯条合成系统
            if (FriesMaker.instance != null)
            {
                FriesMaker.instance.AddIngredient(gameObject);
            }

            // 销毁食材
            Destroy(gameObject);
        }
    }

    // 检测进入薯条合成区
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("FriesArea"))
        {
            isInFriesArea = true;
        }
    }

    // 检测离开薯条合成区
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("FriesArea"))
        {
            isInFriesArea = false;
        }
    }
}