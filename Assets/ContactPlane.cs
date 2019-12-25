using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContactPlane : MonoBehaviour
{
    public float strike = 0;
    public float dip = 0;
    public Vector3 point_on_plane;

    Plane plane;
    public InputField heightInputField;

    private void OnEnable()
    {
        plane = this.GetComponent<Plane>();
    }

    private void Awake()
    {
        heightInputField.gameObject.SetActive(false);
    }

    private void AlignPlane()
    {
        this.transform.rotation = Quaternion.Euler(0f, strike, -dip);
    }

    public void PositionPlane()
    {
        Vector3 spot = new Vector3(point_on_plane.x, float.Parse(heightInputField.text), point_on_plane.z);
        plane.Translate(spot - point_on_plane);
        heightInputField.gameObject.SetActive(false);

    }

    void Start()
    {
        
    }

    void Update()
    {
        AlignPlane();

        //Detect when there is a mouse click
        if (Input.GetMouseButton(0))
        {
            Debug.Log("[+] Mouse clicked");

            //Create a ray from the Mouse click position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.Log("ray = " + ray);

            //Initialise the enter variable
            float enter = 0.0f;
            Debug.Log("enter = " + enter);

            if (plane.Raycast(ray, out enter))
            {
                Debug.Log("[+] Raycast");
                point_on_plane = ray.GetPoint(enter);
                heightInputField.gameObject.SetActive(true);
            }
        }
    }
}

//debug raycast