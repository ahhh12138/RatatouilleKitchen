/*using UnityEngine;

public class Food_Drink : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isDragging;

    private Vector3 mouseOffset;
    private float mouseZCoord;

    public bool isInCan = false;
    public bool isInTaskSlot = false;

    public Vector2 originPos;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isDragging = false;
        originPos = transform.position;
    }

    private void OnMouseDown()
    {
        isDragging = true;
        mouseZCoord = Camera.main.WorldToScreenPoint(transform.position).z;
        mouseOffset = transform.position - GetMouseWorldPos();
    }

    private void OnMouseUp()
    {
        isDragging = false;

        if (isInCan)
        {
            Destroy(gameObject);
        }
        else if (isInTaskSlot)
        {
            SubmitTask();
        }
        else
        {
            transform.position = originPos;
        }
    }

    void SubmitTask()
    {
        TaskIconRandom task = FindObjectOfType<TaskIconRandom>();
        if (task != null)
        {
            task.OnDropFood("juice"); // 100% 正确
            Destroy(gameObject);
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
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
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
        if (other.CompareTag("Can")) isInCan = true;
        if (other.CompareTag("TaskSlot")) isInTaskSlot = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Can")) isInCan = false;
        if (other.CompareTag("TaskSlot")) isInTaskSlot = false;
    }
}
*/