using System;
using System.Collections.Generic;
using UnityEngine;

public enum TraitEffectType
{
    ProductionMultiplier,
    CostReduction,
    AutoProductionUnlock,
    ClickPowerMultiplier,
    ResearchSpeedMultiplier
}

[Serializable]
public class TraitEffect
{
    public TraitEffectType effectType;
    public float value;
}

[Serializable]
public class TraitNodeData
{
    [Header("Basic")]
    public string id;
    public string displayName;
    [TextArea] public string description;
    public Sprite icon;

    [Header("Board")]
    public Vector2 boardPosition;
    public int cost = 1;

    [Header("Connection")]
    public List<string> prerequisiteIds = new();

    [Header("Effect")]
    public List<TraitEffect> effects = new();
}