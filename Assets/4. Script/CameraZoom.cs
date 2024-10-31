using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public Transform[] targetObjects;
    public float moveSpeed = 5f;
    public float targetSize = 2f;
    public float initialSize = 5f;
    public float fixedDuration = 1f;
    private Camera cam;

    public GameObject exitBtn;

    void Start()
    {
        cam = Camera.main;
        cam.orthographicSize = initialSize;
        transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null)
            {
                for (int i = 0; i < targetObjects.Length; i++)
                {
                    if (hit.transform == targetObjects[i])
                    {
                        OnClickedMove(i);
                        break;
                    }
                }
            }
        }
    }

    public void OnClickedMove(int i)
    {
        
        StopAllCoroutines();
        StartCoroutine(MoveToTarget(i));
    }

    IEnumerator MoveToTarget(int i)
    {
        Vector3 targetPosition = targetObjects[i].position;
        Quaternion targetRotation = targetObjects[i].rotation;
        float elapsedTime = 0f;
        float duration = fixedDuration;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;

            transform.position = Vector3.Lerp(transform.position, targetPosition, t);
            transform.position = new Vector3(transform.position.x, transform.position.y, -10);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, t);
            cam.orthographicSize = Mathf.Lerp(initialSize, targetSize, t);

            yield return null;
        }
        exitBtn.SetActive(true);
        transform.position = new Vector3(targetPosition.x, targetPosition.y, -10);
        transform.rotation = targetRotation;
        cam.orthographicSize = targetSize;
    }

    public void MoveToOrigin()
    {
        exitBtn.SetActive(false);
        StopAllCoroutines();
        transform.position = new Vector3(0, 0, -10);
        cam.orthographicSize = initialSize;
    }
}
