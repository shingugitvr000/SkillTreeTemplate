using System.Collections.Generic;
using UnityEngine;

public class TraitTreeBoard : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TraitTreeRuntime treeRuntime;
    [SerializeField] private RectTransform nodeParent;
    [SerializeField] private RectTransform lineParent;

    [Header("Prefabs")]
    [SerializeField] private TraitNodeView nodePrefab;
    [SerializeField] private TraitLineView linePrefab;

    private readonly Dictionary<string, TraitNodeView> spawnedNodes = new();

    private void Start()
    {
        Build();
    }

    public void Build()
    {
        Clear();

        TraitTreeData data = treeRuntime.TreeData;

        foreach (TraitNodeData nodeData in data.nodes)
        {
            TraitNodeView nodeView = Instantiate(nodePrefab, nodeParent);
            nodeView.name = $"TraitNode_{nodeData.id}";

            RectTransform rect = nodeView.GetComponent<RectTransform>();
            rect.anchoredPosition = nodeData.boardPosition;

            nodeView.Init(nodeData, treeRuntime);

            spawnedNodes.Add(nodeData.id, nodeView);
        }

        foreach (TraitNodeData nodeData in data.nodes)
        {
            foreach (string prerequisiteId in nodeData.prerequisiteIds)
            {
                if (!spawnedNodes.ContainsKey(prerequisiteId))
                    continue;

                if (!spawnedNodes.ContainsKey(nodeData.id))
                    continue;

                TraitLineView line = Instantiate(linePrefab, lineParent);
                line.name = $"Line_{prerequisiteId}_to_{nodeData.id}";
                line.Connect(
                    spawnedNodes[prerequisiteId].RectTransform,
                    spawnedNodes[nodeData.id].RectTransform
                );
            }
        }
    }

    private void Clear()
    {
        spawnedNodes.Clear();

        foreach (Transform child in nodeParent)
        {
            Destroy(child.gameObject);
        }

        foreach (Transform child in lineParent)
        {
            Destroy(child.gameObject);
        }
    }
}