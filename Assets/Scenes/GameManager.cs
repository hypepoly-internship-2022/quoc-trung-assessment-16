using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject cube1;
    [SerializeField] GameObject cube2;
    [SerializeField] GameObject positionAObj;
    [SerializeField] GameObject positionBObj;
    [SerializeField] Button button;
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] float cube2WaitTime = 2f;
    [SerializeField] float cube1MoveTime = 2f;
    [SerializeField] float cube1MoveBackTime = 2f;
    [SerializeField] float cube2ScaleTime = 2f;
    Sequence sequence;
    Vector3 cube2Scale;
    Vector3 positionA;
    Vector3 positionB;
    bool fadeOut;

    void Start()
    {
        positionB = positionBObj.transform.position;
        cube2Scale = cube2.transform.localScale;
        sequence = DOTween.Sequence();
        fadeOut = false;
    }

    void Update()
    {
        StartHideUI();
    }

    public void OnButton1Click()
    {
        button.interactable = false;
        sequence.Append(cube1.gameObject.transform.DOMove(positionB + new Vector3(3, 0, 0), cube1MoveTime).OnComplete(() =>
        {
            cube1.gameObject.transform.DOMove(positionB, cube1MoveBackTime).SetEase(Ease.InOutSine).OnComplete(() =>
            {
                cube2.gameObject.transform.DOScale(cube2Scale, cube2WaitTime).OnComplete(() =>
                {
                    cube2.gameObject.transform.DOScale(cube2Scale * 2, cube2ScaleTime).OnComplete(() =>
                    {
                        cube2.gameObject.transform.DOScale(cube2Scale, cube2ScaleTime).OnComplete(() =>
                        {
                            HideUI();
                        });
                    });
                });
            });
        }));  
    }

    void HideUI()
    {
        fadeOut = true;
    }

    void StartHideUI()
    {
        if(fadeOut == true)
        {
            if(canvasGroup.alpha >= 0)
            {
                canvasGroup.alpha -= Time.deltaTime;
                if(canvasGroup.alpha == 0)
                {
                    fadeOut = false;
                }
            }
        }
    }
}
