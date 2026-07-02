using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TraitTreeRuntime : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private TraitTreeData treeData;

    [Header("Point")]
    [SerializeField] private int currentTraitPoint = 10;

    private readonly HashSet<string> purchasedIds = new();

    public TraitTreeData TreeData => treeData;

    public bool IsPurchased(string id)
    {
        return purchasedIds.Contains(id);
    }

    public bool CanPurchase(string id)
    {
        TraitNodeData node = GetNode(id);

        if (node == null)
            return false;

        if (IsPurchased(id))
            return false;

        if (currentTraitPoint < node.cost)
            return false;

        foreach (string prerequisiteId in node.prerequisiteIds)
        {
            if (!IsPurchased(prerequisiteId))
                return false;
        }

        return true;
    }

    public bool TryPurchase(string id)
    {
        TraitNodeData node = GetNode(id);

        if (node == null)
            return false;

        if (!CanPurchase(id))
            return false;

        currentTraitPoint -= node.cost;
        purchasedIds.Add(id);

        ApplyEffects(node);

        Debug.Log($"Purchased Trait: {node.displayName}");

        return true;
    }

    private TraitNodeData GetNode(string id)
    {
        return treeData.nodes.FirstOrDefault(x => x.id == id);
    }

    private void ApplyEffects(TraitNodeData node)
    {
        foreach (TraitEffect effect in node.effects)
        {
            Debug.Log($"Apply Effect: {effect.effectType} / {effect.value}");

            // 나중에 여기를 실제 증분 게임 스탯 시스템에 연결하면 됨
            // 예: GameStatManager.Instance.AddMultiplier(effect.effectType, effect.value);
        }
    }
}