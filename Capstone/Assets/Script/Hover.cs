using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class Hover : MonoBehaviour
{
    [SerializeField] private float Up;
    [SerializeField] private float Down;
    public void EnterUp()
    {
        transform.DOLocalMoveX(Up, 0.5f);
    }
    public void ExitDown()
    {
        transform.DOLocalMoveX(Down, 0.5f);
    }
}
