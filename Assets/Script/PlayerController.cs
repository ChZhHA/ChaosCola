using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public Shower showerL;
    public Shower showerR;
    public Vector2 showerSpeedRange;
    public Handler handlerL;
    public Handler handlerR;

    private float _lastActionTime = 0;

    private PlayerInput _playerInput;

    private InputAction _leftStick;
    private InputAction _rightStick;
    private InputAction _leftTrigger;
    private InputAction _rightTrigger;

    public float colaProgress;
    public bool isCoca;
    public bool isPepsi;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        _playerInput = GetComponent<PlayerInput>();
        _leftStick = _playerInput.actions.FindAction("LeftStick");
        _rightStick = _playerInput.actions.FindAction("RightStick");
        _leftTrigger = _playerInput.actions.FindAction("LeftTrigger");
        _rightTrigger = _playerInput.actions.FindAction("RightTrigger");
    }

    // Update is called once per frame
    void Update()
    {
        var inputL = _leftStick.ReadValue<Vector2>();
        // var progressL = Mathf.Clamp01(-inputL.y);

        var inputR = _rightStick.ReadValue<Vector2>();
        // var progressR = Mathf.Clamp01(-inputR.y);
        if (inputR.y > 0.9f || inputL.y > 0.9f)
        {
            Door.instance.Open();
        }

        if (inputR.y < -0.9f || inputL.y < -0.9f)
        {
            if (ColaBin.instance != null) ColaBin.instance.Close();
        }

        if (inputR.x < -0.9f || inputL.x < -0.9f)
        {
            if (ColaBin.instance != null) ColaBin.instance.Send();
        }

        var inputLT = _leftTrigger.ReadValue<float>();
        var inputRT = _rightTrigger.ReadValue<float>();

        var progressL = inputLT;
        var progressR = inputRT;
        handlerL.progress = progressL;
        handlerR.progress = progressR;
        if (progressL > 0)
        {
            var rate = (showerSpeedRange.y - showerSpeedRange.x) * progressL + showerSpeedRange.x;
            showerL.spawn_rate = rate;
            _lastActionTime = Time.time;
            colaProgress += rate * Time.deltaTime;
            isCoca = true;
        }
        else
        {
            showerL.spawn_rate = 0;
            isCoca = false;
        }

        if (progressR > 0)
        {
            var rate = (showerSpeedRange.y - showerSpeedRange.x) * progressR + showerSpeedRange.x;
            showerR.spawn_rate = rate;
            _lastActionTime = Time.time;
            colaProgress += rate * Time.deltaTime;
            isPepsi = true;
        }
        else
        {
            showerR.spawn_rate = 0;
            isPepsi = false;
        }

        Shader.SetGlobalFloat("_BubbleEdge", Mathf.Clamp01((Time.time - _lastActionTime) * 0.1f));
        // if (colaProgress > 300)
        // {
        //     Debug.Log("满了");
        // }
    }
}