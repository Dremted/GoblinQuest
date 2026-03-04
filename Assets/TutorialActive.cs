using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialActive : MonoBehaviour
{
    [Header("Tutorial")]
    [SerializeField] private GameObject tutorialSprite;
    [SerializeField] private GameInput gameInput;

    [Header("Portret")]
    [SerializeField] private GameObject StandartPortret;
    [SerializeField] private GameObject ShiftPortret;

    private void Awake()
    {
        tutorialSprite.SetActive(false);
        ShiftPortret.SetActive(false);
    }

    private void OnEnable()
    {
        gameInput.OnTutorial += OnTutorialActive;
        gameInput.OnShiftActive += OnShiftActivePortret;
    }


    private void OnDisable()
    {
        gameInput.OnTutorial -= OnTutorialActive;
        gameInput.OnShiftActive -= OnShiftActivePortret;
    }

    private void OnShiftActivePortret(object sender, EventArgs e)
    {
        if(gameInput.CameraMode())
        {
            StandartPortret.SetActive(false);
            ShiftPortret.SetActive(true);
        }
        else
        {
            StandartPortret.SetActive(true);
            ShiftPortret.SetActive(false);
        }
    }

    private void OnTutorialActive(object sender, EventArgs e)
    {
        tutorialSprite.SetActive(!tutorialSprite.activeSelf);
    }
}
