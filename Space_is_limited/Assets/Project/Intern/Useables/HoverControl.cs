using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Bewegt ein Objekt mit der Eingabe des Spielers, (!darf nicht zu beginn aktiviert sein!)
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class HoverControl : MonoBehaviour
{
    [SerializeField] private float _MaxSpeed = 30f;
    [SerializeField] private float _MaxAcceleration = 80f;

    private Vector2 _Velocity = new Vector2(0f, 0f);

    private Rigidbody2D _RB;
    private Rigidbody2D RB
    {
        get
        {
            _RB = _RB != null ? _RB : GetComponent<Rigidbody2D>();
            return _RB;
        }
    }

    /// <summary>
    /// Aktiviert die Bewegung sobald die Komponente aktiviert wird
    /// </summary>
    public void OnEnable()
    {
        PlayerInput.Instance.OnSidewardValue.AddListener(MoveSideward);
        PlayerInput.Instance.OnUpsideValue.AddListener(MoveUpside);
        RB.gravityScale = 0;
    }

    /// <summary>
    /// Deaktiviert die Bewegung sobald die Komponente deaktiviert wird
    /// </summary>
    public void OnDisable()
    {
        PlayerInput.Instance.OnSidewardValue.RemoveListener(MoveSideward);
        PlayerInput.Instance.OnUpsideValue.RemoveListener(MoveUpside);
        RB.gravityScale = 9.81f;
    }

    /// <summary>
    /// Bewegt das Objekt seitw�rts
    /// Indem es die Geschwindigkeit setzt f�r das FixedUpdate
    /// </summary>
    /// <param name="value">Ver�nderung in Richtung</param>
    public void MoveSideward(float value)
    {
        _Velocity.x = value * _MaxSpeed;
    }

    /// <summary>
    /// Bewegt das Objekt herauf oder runter
    /// Indem es die Geschwindigkeit setzt f�r das FixedUpdate
    /// </summary>
    /// <param name="value">Ver�nderung in H�he</param>
    public void MoveUpside(float value)
    {
        _Velocity.y = value * _MaxSpeed;
    }

    private void Update()
    {
        // Energy Costs
    }

    /// <summary>
    /// Beschleunigt das Objekt auf die gew�nschte Geschwindigkeit mit der gew�nschten Beschleunigung
    /// </summary>
    private void FixedUpdate()
    {
        RB.AddForce(HelpFunctions.GetAccelerationVelocity(_Velocity, RB.velocity, _MaxAcceleration), ForceMode2D.Impulse);
    }
}
