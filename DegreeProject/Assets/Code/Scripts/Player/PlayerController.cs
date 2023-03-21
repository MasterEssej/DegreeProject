using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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

    private InputMaster controls;


    public TilemapPopulator tmp;



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
        controls.SideScroller.LeftRight.performed += OnMoveInputTopDown;
        controls.SideScroller.LeftRight.canceled += OnMoveInputTopDown;
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
    }

    private void UnbindGeneral()
    {
        controls.General.SwitchView.performed -= SwitchMode;
    }

    private void OnMoveInputTopDown(InputAction.CallbackContext obj)
    {
        moveInputTopDown = obj.ReadValue<Vector3>();
    }

    private void OnMoveInputSideScroller(InputAction.CallbackContext obj)
    {
        moveInputSideScroller = obj.ReadValue<float>();
    }

    private void OnJumpInput(InputAction.CallbackContext obj)
    {
        rb.AddForce(Vector3.up * 5);
    }


    private void TopDownMove()
    {
        rb.MovePosition(rb.position + moveSpeed * Time.fixedDeltaTime * moveInputTopDown);

    }

    private void SideScrollerMove()
    {
        var modifier = moveSpeed * Time.fixedDeltaTime * moveInputSideScroller;
        rb.MovePosition(rb.position + new Vector3(modifier, 0 ,0));
    }


    private void SwitchMode(InputAction.CallbackContext obj)
    {
        Debug.Log($"switchmode called {mode.ToString()}");
        switch (mode)
        {
            case ControlMode.none:
                //SideScrollerCamera.gameObject.SetActive(false);



            case ControlMode.TopDown:
                tmp.FetchSCMap();
                SideScrollerCamera.gameObject.SetActive(true);
                TopDownCamera.gameObject.SetActive(false);

                //when toggling to SC
                //rb.useGravity = true;
                //rb.isKinematic = false;

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
