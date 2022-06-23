using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Voody.UniLeo.Lite;

public sealed class BTreeDataProvider : MonoProvider<BTreeData>
{
    [SerializeField] private BTree _BTree;

    private void Awake()
    {
        value.BTree = _BTree;
    }
}
