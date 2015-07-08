public class Player : Character
{
    public int PlayerNumber { get; private set; }

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
}