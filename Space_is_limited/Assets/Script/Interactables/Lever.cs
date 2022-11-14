using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    [SerializeField] private SpriteRenderer VisualLever;
    [SerializeField] private Color ActiveColor;
    [SerializeField] private Color DeactiveColor;

    private bool active = false;
    private bool canBeChanged = false;
    [SerializeField] private int id;

    private void Awake()
    {
        SetState(active);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        canBeChanged = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        canBeChanged = false;
    }

    private void Update()
    {
        if (canBeChanged)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                SetState(!active);
                EventManager.Instance.PullLever(id, active);
            }
        }
    }

    private void SetState(bool state)
    {
        active = state;

        if (state)
        {
            VisualLever.color = ActiveColor;
        } else
        {
            VisualLever.color = DeactiveColor;
        }
    }
}
