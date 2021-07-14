using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public static class QuaternionHelpers
{
    public static Quaternion Up = Quaternion.FromToRotation(Vector3.zero, Vector3.up);
    public static Quaternion Right = Quaternion.FromToRotation(Vector3.zero, Vector3.right);
    public static Quaternion Forward = Quaternion.FromToRotation(Vector3.zero, Vector3.forward);
}
public class PlayerController : MonoBehaviour
{
    [SerializeField] private CharacterController character;
    [SerializeField] private float speed = 0.5f;
    [SerializeField] private float turnSpeed = 0.5f;
    [SerializeField] private Texture2D crosshair;
    
    [SerializeField, BoxGroup("Constrain Rot - X")] private bool constrainX;
    [SerializeField, BoxGroup("Constrain Rot - X")] private float minRotX;
    [SerializeField, BoxGroup("Constrain Rot - X")] private float maxRotX;
    
    [SerializeField, BoxGroup("Constrain Rot - Y")] private bool constrainY;
    [SerializeField, BoxGroup("Constrain Rot - Y")] private float minRotY;
    [SerializeField, BoxGroup("Constrain Rot - Y")] private float maxRotY;
    
    [SerializeField, BoxGroup("Constrain Rot - Z")] private bool constrainZ;
    [SerializeField, BoxGroup("Constrain Rot - Z")] private float minRotZ;
    [SerializeField, BoxGroup("Constrain Rot - Z")] private float maxRotZ;

    private Vector3 _lastMousePos;

    private void Awake()
    {
        character ??= GetComponent<CharacterController>();
        _lastMousePos = Input.mousePosition;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
        Cursor.SetCursor(crosshair, new Vector2(0.5f, 0.5f), CursorMode.Auto);
    }

    // Update is called once per frame
    void Update()
    {
        var hor = Input.GetAxis("Horizontal");
        var ver = Input.GetAxis("Vertical");
        var moveSpeed = new Vector3(
            hor * speed,
            0f,
            ver * speed);
        
        character.SimpleMove(transform.TransformVector(moveSpeed));

        var mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        
        Rotate(mouseDelta);
    }

    private void Rotate(Vector2 input)
    {
        var rotationAxis = new Vector3(-input.y, input.x, 0f);
        var turnAmount = turnSpeed * Time.deltaTime;
        transform.Rotate(rotationAxis, turnAmount);
        
        ConstrainRotation();
    }

    private void ConstrainRotation()
    {
        // constrain
        if (!(constrainX || constrainY || constrainZ)) return;
        
        var oldRot = transform.eulerAngles;
        transform.eulerAngles = new Vector3(
            constrainX ? Mathf.Clamp(oldRot.x, minRotX, maxRotX) : oldRot.x,
            constrainY ? Mathf.Clamp(oldRot.y, minRotY, maxRotY) : oldRot.y,
            constrainZ ? Mathf.Clamp(oldRot.z, minRotZ, maxRotZ) : oldRot.z
        );
    }
}