using UnityEngine;

// 这个脚本专门处理：食材拖进汉堡合成区 的功能
// 完全不影响你原来的垃圾桶、拖拽逻辑
public class DragToBurgerArea : MonoBehaviour
{
    private Ingredient originalScript;
    private bool isInBurgerArea = false;

    void Awake()
    {
        // 获取你原来的脚本
        originalScript = GetComponent<Ingredient>();
    }

    void OnMouseUp()
    {
        // 如果已经扔进垃圾桶 → 不执行
        if (originalScript.isInCan) return;

        // 如果鼠标抬起时在汉堡合成区
        if (isInBurgerArea)
        {
            // 通知合成系统
            if (BurgerMaker.instance != null)
            {
                BurgerMaker.instance.AddIngredient(gameObject);
            }

            // 销毁食材
            Destroy(gameObject);
        }
    }

    // 检测进入合成区
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("BurgerArea"))
        {
            isInBurgerArea = true;
        }
    }

    // 检测离开合成区
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("BurgerArea"))
        {
            isInBurgerArea = false;
        }
    }
}