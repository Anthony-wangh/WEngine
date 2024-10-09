using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class BoxSelect : MonoBehaviour
{
    public Material boxMaterial;
    public Material lineMaterial;

    private Vector3 startPoint;
    private bool isSelecting = false;

    [SerializeField]
    private LineRenderer lineRenderer;

    private List<GameObject> cubes = new List<GameObject>();
    private void Start()
    {
        lineRenderer.material = lineMaterial;
        lineRenderer.positionCount = 5;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lineRenderer.enabled = true;
            startPoint = GetPoint();
            isSelecting = true;
            CreateCube();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isSelecting = false;
            SelectObjects();
            CreateCube();
            Release();

        }
        UpdateLine();
    }

    private async void Release()
    {
        await Task.Delay(100);
        foreach (GameObject go in cubes)
        {
            Destroy(go);
        }
        cubes.Clear();
        lineRenderer.enabled = false;
    }

    private void CreateCube()
    {
        var go = GameObject.CreatePrimitive(PrimitiveType.Cube);
        var render = go.GetComponent<MeshRenderer>();
        render.material = boxMaterial;
        go.transform.position = GetPoint();
        go.transform.rotation = Quaternion.identity;
        go.transform.localScale = Vector3.one * 0.1f;
        cubes.Add(go);
    }

    private Vector3 GetPoint()
    {
        var screenPos = Input.mousePosition;
        screenPos.z = 5;
        return Camera.main.ScreenToWorldPoint(screenPos);
    }

    private void UpdateLine()
    {
        if (isSelecting)
        {
            Vector3 endPos = GetPoint();
            lineRenderer.SetPosition(0, startPoint);
            lineRenderer.SetPosition(1, new Vector3(endPos.x, startPoint.y, startPoint.z));
            lineRenderer.SetPosition(2, endPos);
            lineRenderer.SetPosition(3, new Vector3(startPoint.x, endPos.y, startPoint.z));
            lineRenderer.SetPosition(4, startPoint);
        }
    }

    private void SelectObjects()
    {
        Vector3 mouseStartPos = startPoint;
        Vector3 mouseEndPos = Input.mousePosition;

        Vector3 min = Vector3.Min(mouseStartPos, mouseEndPos);
        Vector3 max = Vector3.Max(mouseStartPos, mouseEndPos);

        Rect selectRect = new Rect(min.x, Screen.height - max.y, max.x - min.x, max.y - min.y);

        foreach (GameObject obj in FindObjectsOfType<GameObject>())
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(obj.transform.position);
            if (selectRect.Contains(screenPos))
            {
                Debug.Log("Selected object: " + obj.name);
                // 在这里可以添加选中对象的操作逻辑
            }
        }
    }
}
