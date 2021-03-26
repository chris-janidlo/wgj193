using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using TMPro;
using crass;

public class GameLifeCycleManager : MonoBehaviour
{
    [Tooltip("The number of cards that are drawn when entering the build phase. Does not affect the maximum number of cards you can have in your hand (which is technically limitless)")]
    public int HandSize;

    public CardValueList Deck, DrawPile, DiscardPile;
    public PhaseVariable CurrentPhase;
    public float TimeToStayInEnemyPhase;

    public FloorList FloorList;
    public IntVariable CurrentPlayerFloorNumber;

    public PlayerAbilityCharges PlayerAbilityCharges;

    public LayoutGroup HandUILayoutGroup;

    public PlayerLifeCycleManager PlayerPrefab;
    public UICard UICardPrefab;

    PlayerLifeCycleManager currentlyInstantiatedPlayer;
    List<Card> hand;

    void Start ()
    {
        PlayerAbilityCharges.Initialize();

        DrawPile.Clear();
        DiscardPile.Clear();

        DiscardPile.List.AddRange(Deck);
        startBuildPhase();
    }

    public void PlayerReachedNewFloor (int newFloor)
    {
        if (CurrentPlayerFloorNumber.Value == newFloor)
        {
            throw new InvalidOperationException($"phase and deck manager received PlayerReachedNewFloor notification even though the CurrentPlayerFloor atom has the same value as the newFloor ({newFloor}). this should never happen");
        }

        if (CurrentPlayerFloorNumber.Value > newFloor)
        {
            // player fell down a floor

            var cardsToRetrieve = FloorList.CurrentPlayerFloor.BuildZones
                .Where(bz => bz.HasCard)
                .Select(bz => bz.CurrentCard);

            DiscardPile.List.AddRange(cardsToRetrieve);
        }

        CurrentPlayerFloorNumber.Value = newFloor;

        currentlyInstantiatedPlayer.Die();
    }

    public void OnPlayerDied ()
    {
        DiscardPile.List.AddRange(hand);
        startBuildPhase();
    }

    public void OnEndBuildPhaseButtonClicked ()
    {
        StartCoroutine(enemyToPlatformingPhaseRoutine());
    }

    void startBuildPhase ()
    {
        CurrentPhase.Value = Phase.Build;

        drawHand();

        foreach (var card in hand)
        {
            var uiCard = Instantiate(UICardPrefab);
            uiCard.Initialize(card, HandUILayoutGroup.transform, null);
        }

        PlayerAbilityCharges.Clear();
    }

    IEnumerator enemyToPlatformingPhaseRoutine ()
    {
        CurrentPhase.Value = Phase.Enemy;

        yield return new WaitForSeconds(TimeToStayInEnemyPhase);

        CurrentPhase.Value = Phase.Platforming;
        currentlyInstantiatedPlayer = Instantiate(PlayerPrefab, FloorList.CurrentPlayerFloor.SpawnPoint);
    }

    void drawHand ()
    {
        hand = new List<Card>();

        for (int i = 0; i < HandSize; i++)
        {
            if (DrawPile.Count == 0)
            {
                DrawPile.List.AddRange(DiscardPile);
                DrawPile.List.ShuffleInPlace();
                DiscardPile.Clear();

                if (DrawPile.Count != Deck.Count)
                {
                    // we should be able to continue playing if this happens, but I absolutely want to know about it
                    Debug.LogError($"you lost {Deck.Count - DrawPile.Count} cards somewhere");
                }
            }

            var drawIndex = DrawPile.Count - 1;

            hand.Add(DrawPile[drawIndex]);
            DrawPile.RemoveAt(drawIndex);
        }
    }
}
