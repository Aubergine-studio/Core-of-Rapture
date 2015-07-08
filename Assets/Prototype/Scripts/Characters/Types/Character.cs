using System;
using UnityEngine;

/// <summary>
/// Enum determinujący kierunek poruszania się w projektach 2D.
/// </summary>
public enum Direction2D
{
    Left,
    Right
}

public class Character : MonoBehaviour
{
    #region Referencje na komponenty własne.

    // Ciało fizyczne 3D
    public Rigidbody Rigidbody { get; private set; }

    // Ciało fizyczne 2D
    public Rigidbody2D Rigidbody2D { get; private set; }

    #endregion Referencje na komponenty własne.

    #region Statystyki postaci

    public float MaxSpead;

    #endregion Statystyki postaci

    protected void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Poruszanie się postaci wykorzystywane w projektach 3D.
    /// </summary>
    public void Move()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Poruszanie się postaci wykorzystywane w projektach 2D.
    /// </summary>
    /// <param name="direction2D">
    /// Kierunek:
    ///     - Prawo
    ///     - Lewo
    /// </param>
    /// <param name="spead">
    /// Prędkość z jaką porusza się postać.
    /// </param>
    public void Move2D(float spead)
    {
        if (Rigidbody != null)
            Rigidbody.velocity = new Vector3((MaxSpead * spead), Rigidbody.velocity.y);
        else
            Rigidbody2D.velocity = new Vector3((MaxSpead * spead), Rigidbody2D.velocity.y);
    }
}