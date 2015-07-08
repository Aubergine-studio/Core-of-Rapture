using UnityEngine;

public enum InputSource
{
    KeyboardAndMouse,
    GamePad1,
    GamePad2,
    GamePad3,
    GamePad4
}

public abstract class InputMenager : MonoBehaviour
{
    #region Konfiguraca

    protected InputSource _inputSorce;

    public InputSource InputSource
    {
        get { return _inputSorce; }
        set { _inputSorce = value; }
    }

    protected Character _character = null;

    #endregion Konfiguraca

    #region Watości wejść

    protected float _horizontalLeftAnalog = 0;
    protected float _verticalLeftAalog = 0;

    #endregion Watości wejść

    #region Metody wywodzące się z MonoBehaviour UnityEngine

    private void Start()
    {
        _character = GetComponent<Character>();
    }

    #endregion Metody wywodzące się z MonoBehaviour UnityEngine

    #region Metody abstrakcyjne

    public abstract void Collect();

    public abstract void Control();

    #endregion Metody abstrakcyjne
}