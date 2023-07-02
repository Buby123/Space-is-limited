using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Moves the platform to the next point and when it reaches the next point it will move to the next point
/// </summary>
public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private List<GameObject> Points = new();

    [SerializeField] private float Speed = 1f;

    private int _currentPoint = 0;

    /// <summary>
    /// Moves the platform to the next point and when it reaches the next point it will move to the next point
    /// when the current point is greater than the amount of points it will set the current point to 0
    /// </summary>
    private void Update()
    {
        if (Points.Count == 0)
        {
            return;
        }

        MoveToNextPoint();
    }

    /// <summary>
    /// Moves the platform to the next point and when it reaches the next point it will move to the next point
    /// by increasing the current point by 1
    /// </summary>
    private void MoveToNextPoint()
    {
        if (Vector3.Distance(transform.position, Points[_currentPoint].transform.position) < 0.1f)
        {
            _currentPoint++;
            if (_currentPoint >= Points.Count)
            {
                _currentPoint = 0;
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, Points[_currentPoint].transform.position, Speed * Time.deltaTime);
    }

    #region Move Player
    /// <summary>
    /// Changes the player to a child of the platform so he moves with the platform
    /// </summary>
    /// <param name="collision">Player Collider</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.parent = transform;
        }
    }

    /// <summary>
    /// Ends the movement with the platform
    /// </summary>
    /// <param name="collision">Player Collider</param>
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.parent = null;
        }
    }
    #endregion

    #region Editor
    private void OnDrawGizmos()
    {
        if (Points.Count == 0)
        {
            return;
        }

        for (int i = 0; i < Points.Count; i++)
        {
            if (i + 1 < Points.Count)
            {
                Gizmos.DrawLine(Points[i].transform.position, Points[i + 1].transform.position);
            }
            else
            {
                Gizmos.DrawLine(Points[i].transform.position, Points[0].transform.position);
            }
        }
    }
    #endregion
}
