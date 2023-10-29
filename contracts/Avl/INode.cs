namespace contracts.Avl;

/// <summary>
/// Interface to define treenode structure
/// T type identifies a node value
/// </summary>
public interface INode<T>: IDisposable where T:class
{
    /// <summary>
    /// Key to balance the tree
    /// </summary>
    public uint Score { get; set; }

    /// <summary>
    /// Node values
    /// In case of two values contains the same Score, the collection would contain more than one record
    /// </summary>
    public Dictionary<T, bool> NodeIds { get; set; }

    /// <summary>
    /// Number of node values at NodeIds collection
    /// </summary>
    public int NodesCount { get; set; }

    /// <summary>
    /// Tree height
    /// </summary>
    public int Height { get; set; }

    /// <summary>
    /// Left node
    /// </summary>
    public INode<T> Left { get; set; }

    /// <summary>
    /// Right node
    /// </summary>
    public INode<T> Right { get; set; }
}
