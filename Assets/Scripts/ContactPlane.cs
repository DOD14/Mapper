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

    InputField heightInputField;

    private void Awake()
    {
        heightInputField = canvas.GetComponentInChildren<InputField>();
        heightInputField.gameObject.SetActive(false);
        pointerSphere.gameObject.SetActive(false);
    }

    private void AlignPlane()
    {
        this.transform.rotation = Quaternion.Euler(0f, strike, -dip);
    }

    void RaycastClick()
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
            }
        }
    }

    public void PositionPlane()
    {
        heightInputField.gameObject.SetActive(false);
        Vector3 displacement = (Vector3.up * (float.Parse(heightInputField.text) - pointerSphere.transform.position.y));
        //Debug.Log("[+] displacement = " + displacement);
        this.transform.position += displacement;
    }


    void Update()
    {
        AlignPlane();
        RaycastClick();
    }
}
//instantiate pointerSphere
//structure contours 