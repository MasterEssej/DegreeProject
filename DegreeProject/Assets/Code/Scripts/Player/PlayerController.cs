using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 10f;
    
    private Vector3 moveInputTopDown;
    private float moveInputSideScroller;
    private Vector3 moveVector;
    private Rigidbody rb;

    [SerializeField]
    private Sprite TopDown;

    [SerializeField]
    private Sprite SideScroller;

    [SerializeField]
    private ControlMode mode = ControlMode.TopDown;


    public GameObject TopDownCamera;
    public GameObject SideScrollerCamera;

    private float cameraRotation = 0f;
    private float rotationFixer = 1f;

    private InputMaster controls;


    public TilemapPopulator tmp;

    [SerializeField]
    private Tilemap SideScrollerMap;

    private int tileRotatorX = 1;
    private int tileRotatorZ = 1;
    private int angleInt = 1;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        controls = new InputMaster();
        controls.Enable();
        



        TopDownCamera.gameObject.SetActive(true);

        //when toggling to TD
        rb.useGravity = false;
        rb.isKinematic = true;

        //UnbindSideScrollerControls();
        BindTopDownControls();
        BindGeneral();
        mode = ControlMode.TopDown;
    }

    private void FixedUpdate()
    {
        switch (mode)
        {
            case ControlMode.TopDown:
                TopDownMove();
                break;
            case ControlMode.SideScroller:
                SideScrollerMove();
                break;
        }
    }


    private void BindTopDownControls()
    {
        Debug.Log("Bound topDown");

        controls.TopDown.Move.performed += OnMoveInputTopDown;
        controls.TopDown.Move.canceled += OnMoveInputTopDown;
    }


    private void UnbindTopDownControls()
    {
        controls.TopDown.Move.performed -= OnMoveInputTopDown;
        controls.TopDown.Move.canceled  -= OnMoveInputTopDown;
    }


    private void BindSideScrollerControls()
    {
        Debug.Log("Bound sideScroller");
        controls.SideScroller.LeftRight.performed += OnMoveInputSideScroller;
        controls.SideScroller.LeftRight.canceled += OnMoveInputSideScroller;
        controls.SideScroller.Jump.performed += OnJumpInput;
    }

    private void UnbindSideScrollerControls()
    {
        controls.SideScroller.LeftRight.performed -= OnMoveInputTopDown;
        controls.SideScroller.LeftRight.canceled -= OnMoveInputTopDown;
        controls.SideScroller.Jump.performed -= OnJumpInput;
    }

    private void BindGeneral()
    {
        Debug.Log("Bound General");
        controls.General.SwitchView.performed += SwitchMode;
        controls.General.RotateView.performed += OnRotateInput;
    }

    private void UnbindGeneral()
    {
        controls.General.SwitchView.performed -= SwitchMode;
    }

    private void OnMoveInputTopDown(InputAction.CallbackContext obj)
    {
        moveInputTopDown = Quaternion.AngleAxis(cameraRotation, Vector3.up) * obj.ReadValue<Vector3>();
    }

    private void OnMoveInputSideScroller(InputAction.CallbackContext obj)
    {
        moveInputSideScroller = obj.ReadValue<float>();
    }

    private void OnJumpInput(InputAction.CallbackContext obj)
    {
        Debug.Log("Jumping");
        rb.AddForce(Vector3.up * 500);
    }

    private void OnRotateInput(InputAction.CallbackContext obj)
    {
        float rotationInput = obj.ReadValue<float>();

        rotationFixer = -rotationFixer;

        Transform scCam = SideScrollerCamera.transform;
        scCam.position = new Vector3(scCam.position.z * rotationInput * rotationFixer, scCam.position.y, scCam.position.x * rotationInput * rotationFixer);


        Vector3 rotationTD = new(0, 0, rotationInput*90);
        Vector3 rotationSC = new(0, rotationInput*-90, 0);
        Vector3 rotationTilemap = new(0, rotationInput*-90, 0);

        TopDownCamera.transform.Rotate(rotationTD);
        scCam.Rotate(rotationSC);

        SideScrollerMap.transform.Rotate(rotationTilemap);

        //cameraRotation += rotationInput;

        cameraRotation = scCam.rotation.eulerAngles.y;
        if(cameraRotation > 90 || cameraRotation < -90)
        {
            tileRotatorX = -1;
        }
        else
        {
            tileRotatorX = 1;
        }
        if(cameraRotation == 270) // fix plz
        {
            tileRotatorZ = 1;
        }
        else
        {
            tileRotatorZ = -1;
        }

    }

    private void TopDownMove()
    {
        rb.MovePosition(rb.position + moveSpeed * Time.fixedDeltaTime * moveInputTopDown);

    }

    private void SideScrollerMove()
    {
        var modifier = moveSpeed * Time.fixedDeltaTime * moveInputSideScroller;
        rb.MovePosition(rb.position + new Vector3(modifier, 0, 0));
    }


    private void SwitchMode(InputAction.CallbackContext obj)
    {
        Debug.Log($"switchmode called {mode}");
        switch (mode)
        {
            case ControlMode.none:
                //SideScrollerCamera.gameObject.SetActive(false);



            case ControlMode.TopDown:
                tmp.FetchSCMap(tileRotatorX, tileRotatorZ, rotationFixer);
                SideScrollerCamera.gameObject.SetActive(true);
                TopDownCamera.gameObject.SetActive(false);

                //when toggling to SC
                rb.useGravity = true;
                rb.isKinematic = false;

                BindSideScrollerControls();
                UnbindTopDownControls();

                mode = ControlMode.SideScroller;
                break;


            case ControlMode.SideScroller:
                SideScrollerCamera.gameObject.SetActive(false);
                TopDownCamera.gameObject.SetActive(true);

                //when toggling to TD
                rb.useGravity = false;
                rb.isKinematic = true;





                UnbindSideScrollerControls();
                BindTopDownControls();

                mode = ControlMode.TopDown;
                break;
        }
    }


    public enum ControlMode
    {
        none,
        TopDown,
        SideScroller,
    }

}
