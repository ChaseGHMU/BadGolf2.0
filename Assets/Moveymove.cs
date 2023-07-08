using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Moveymove : MonoBehaviour
{
    private Vector2 dragStartPosition;
    private Rigidbody2D ballRigidbody;
    private bool isDragging;

    public float launchPower = 10f;
    public Image powerArrow;

    private void Start()
    {
        ballRigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartDragging();
        }

        if (Input.GetMouseButtonUp(0))
        {
            LaunchBall();
        }

        if (isDragging)
        {
            UpdateDrag();
        }
    }

    private void StartDragging()
    {
        isDragging = true;
        dragStartPosition = Input.mousePosition;
        powerArrow.gameObject.SetActive(true);
    }

    private void UpdateDrag()
    {
        Vector2 currentDragPosition = Input.mousePosition;
        Vector2 dragVector = currentDragPosition - dragStartPosition;
        float launchPowerMultiplier = Mathf.Clamp01(dragVector.magnitude / Screen.width);

        // Visualize the drag vector (optional)
        Debug.DrawLine(transform.position, transform.position + (Vector3)dragVector, Color.red);

        // Rotate the ball based on the drag direction
        float rotationAngle = Mathf.Atan2(dragVector.y, dragVector.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(rotationAngle, Vector3.forward);

        // Adjust launch power based on drag distance
        float currentLaunchPower = launchPower * launchPowerMultiplier;

        // Visualize the launch power (optional)
        Debug.DrawRay(transform.position, transform.right * currentLaunchPower, Color.blue);

        // Scale the power arrow based on the launch power
        powerArrow.transform.localScale = new Vector3(launchPowerMultiplier, 1f, 1f);
    }

    private void LaunchBall()
    {
        isDragging = false;
        powerArrow.gameObject.SetActive(false);
        ballRigidbody.AddForce(transform.right * launchPower, ForceMode2D.Impulse);
    }
}