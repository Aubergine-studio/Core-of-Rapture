using UnityEngine;

public class PlayerInputMenager : InputMenager
{
    
    public override void Collect()
    {
        _horizontalLeftAnalog = Input.GetAxis("Horizontal"+_inputSorce);
        _verticalLeftAalog = Input.GetAxis("Vertical" + _inputSorce);
    }

    public override void Control()
    {
        _character.Move2D(_horizontalLeftAnalog);
    }

    void FixedUpdate()
    {
        Collect();
    }

    void Update()
    {
        Control();
    }
}