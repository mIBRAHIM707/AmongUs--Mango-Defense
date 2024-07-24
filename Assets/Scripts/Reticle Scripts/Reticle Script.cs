using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReticleScript : MonoBehaviour
{
    [Header("Animation Settings")]
    [SerializeField] private AnimationCurve animCurve;
    [SerializeField] private float selectAnimTime;
    [SerializeField] private float deselectAnimTime;

    [Header("Drag Settings")]
    [SerializeField] private float amplitude;

    [Header("Spring")]
    [SerializeField] private float stiffness;
    [SerializeField] private float clamp;

    [Header("Points")]
    [SerializeField] private List<GameObject> points = new List<GameObject>();
    [SerializeField] private List<GameObject> launchPoints = new List<GameObject>();
    private List<Vector3> pointStartPos = new List<Vector3>();
    private GameObject selectedObject;

    private void Awake()
    {
        foreach (GameObject point in points)
        {
            if (point != null)
            {
                pointStartPos.Add(point.transform.localPosition);
            }
            else
            {
                Debug.LogWarning("Point GameObject is null in Awake.");
            }
        }
    }

    private void Update()
    {
        if (selectedObject != null)
        {
            StopAllCoroutines();
            Selected(selectedObject);
        }
    }

    private void HandleRotation(GameObject item)
    {
        if (Camera.main == null)
        {
            Debug.LogError("Main Camera not found.");
            return;
        }

        Vector2 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(this.transform.position);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        item.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void HandlePoint(Vector3 mousePosRelative, float distance, int i, Point point)
    {
        if (point == null)
        {
            Debug.LogWarning("Point component is null.");
            return;
        }

        int dir = point.flip ? -1 : 1;

        if (i >= pointStartPos.Count)
        {
            Debug.LogWarning("Index out of range for pointStartPos.");
            return;
        }

        Vector3 startPos = pointStartPos[i];
        float magnitude = (amplitude * distance) / distance;
        float pointIdentity = (startPos.x * magnitude) * dir;
        Vector3 targetPos = (mousePosRelative * pointIdentity) * dir;

        float lerpTime = (selectAnimTime / pointIdentity) * Time.deltaTime;
        Vector3 lerpPos = Vector3.Lerp(point.transform.localPosition, targetPos, lerpTime);

        lerpPos.z = 0;
        float pointDistance = Vector3.Distance(point.transform.position, this.transform.position);
        lerpPos = Vector3.ClampMagnitude(lerpPos, (pointIdentity / magnitude) + (pointDistance / clamp));

        point.transform.localPosition = lerpPos;
    }

    public void Selected(GameObject selected)
    {
        if (selected == null)
        {
            Debug.LogWarning("Selected GameObject is null.");
            return;
        }

        this.gameObject.SetActive(true);
        this.transform.position = selected.transform.position;
        selectedObject = selected;
        Vector3 mousePos = Input.mousePosition;
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector3 mousePosRelative = mouseWorldPos - this.transform.position;

        for (int i = 0; i < points.Count; i++)
        {
            if (points[i] == null)
            {
                Debug.LogWarning($"Point GameObject at index {i} is null.");
                continue;
            }

            HandleRotation(points[i]);
            float distance = Vector2.Distance(this.transform.position, mouseWorldPos);
            Point point = points[i].GetComponent<Point>();
            HandlePoint(mousePosRelative, distance, i, point);
        }
    }

    public void Deselect()
    {
        selectedObject = null;
        for (int i = 0; i < points.Count; i++)
        {
            if (points[i] != null)
            {
                StartCoroutine(LerpObject(points[i], Vector3.zero, deselectAnimTime));
            }
            else
            {
                Debug.LogWarning($"Point GameObject at index {i} is null.");
            }
        }
    }

    private void FireProjectiles()
    {
        for (int i = 0; i < launchPoints.Count; i++)
        {
            if (launchPoints[i] != null)
            {
                ShootProjectile shooter = launchPoints[i].GetComponent<ShootProjectile>();
                if (shooter != null)
                {
                    shooter.FireProjectile();
                }
                else
                {
                    Debug.LogWarning($"ShootProjectile component not found on launch point at index {i}.");
                }
            }
            else
            {
                Debug.LogWarning($"Launch point GameObject at index {i} is null.");
            }
        }
        this.gameObject.SetActive(false);
    }

    private int count = 0;
    private IEnumerator LerpObject(GameObject item, Vector3 pos, float time)
    {
        if (item == null)
        {
            Debug.LogWarning("LerpObject GameObject is null.");
            yield break;
        }

        Vector3 currentPos = item.transform.localPosition;
        float elapsed = 0f;
        float distance = Vector3.Distance(currentPos, pos);
        float ratio = 0;

        while (ratio < 1)
        {
            elapsed += Time.fixedDeltaTime;
            float offset = animCurve.Evaluate(ratio);
            float newOffset = offset - ratio;
            newOffset = newOffset / stiffness;
            offset = newOffset + ratio;
            float invertOffset = 1.0f - offset;
            item.transform.localPosition = Vector3.Lerp(currentPos, pos, ratio) * invertOffset;

            yield return null;
            ratio = (elapsed / time);
        }
        count++;
        if (count >= points.Count)
        {
            FireProjectiles();
        }
    }
}
