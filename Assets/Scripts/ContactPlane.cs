using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContactPlane : MonoBehaviour
{
    public float strike = 0;
    public float dip = 0;

    public Canvas canvas;
    public GameObject pointerSphere;
    public GameObject line;

    InputField heightInputField;
    Transform structContours;

    private void Awake()
    {
        heightInputField = canvas.GetComponentInChildren<InputField>();
        heightInputField.gameObject.SetActive(false);

        structContours = this.GetComponentInChildren<StructureContours>().gameObject.transform;
        Debug.Log(structContours.gameObject.name);
        pointerSphere.gameObject.SetActive(false);
    }

    private void AlignPlane()
    {
        this.transform.rotation = Quaternion.Euler(0f, strike, -dip);
    }

    private void RaycastClick()
    {
        if (!heightInputField.gameObject.activeSelf && Input.GetMouseButton(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit = new RaycastHit();

            if (Physics.Raycast(ray, out hit))
            {
                heightInputField.gameObject.SetActive(true);
                pointerSphere.gameObject.SetActive(true);

                pointerSphere.transform.position = hit.point;
                pointerSphere.transform.SetParent(this.transform);

                Debug.Log("[+] hit.point = " + hit.point);
                ContourThroughPoint(hit.point);
            }
        }
    }

    public void PositionPlaneFromPointHeight()
    {
        heightInputField.gameObject.SetActive(false);
        Vector3 displacement = (Vector3.up * (float.Parse(heightInputField.text) - pointerSphere.transform.position.y));
        //Debug.Log("[+] displacement = " + displacement);
        this.transform.position += displacement;
    }

    private void ContourThroughPoint(Vector3 point)
    {
        GameObject contour = Instantiate(line, structContours);
        LineRenderer lineRenderer = contour.GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, point+ (5-point.z)*contour.transform.forward);
        lineRenderer.SetPosition(1, point+ (-5-point.z)*contour.transform.forward);

        ExtrapolateContour(lineRenderer, 1, 5);
    }

    private void ExtrapolateContour(LineRenderer contourLine, float spacing, int number)
    {
        for(int i = 1; i<= number; i++)
        {
            GameObject left = Instantiate(line, structContours);
            LineRenderer lineRenderer= left.GetComponent<LineRenderer>();
            lineRenderer.SetPosition(0, contourLine.GetPosition(0)+i*spacing*contourLine.gameObject.transform.right);
            lineRenderer.SetPosition(1, contourLine.GetPosition(1) + i *spacing* contourLine.gameObject.transform.right);

            GameObject right = Instantiate(line, structContours);
            lineRenderer = right.GetComponent<LineRenderer>();
            lineRenderer.SetPosition(0, contourLine.GetPosition(0) - i * spacing * contourLine.gameObject.transform.right);
            lineRenderer.SetPosition(1, contourLine.GetPosition(1) - i * spacing * contourLine.gameObject.transform.right);
        }
    }

    private void Start()
    {

    }

    void Update()
    {
        AlignPlane();
        RaycastClick();
    }
}
//instantiate pointerSphere
//structure contours: create function; ensure structure contours do not go beyond plane
//comment and clean up