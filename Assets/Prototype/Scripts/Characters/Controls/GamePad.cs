using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///
/// </summary>
public enum InputSource
{
    KeyboardAndMouse,
    GamePad1,
    GamePad2,
    GamePad3,
    GamePad4
}

public enum AnalogStick
{
    Left,
    Right
}

public abstract class Inputs
{
    public InputSource InputSource { get; set; }

    public abstract void GetInputs();
}

[Serializable]
public class ActionButtons : Inputs
{
    #region Przytrzymanie

    [SerializeField]
    private bool _checkHold = true;

    public bool ButtonY { get; private set; }

    public bool ButtonB { get; private set; }

    public bool ButtonX { get; private set; }

    public bool ButtonA { get; private set; }

    #endregion Przytrzymanie

    #region Nasiśnięcie

    [SerializeField]
    private bool _checkUp = true;

    public bool ButtonYUp { get; private set; }

    public bool ButtonBUp { get; private set; }

    public bool ButtonXUp { get; private set; }

    public bool ButtonAUp { get; private set; }

    #endregion Nasiśnięcie

    #region Puszczenie

    [SerializeField]
    private bool _checkDown = true;

    public bool ButtonYDown { get; private set; }

    public bool ButtonBDown { get; private set; }

    public bool ButtonXDown { get; private set; }

    public bool ButtonADown { get; private set; }

    #endregion Puszczenie

    public override void GetInputs()
    {
        if (_checkHold)
        {
            ButtonY = Input.GetButton("ButtonY" + InputSource);
            ButtonB = Input.GetButton("ButtonB" + InputSource);
            ButtonX = Input.GetButton("ButtonX" + InputSource);
            ButtonA = Input.GetButton("ButtonA" + InputSource);
        }
        if (_checkUp)
        {
            ButtonYUp = Input.GetButtonUp("ButtonY" + InputSource);
            ButtonBUp = Input.GetButtonUp("ButtonB" + InputSource);
            ButtonXUp = Input.GetButtonUp("ButtonX" + InputSource);
            ButtonAUp = Input.GetButtonUp("ButtonA" + InputSource);
        }

        if (_checkDown)
        {
            ButtonYDown = Input.GetButtonDown("ButtonY" + InputSource);
            ButtonBDown = Input.GetButtonDown("ButtonB" + InputSource);
            ButtonXDown = Input.GetButtonDown("ButtonX" + InputSource);
            ButtonADown = Input.GetButtonDown("ButtonA" + InputSource);
        }
    }
}

/// <summary>
///
/// </summary>
[Serializable]
public class Analog : Inputs
{
    public AnalogStick AnalogStick { get; private set; }

    public bool InverVertical = true;

    public float Horizontal { get; private set; }

    public float Vertical { get; private set; }

    public Vector3 Reading
    {
        get
        {
            return (Vertical * Vector3.forward) + (Horizontal * Vector3.right);
        }
    }

    /// <summary>
    ///
    /// </summary>
    public Analog(AnalogStick analogStick)
    {
        Horizontal = Vertical = 0;
        AnalogStick = analogStick;
    }

    /// <summary>
    ///
    /// </summary>
    override public void GetInputs()
    {
        Horizontal = Input.GetAxis("Horizontal" + AnalogStick + InputSource);
        Vertical = Input.GetAxis("Vertical" + AnalogStick + InputSource);

        if (InverVertical) Vertical *= -1;
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="directionSpeed"></param>
    /// <param name="rootTransform"></param>
    /// <param name="cameraTransform"></param>
    /// <param name="directionOut"></param>
    /// <param name="speedOut"></param>
    public void AnalogToWorldSpace(float directionSpeed, Transform rootTransform, Transform cameraTransform,
        ref float directionOut, ref float speedOut, bool isPivoting, ref float angleOut)
    {
        var rootDirection = rootTransform.forward;
        var stickDirection = new Vector3(Horizontal, 0, Vertical);
        speedOut = stickDirection.sqrMagnitude;
        var cameraDirection = cameraTransform.forward;
        cameraDirection.y = 0;
        var referentialShift = Quaternion.FromToRotation(Vector3.forward, cameraDirection);

        var moveDirection = referentialShift * stickDirection;
        var axisSign = Vector3.Cross(moveDirection, rootDirection);

        //Debug.DrawRay(new Vector3(rootTransform.position._starUpVerticalAngle, rootTransform.position._startUpHorizontalAngle + 2f, rootTransform.position.z), stickDirection, Color.blue);
        //Debug.DrawRay(new Vector3(rootTransform.position._starUpVerticalAngle, rootTransform.position._startUpHorizontalAngle + 2f, rootTransform.position.z), axisSign, Color.red);
        //Debug.DrawRay(new Vector3(rootTransform.position._starUpVerticalAngle, rootTransform.position._startUpHorizontalAngle + 2f, rootTransform.position.z), rootDirection, Color.magenta);

        var angleRootToMove = Vector3.Angle(rootDirection, moveDirection) * (axisSign.y > 0 ? -1f : 1f);
        if (!isPivoting)
        {
            angleOut = angleRootToMove;
        }

        angleRootToMove /= 180;
        directionOut = angleRootToMove * directionSpeed;
    }

    public override string ToString()
    {
        return "Horizontal: " + Horizontal + ", Vertical: " + Vertical;
    }
}

[Serializable]
public class GamePad : Inputs
{
    public InputSource Sorce
    {
        get { return InputSource; }
        set
        {
            InputSource = value;
            foreach (var i in _inputs)
            {
                i.InputSource = InputSource;
            }
        }
    }

    public Analog LefAnalog = new Analog(AnalogStick.Left);
    public Analog RightAnalog = new Analog(AnalogStick.Right);
    public ActionButtons ActionButtons = new ActionButtons();

    private readonly List<Inputs> _inputs = new List<Inputs>();

    public GamePad()
    {
        _inputs.Add(LefAnalog);
        _inputs.Add(RightAnalog);
        _inputs.Add(ActionButtons);
    }

    public override void GetInputs()
    {
        foreach (var i in _inputs)
        {
            i.GetInputs();
        }
    }
}