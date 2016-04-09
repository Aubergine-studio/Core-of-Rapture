using UnityEngine;

public class PlayerInputMenager : InputMenager
{
    private PlayerPlatformerController2D PlayerPlatformerController2D;
    private new void Start()
    {
        base.Start();
        PlayerPlatformerController2D = (CharacterController as PlayerPlatformerController2D);
    }

    public override void CollectInputs()
    {
        GamePad.GetInputs();
    }

    public override void ControlPhysicsActions()
    {
        PlayerPlatformerController2D.PlatformerMove2D(GamePad.LefAnalog.Reading);
        //if(GamePad.ActionButtons.ButtonYDown || GamePad.ActionButtons.ButtonXDown || GamePad.ActionButtons.ButtonADown || GamePad.ActionButtons.ButtonBDown)
        //   Debug.Log("Down");

        //if (GamePad.ActionButtons.ButtonYUp || GamePad.ActionButtons.ButtonXUp || GamePad.ActionButtons.ButtonAUp || GamePad.ActionButtons.ButtonBUp)
        //    Debug.Log("Up");
        
        PlayerPlatformerController2D.Jump(GamePad.ActionButtons.ButtonBDown);
    }

    public override void ControlActions()
    {
    }
}