using UnityEngine;

/// <summary>
/// Enum determinujący kierunek poruszania się w projektach 2D.
/// </summary>
public enum Direction2D
{
    Left,
    Right
}

public class CharacterController : MonoBehaviour
{
    #region Referencje na komponenty własne.

    // Ciało fizyczne 3D
    public Rigidbody Rigidbody { get; private set; }

    // Ciało fizyczne 2D
    public Rigidbody2D Rigidbody2D { get; private set; }

    //  Transformacja postaci
    public Transform Transform { get; private set; }

    public Animator Animator { get; private set; }

    #endregion Referencje na komponenty własne.

    #region Referencje na obiekty dzieci.

    protected Transform _isOnGround;
    [SerializeField] protected LayerMask z;

    #endregion Referencje na obiekty dzieci.

    #region Referencje na komponenty obce

    public HashIDs HashIDs { get; protected set; }

    #endregion Referencje na komponenty obce

    #region Statusy postaci

    public bool IsOnGround { get; protected set; }

    #endregion Statusy postaci

    protected void Start()
    {
        //z = LayerMask.NameToLayer("Ground");
        Rigidbody = GetComponent<Rigidbody>();
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Transform = GetComponent<Transform>();
        Animator = GetComponent<Animator>();
        HashIDs = FindObjectOfType<HashIDs>();
        var chldrens = GetComponentsInChildren<Transform>();

        foreach (var c in chldrens)
        {
            if (c.name == "Ground")
            {
                _isOnGround = c;
                break;
            }
        }
    }
}