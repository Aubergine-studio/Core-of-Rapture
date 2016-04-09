using UnityEngine;

public abstract class InputMenager : MonoBehaviour
{
    #region Konfiguraca

    protected CharacterController CharacterController;

    #endregion Konfiguraca

    #region Watości wejść

    public GamePad GamePad = new GamePad();

    #endregion Watości wejść

    #region Metody wywodzące się z MonoBehaviour UnityEngine

    /// <summary>
    ///
    /// </summary>
    protected void Start()
    {
        CharacterController = GetComponent<CharacterController>();
    }

    /// <summary>
    ///
    /// </summary>
    private void FixedUpdate()
    {
        ControlPhysicsActions();
    }

    /// <summary>
    ///
    /// </summary>
    private void Update()
    {
        CollectInputs();
        ControlActions();
    }

    #endregion Metody wywodzące się z MonoBehaviour UnityEngine

    #region Metody abstrakcyjne

    /// <summary>
    ///
    /// </summary>
    public abstract void CollectInputs();

    /// <summary>
    ///
    /// </summary>
    public abstract void ControlPhysicsActions();

    /// <summary>
    ///
    /// </summary>
    public abstract void ControlActions();

    #endregion Metody abstrakcyjne
}