using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour, IInteract
{
    public event EventHandler OnStartSetTrap;
    public event EventHandler OnStopSetTrap;
    public event EventHandler<float> OnProgressSet;


    [Header("Visual")]
    [SerializeField] private Transform Selected_Trap;
    [SerializeField] private Transform Set_Trap;

    [Header("Inventory")]
    [SerializeField] private Inventory inventory;
    [SerializeField] private ItemsSO itemSO;

    [Header("Timer")]
    private float timerSetTrap = 0;
    [SerializeField] private float timerSetTrapMax;

    private Player currentPlayer;
    private TrapState currentTrapState;
    private Collider2D col;

    public void Interact(Player player)
    {
        if (currentTrapState != TrapState.Ready) return;
        if (player.IsWalking) return;

        Selected_Trap.gameObject.SetActive(false);

        currentPlayer = player;
        timerSetTrap = 0f;
        currentTrapState = TrapState.UpdateSet;

        OnStartSetTrap?.Invoke(this, EventArgs.Empty);
        player.PlayerSetTrap();
    }

    private void Awake()
    {
        currentTrapState = TrapState.NotReady;
        col = GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        inventory.OnGetItem += ReadySet;
    }

    private void OnDisable()
    {
        inventory.OnGetItem -= ReadySet;
    }

    public void ReadySet(object sender, Inventory.EventArgsItem e)
    {
        if (itemSO == e.Item)
        {
            currentTrapState = TrapState.Ready;
        }
    }

    private void Update()
    {
        switch (currentTrapState)
        {
            case TrapState.UpdateSet:
                SetTrap();
                break;
        }
    }

    private void SetTrap()
    {
        if (currentPlayer == null || currentPlayer.IsWalking)
        {
            ResetSet();
            return;
        }

        timerSetTrap += Time.deltaTime;

        float progress = timerSetTrap / timerSetTrapMax;
        OnProgressSet?.Invoke(this, Mathf.Clamp01(progress));

        if (timerSetTrap >= timerSetTrapMax)
        {
            Selected_Trap.gameObject.SetActive(false);
            Set_Trap.gameObject.SetActive(true);
            currentPlayer.PlayerStopInteract();

            currentTrapState = TrapState.Set;
            OnStopSetTrap?.Invoke(this, EventArgs.Empty);
            inventory.DeleteItem(itemSO);
            col.enabled = false;
        }        
    }

    private void ResetSet()
    {
        currentPlayer.PlayerStopInteract();
        timerSetTrap = 0f;
        currentPlayer = null;
        currentTrapState = TrapState.Ready;
        OnStopSetTrap?.Invoke(this, EventArgs.Empty);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.TryGetComponent(out Player player)) return;

        player.SetInteractable(this);

        if (currentTrapState != TrapState.Ready) return;
        Selected_Trap.gameObject.SetActive(true);

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.gameObject.TryGetComponent(out Player player)) return;

        player.ClearInteractable(this);

        if (currentTrapState != TrapState.Ready) return;
        Selected_Trap.gameObject.SetActive(false);

    }


    public enum TrapState
    {
        NotReady,
        Ready,
        UpdateSet,
        Set
    }
}
