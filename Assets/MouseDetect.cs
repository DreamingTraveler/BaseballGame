using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseDetect : MonoBehaviour {

    private Vector3 InitialPosition;
    private float screenHight;
    private float batPositionX = Screen.width / 2;
    private float batPositionY = Screen.height / 2;
    private float mousePositionY;
    private float mousePositionX;
    private float diffPositionX;
    private float diffPositionY;
    private float swingDegree;
    private float degree = 0;
    private bool swing;
    private bool finish = true;
    public float batRange;

    // Use this for initialization
    void Start () {
        screenHight = Screen.height;
        //GameObject.Find("Bat").GetComponent<Transform>().rotation = Quaternion.identity;
        //InitialPosition = GameObject.Find("Bat").GetComponent<Transform>().rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && finish == true)
        {
            Debug.Log(Input.mousePosition);
            mousePositionX = Input.mousePosition.x;
            mousePositionY = Input.mousePosition.y;
            diffPositionX = (mousePositionX - batPositionX);
            diffPositionY = (mousePositionY - batPositionY);
            CalculateDegree();
            swing = true;
            finish = false;
        }
        if (swing)
        {
            Swing();
            degree += 0.05f;
            if (degree >= 1)
            {
                swing = false;
            }
        }
        else if (swing == false && degree > 0)
        {
            if (degree > 0 && degree < 0.1)
                finish = true;
            degree -= 0.05f;
            Swing();
        }
    }

    void Swing()
    {
        Quaternion initail = Quaternion.Euler(0, 0, -20);
        Quaternion target = Quaternion.Euler(0, 180, swingDegree);
        GameObject.Find("Bat").GetComponent<Transform>().rotation = Quaternion.Slerp(initail, target, degree);
    }

    void CalculateDegree()
    {
        float slide = Mathf.Sqrt(diffPositionX * diffPositionX + diffPositionY * diffPositionY);
        swingDegree = ((diffPositionX * diffPositionX + slide * slide - diffPositionY * diffPositionY) / (2 * diffPositionX * slide));
        swingDegree = Mathf.Acos(swingDegree);
        swingDegree *= (float)(180.0 / Mathf.PI);
        Debug.Log(swingDegree);
        if (mousePositionY > batPositionY)
            swingDegree = -swingDegree;
    }
}
