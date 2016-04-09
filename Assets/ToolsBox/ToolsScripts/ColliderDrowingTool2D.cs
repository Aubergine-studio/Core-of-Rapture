using UnityEngine;

/// <summary>
/// Klasa rysująca wskaźniki gizmo domen kolizji, obiektów do których jest
/// dołączona.
/// </summary>
public class ColliderDrowingTool2D : MonoBehaviour
{
    #region Zmienne

    /// <summary>
    /// Lista domen kolizji
    /// </summary>
    private Collider2D[] _collider2DList;

    #endregion Zmienne

    #region Zdarzenia

    /// <summary>
    /// Metoa rysujaca wskaźniki gizmo na ekranie.
    /// </summary>
    public void OnDrawGizmos()
    {
        _collider2DList = GetComponents<Collider2D>();

        foreach (var c in _collider2DList)
        {
            var oldCollor = Gizmos.color;
            Gizmos.color = Color.green;

            if (c is BoxCollider2D)
            {
                var boxCollider2D = c as BoxCollider2D;
                Gizmos.DrawWireCube(new Vector3(boxCollider2D.transform.position.x + boxCollider2D.offset.x, boxCollider2D.transform.position.y + boxCollider2D.offset.y, boxCollider2D.transform.position.z), boxCollider2D.size);
            }

            if (c is CircleCollider2D)
            {
                var circleCollider2D = c as CircleCollider2D;
                Gizmos.DrawWireSphere(new Vector3(circleCollider2D.transform.position.x + circleCollider2D.offset.x, circleCollider2D.transform.position.y + circleCollider2D.offset.y, circleCollider2D.transform.position.z), circleCollider2D.radius);
            }

            if (c is PolygonCollider2D)
            {
                var polygonCollider2D = c as PolygonCollider2D;
                var lenght = polygonCollider2D.points.Length;

                Gizmos.DrawLine(transform.TransformPoint(polygonCollider2D.points[lenght - 1]), transform.TransformPoint(polygonCollider2D.points[0]));

                for (var i = 1; i < lenght; i++)
                {
                    Gizmos.DrawLine(transform.TransformPoint(polygonCollider2D.points[i - 1]), transform.TransformPoint(polygonCollider2D.points[i]));
                }
            }

            Gizmos.color = oldCollor;
        }
    }

    #endregion Zdarzenia
}