using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food_Drink : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isDragging;

    private Vector3 mouseOffset;
    private float mouseZCoord;

    public bool isInCan = false;
    public TaskIconRandom currentTask;

    public Vector2 originPos;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originPos = transform.position;
    }

    private void OnMouseDown()
    {
        isDragging = true;
        mouseZCoord = Camera.main.WorldToScreenPoint(transform.position).z;
        mouseOffset = transform.position - GetMouseWorldPos();

        // 饮料拿起音效
        AudioManager.instance.PlayPickUp("juice");
    }

    private void OnMouseUp()
    {
        isDragging = false;

        if (isInCan)
        {
            // 丢垃圾桶音效
            AudioManager.instance.PlayThrowTrash();
            Destroy(gameObject);
        }
        else if (currentTask != null)
        {
            Debug.Log("✅ 测试：juice 已经正确进入任务区域，可以提交！");
            bool success = currentTask.CheckAndSubmit("juice");
            if (success)
            {
                Debug.Log("✅ 测试：juice 提交成功！");
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("❌ 测试：juice 进入了任务区域，但提交失败（任务不匹配）");
                transform.position = originPos;
                // 放回原位音效
                AudioManager.instance.PlayPutBack();
            }
        }
        else
        {
            Debug.Log("❌ 测试：juice 没有进入任何任务区域 currentTask = null");
            transform.position = originPos;
            // 放回原位音效
            AudioManager.instance.PlayPutBack();
        }
    }

    private void OnMouseEnter()
    {
        transform.localScale += Vector3.one * 0.07f;
    }

    private void OnMouseExit()
    {
        transform.localScale -= Vector3.one * 0.07f;
    }

    private void FixedUpdate()
    {
        if (isDragging)
        {
            rb.MovePosition(GetMouseWorldPos() + mouseOffset);
        }
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mouseZCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Can"))
        {
            isInCan = true;
        }

        if (other.CompareTag("TaskSlot"))
        {
            currentTask = other.GetComponent<TaskIconRandom>();
            Debug.Log("=====================================");
            Debug.Log("🟢 测试：JUICE 进入任务区域！！！");
            Debug.Log("🟢 触发对象：" + other.name);
            Debug.Log("=====================================");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Can"))
        {
            isInCan = false;
        }

        if (other.CompareTag("TaskSlot"))
        {
            currentTask = null;
            Debug.Log("=====================================");
            Debug.Log("🔴 测试：JUICE 离开任务区域");
            Debug.Log("=====================================");
        }
    }
}