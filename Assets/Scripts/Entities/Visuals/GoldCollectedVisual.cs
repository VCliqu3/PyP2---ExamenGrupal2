using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCollectedVisual : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private CollectorStructureCollection collectorStructureCollection;
    [SerializeField] private Transform feedbackPrefab;

    [Header("Settings")]
    [SerializeField] Sprite goldSprite;
    [SerializeField] private Vector3 offset;

    private void OnEnable()
    {
        collectorStructureCollection.OnGoldCollected += CollectorStructureCollection_OnGoldCollected;
    }

    private void OnDisable()
    {
        collectorStructureCollection.OnGoldCollected -= CollectorStructureCollection_OnGoldCollected;
    }

    private void ShowFeedback(int gold)
    {
        Transform feedbackTransform = Instantiate(feedbackPrefab, transform.position + offset, transform.rotation);

        FeedbackUI feedbackUI = feedbackTransform.GetComponentInChildren<FeedbackUI>();

        if (!feedbackUI)
        {
            Debug.LogWarning("There's not a FeedbackUI attached to instantiated prefab");
            return;
        }

        feedbackUI.SetFeedbackImage(goldSprite);
        feedbackUI.SetFeedbackText($"+{gold}");
    }

    private void CollectorStructureCollection_OnGoldCollected(object sender, CollectorStructureCollection.OnGoldCollectedEventArgs e)
    {
        ShowFeedback(e.gold);
    }
}
