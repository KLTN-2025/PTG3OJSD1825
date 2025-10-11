using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRotateAbility : MonoBehaviour
{

    private float _mx;
    private float _my;
    public float RotationSpeed = 200f;
    public Transform CameraRoot;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        _mx = 180f;  
        _my = 0f;    
        UpdateRotation();  
    }

    private void UpdateRotation()
    {
        transform.eulerAngles = new Vector3(0, _mx, 0f);
        CameraRoot.localEulerAngles = new Vector3(-_my, 0, 0f);
    }

    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        _mx += mouseX * RotationSpeed * Time.deltaTime;
        _my += mouseY * RotationSpeed * Time.deltaTime;
        _my = Mathf.Clamp(_my, -90f, 90f);
        UpdateRotation();
    }
}
