using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Image bar;
    [SerializeField] private Image background;
    private Trap trap;
    

    private void Awake()
    {
        trap = GetComponent<Trap>();
    }

    private void OnEnable()
    {
        trap.OnStartSetTrap += ProgressBar_StartSetTrap;
        trap.OnStopSetTrap += ProgressBar_StopSetTrap;
        trap.OnProgressSet += ProgressBar_OnProgressSet;
    }

    private void ProgressBar_OnProgressSet(object sender, float e)
    {

        bar.fillAmount = e;
    }

    private void OnDisable()
    {
        trap.OnStartSetTrap -= ProgressBar_StartSetTrap;
        trap.OnStopSetTrap -= ProgressBar_StopSetTrap;
    }

    private void ProgressBar_StartSetTrap(object sender, EventArgs e)
    {
        bar.gameObject.SetActive(true);
        background.gameObject.SetActive(true);
    }

    private void ProgressBar_StopSetTrap(object sender, EventArgs e)
    {
        bar.gameObject.SetActive(false);
        background.gameObject.SetActive(false);
    }

}
