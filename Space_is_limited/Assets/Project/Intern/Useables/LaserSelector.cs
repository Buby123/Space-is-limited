using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;
using Intern.UI;

/// <summary>
/// Is used for the Hoverer to Select and Connect the player to objects
/// It uses a LineRenderer to draw a line from the player to the object
/// And it uses a Raycast to check if the object is still in range
/// </summary>
[RequireComponent(typeof(LineRenderer))]
public class LaserSelector : Singleton<LaserSelector>
{
    [Tooltip("The offset of the laser from the player")]
    [SerializeField] private Vector3 LaserOffset = new(0f, 0.5f, 0f);

    [Tooltip("The LineRenderer used to draw the connection")]
    private LineRenderer _LineRenderer;

    [Tooltip("The maximum range of the connection")]
    private float _MaxRange = 0f;
    [Tooltip("The object that is currently connected to the player")]
    private Transform _ConnectedObject = null;
    [Tooltip("The LayerMask used to check if the object is still in range")]
    private LayerMask _ConnectionLayerMask = 0;
    [Tooltip("The Action that is called when the connection ends")]
    private UnityAction _OnEndConnection;

    /// <summary>
    /// Initializes the LineRenderer
    /// </summary>
    private void Start()
    {
        _LineRenderer = GetComponent<LineRenderer>();
    }

    #region Connection Draw
    /// <summary>
    /// Draws the connection between the player and the object
    /// 
    /// Is used to update the connection
    /// </summary>
    private void Update()
    {
        var startPos = transform.position + LaserOffset;

        if (_ConnectedObject != null)
        {
            var endPos = _ConnectedObject.position;
            DrawConnection(startPos, endPos);
            TestConnectionEnd(startPos, endPos, _ConnectedObject.gameObject);
        }
        else
        {
            EndConnection();
        }
    }

    /// <summary>
    /// Initializes a connection between the player and the object
    /// </summary>
    /// <param name="trackObj">Target Object that should be connected with the obj of the line renderer</param>
    /// <param name="connectionLayerMask">Layermask of objects that disrupt the connection and of the target object</param>
    /// <param name="maxRange">Maximum range of the laser</param>
    /// <param name="onEndConnection">Action when the connection breaks</param>
    public void BeginConnection(Transform trackObj, LayerMask connectionLayerMask, float maxRange = 10, UnityAction onEndConnection = null)
    {
        if (_ConnectedObject != null)
            return;

        InvokeRepeating(nameof(ReduceEnergy), 0f, 1f);
        _ConnectedObject = trackObj;
        _OnEndConnection = onEndConnection;
        _ConnectionLayerMask = connectionLayerMask;
        _MaxRange = maxRange;
    }

    /// <summary>
    /// Ends the connection between the player and the object
    /// </summary>
    public void EndConnection()
    {
        CancelInvoke();
        _OnEndConnection = null;
        _ConnectedObject = null;
        _LineRenderer.positionCount = 0;
    }

    /// <summary>
    /// Test if the connection ends by using a raycast with the maximum length
    /// from the player to the target. If the target hits the laser stays
    /// </summary>
    /// <param name="startPos">Starting Position</param>
    /// <param name="endPos">Target Position</param>
    private void TestConnectionEnd(Vector3 startPos, Vector3 endPos, GameObject ExspectedTarget)
    {
        if (ExspectedTarget == null)
        {
            return;
        }

        var direction = endPos - startPos;
        var Hit = Physics2D.Raycast(startPos, direction, _MaxRange, _ConnectionLayerMask);

        if (Hit.collider == null)
        {
            _OnEndConnection.Invoke();
            return;
        }
        
        if (Hit.collider.gameObject != ExspectedTarget)
        {
            Debug.Log(ExspectedTarget + " is not " + Hit.collider.gameObject.name);
            _OnEndConnection.Invoke();
        }
    }

    /// <summary>
    /// Reduce Energy and when no Energy is left the connection ends
    /// </summary>
    private void ReduceEnergy()
    {
        if (!EnergyBar.Instance.reduceEnergy(1))
        {
            _OnEndConnection.Invoke();
        }
    }

    /// <summary>
    /// Draws a line from the player to the object or resets the connection
    /// </summary>
    /// <param name="startPos">Starting Position</param>
    /// <param name="endPos">End Position</param>
    private void DrawConnection(Vector3 startPos, Vector3 endPos)
    {
        _LineRenderer.positionCount = 2;
        _LineRenderer.SetPositions(new Vector3[] { startPos, endPos });
    }
    #endregion

    #region Line Functions
    /// <summary>
    /// Draws a line from the player to the given position and returns the object that is hit
    /// It finds the forward direction of the player and draws a ray from the player to the given position
    /// </summary>
    /// <param name="range">length of the ray</param>
    /// <param name="_LayerMask">physics layers that are viewed</param>
    /// <returns></returns>
    public GameObject GetObjectInFront(float range, LayerMask _LayerMask)
    {
        var forward = PlayerController.Instance.FlippedLeft ? -transform.right : transform.right;
        var origin = transform.position + new Vector3(0f, 0.5f, 0f);

        var Hit = Physics2D.Raycast(origin, forward, range, _LayerMask);

        if (Hit.collider != null)
        {
            DrawLineTo(origin, forward, Hit.distance);
            return Hit.collider.gameObject;
        }
        else
        {
            DrawLineTo(origin, forward, range);
            return default;
        }
    }

    /// <summary>
    /// Draw a line like a raycast
    /// </summary>
    /// <param name="origin">Origin point</param>
    /// <param name="forward">Forward direction of ray</param>
    /// <param name="range">Range of the ray</param>
    private void DrawLineTo(Vector3 origin, Vector3 forward, float range)
    {
        var secondPos = origin + forward * range;

        _LineRenderer.positionCount = 2;
        _LineRenderer.SetPositions(new Vector3[] { origin, secondPos });
        Invoke(nameof(ResetLine), 0.1f);
    }

    /// <summary>
    /// Resets the line from line renderer
    /// </summary>
    private void ResetLine()
    {
        _LineRenderer.positionCount = 0;
    }
    #endregion
}
