using UnityEngine;

[RequireComponent(typeof(PlayerInputMenager))]
public class PlayerPlatformerController2D : CharacterController
{
    public int PlayerNumber { get; private set; }

    public bool FlipSpriteOn = true;
    private bool _isFacingLeft = true;

    [SerializeField]
    private float _moveSpeed = 5f;

    /// <summary>
    /// Wywoływana przy stworzeniu obiektu gracza.
    ///     - Rejestruje gracza
    /// </summary>
    private new void Start()
    {
        base.Start();
        var split = name.Split('_');
        PlayerNumber = int.Parse(split[1]);
        PlayerManager.Instance.RegisterPlayer(gameObject);
    }

    public void PlatformerMove2D(Vector3 leftAnalog)
    {
        FlipSprite(leftAnalog.x);
        Animator.SetFloat(HashIDs.SpeedFloat, Mathf.Abs(leftAnalog.x));
        //Rigidbody2D.AddForce(new Vector2(leftAnalog.x * 10, 0f));
        var position = Transform.position;
        position.x += (leftAnalog.x * _moveSpeed * Time.deltaTime);
        Transform.position = position;
    }

    public void Jump(bool jump)
    {
        Debug.Log(Rigidbody2D.velocity.y);
        Animator.SetFloat(HashIDs.VerticalSpeedFloat, Mathf.Abs(Rigidbody2D.velocity.y));
        IsOnGround = Physics2D.OverlapCircle(_isOnGround.position, 1f, z);
        Animator.SetBool(HashIDs.OnGroundBool, IsOnGround);

        if (jump && IsOnGround)
        {
            Animator.SetTrigger(HashIDs.JumpBool);

            Rigidbody2D.AddForce(new Vector2(0, 500));
        }
    }

    private void FlipSprite(float direction)
    {
        if (!FlipSpriteOn) return;

        if (direction < 0 && !_isFacingLeft)
        {
            Flip();
        }

        if (direction > 0 && _isFacingLeft)
        {
            Flip();
        }
    }

    private void Flip()
    {
        _isFacingLeft = !_isFacingLeft;

        var scale = Transform.localScale;
        scale.x *= -1;
        Transform.localScale = scale;
    }
}