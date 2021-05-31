using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    private float mouseX, mouseY;
    [SerializeField] private float _sensitivity = 1f;

    private Camera _mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        _mainCamera = Camera.main;
        if(_mainCamera == null)
        {
            Debug.LogError("Main Camera is null");
        }

        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        CameraRotation();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void CameraRotation()
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        Vector3 currentRotation = transform.localEulerAngles;
        currentRotation.y += mouseX * _sensitivity;
        transform.localRotation = Quaternion.AngleAxis(currentRotation.y, Vector3.up);

        Vector3 currentCameraRotation = _mainCamera.gameObject.transform.localEulerAngles;
        currentCameraRotation.x -= mouseY * _sensitivity; //if +, mouse look is reversed
        //_mainCamera.gameObject.transform.localEulerAngles = currentCameraRotation; 
        //Above works fine but we better use Quaternion. 
        _mainCamera.gameObject.transform.localRotation = Quaternion.AngleAxis(
            Mathf.Clamp(currentCameraRotation.x,0,28), Vector3.right);
    }
}
