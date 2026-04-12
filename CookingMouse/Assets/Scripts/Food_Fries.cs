using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food_Fries : MonoBehaviour
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

        AudioManager.instance.PlayPickUp("potato");
    }

    private void OnMouseUp()
    {
        isDragging = false;

        if (isInCan)
        {
            // 垃圾桶音效
            AudioManager.instance.PlayThrowTrash();
            Destroy(gameObject);
        }
        else if (currentTask != null)
        {
            bool success = currentTask.CheckAndSubmit("Fries");
            if (success)
            {
                Destroy(gameObject);
            }
            else
            {
                transform.position = originPos;
                AudioManager.instance.PlayPutBack();
            }
        }
        else
        {
            transform.position = originPos;
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
        }
    }
}