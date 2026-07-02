using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Incremental Game/Trait Tree Data")]
public class TraitTreeData : ScriptableObject
{
    public string treeName;
    public List<TraitNodeData> nodes = new();
}