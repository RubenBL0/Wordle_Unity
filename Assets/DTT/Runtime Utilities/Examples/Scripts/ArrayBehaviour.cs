using DTT.Utils.Extensions;
using UnityEngine;

public class ArrayBehaviour : MonoBehaviour
{
    /// <summary>
    /// The array on which to check the index.
    /// </summary>
    [SerializeField]
    private int[] _array;

    /// <summary>
    /// The index to check.
    /// </summary>
    [SerializeField]
    private int _index;
    
    private void Awake()
    {
        // Check whether an array has an index or not.
        bool condition = _array.HasIndex(_index);
        
        Debug.Log($"Has index {_index}: {condition}");
    }
}
