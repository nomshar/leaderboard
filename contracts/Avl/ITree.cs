namespace contracts.Avl;

/// <summary>
/// Interface to define AVL tree behaviour
/// T type identifies a node value
/// </summary>
public interface ITree<T>: IDisposable where T:class
{
    /// <summary>
    /// Adds new node or updates existing one
    /// If the node exists in a tree, the tree would be re-balanced
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    void Add(uint key, T value);

    /// <summary>
    /// Removes node from the tree
    /// </summary>
    /// <param name="key"></param>
    void Remove(uint key);

    /// <summary>
    /// Removes a node value from a collection of equal-scored nodes
    /// </summary>
    /// <param name="oldKey"></param>
    /// <param name="nodeId"></param>
    void RemoveNodesFromScore(uint oldKey, T nodeId);

    /// <summary>
    /// Search for a node
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    INode<T> Search(uint key);

    /// <summary>
    /// Returns a flat list of all nodes values
    /// </summary>
    /// <param name="k"></param>
    /// <returns></returns>
    IEnumerable<T> Top(int k);

    /// <summary>
    /// Cleans up allocated resources
    /// </summary>
    void Clean();
}
