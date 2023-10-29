namespace leaderboard.Avl;

using System.Collections.Generic;
using contracts;
using contracts.Avl;

public class AvlTree : ITree<IMatch>
{
    private INode<IMatch> root;

    public void Add(uint key, IMatch value)
    {
        root = AddNode(root, key, value);
    }

    public void Remove(uint key)
    {
        root = RemoveNode(root, key);
    }

    public void RemoveNodesFromScore(uint oldKey, IMatch match) 
    {
        var node = Search(oldKey);
        if (node.NodesCount == 1)
        {
            root = RemoveNode(root, oldKey);
        }
        else
        {
            RemoveItemFromScoreNode(node, oldKey, match);
        }
    }

    public INode<IMatch> Search(uint key)
    {
        return SearchNode(root, key);
    }

    public IEnumerable<IMatch> Top(int k)
    {
         int curr = 0;
        var output = new List<IMatch>();
        _ = TopNodes(root, ref curr, k, output);
        return output;
    }

    private INode<IMatch> AddNode(INode<IMatch> node, uint key, IMatch value)
    {
        if (node == null)
        {
            var matches = new Dictionary<IMatch, bool>();
            matches[value] = true;
            return new TreeNode<IMatch>(key, matches, 1, 1, null, null);
        }

        if (key < node.Score)
        {
            node.Left = AddNode(node.Left, key, value);
        }
        else if (key > node.Score)
        {
            node.Right = AddNode(node.Right, key, value);
        }
        else
        {
            node.NodesCount++;
            node.NodeIds[value] = true;
        }

        return RebalanceTree(node);
    }

    private INode<IMatch> RemoveNode(INode<IMatch> node, uint key)
    {
        if (node == null)
        {
            return null;
        }

        if (key < node.Score)
        {
            node.Left = RemoveNode(node.Left, key);
        }
        else if (key > node.Score)
        {
            node.Right = RemoveNode(node.Right, key);
        }
        else
        {
            if (node.Left != null && node.Right != null)
            {
                var rightMinNode = FindSmallest(node.Right);
                node.Score = rightMinNode.Score;
                node.NodeIds = rightMinNode.NodeIds;
                node.NodesCount = rightMinNode.NodesCount;
                node.Right = RemoveNode(node.Right, rightMinNode.Score);
            }
            else if (node.Left != null)
            {
                node = node.Left;
            }
            else if (node.Right != null)
            {
                node = node.Right;
            }
            else
            {
                node = null;
                return node;
            }
        }

        return RebalanceTree(node);
    }

    private void RemoveItemFromScoreNode(INode<IMatch> node, uint key, IMatch id)
    {
        if (key < node.Score)
        {
            RemoveItemFromScoreNode(node.Left, key, id);
        }
        else if (key > node.Score)
        {
            RemoveItemFromScoreNode(node.Right, key, id);
        }
        else
        {
            node.NodesCount--;
            node.NodeIds.Remove(id);
        }
    }

    private INode<IMatch> SearchNode(INode<IMatch> node, uint key)
    {
        if (node == null)
        {
            return null;
        }
        if (key < node.Score)
        {
            return SearchNode(node.Left, key);
        }
        else if (key > node.Score)
        {
            return SearchNode(node.Right, key);
        }
        else
        {
            return node;
        }
    }

    private (IList<IMatch>, bool) TopNodes(INode<IMatch> node, ref int curr, int k, List<IMatch> output)
    {
        if (node.Right != null)
        {
            var (o, br) = TopNodes(node.Right, ref curr, k, output);
            if (br)
            {
                return (output, true);
            }
        }

        int i = 0;
        var matches = new List<IMatch>();
        foreach (var id in node.NodeIds.Keys)
        {
            if (curr < k && i < node.NodesCount)
            {
                matches.Insert(0, id);
                curr++;
                i++;
            }
            else
            {
                break;
            }
        }
        output.AddRange(matches);

        if (curr == k)
        {
            return (output, true);
        }

        if (node.Left != null)
        {
            var (o, br) = TopNodes(node.Left, ref curr, k, output);
            if (br)
            {
                return (output, true);
            }
        }

        return (output, false);
    }

    private INode<IMatch> RebalanceTree(INode<IMatch> node)
    {
        if (node == null)
        {
            return node;
        }

        RecalculateHeight(node);

        var balanceFactor = GetHeight(node.Left) - GetHeight(node.Right);

        if (balanceFactor == -2)
        {
            if (GetHeight(node.Right.Left) > GetHeight(node.Right.Right))
            {
                node.Right = RotateRight(node.Right);
            }
            return RotateLeft(node);
        }
        else if (balanceFactor == 2)
        {
            if (GetHeight(node.Left.Right) > GetHeight(node.Left.Left))
            {
                node.Left = RotateLeft(node.Left);
            }
            return RotateRight(node);
        }

        return node;
    }

    private INode<IMatch> RotateLeft(INode<IMatch> node)
    {
        var newRoot = node.Right;
        node.Right = newRoot.Left;
        newRoot.Left = node;

        RecalculateHeight(node);
        RecalculateHeight(newRoot);

        return newRoot;
    }

    private INode<IMatch> RotateRight(INode<IMatch> node)
    {
        var newRoot = node.Left;
        node.Left = newRoot.Right;
        newRoot.Right = node;

        RecalculateHeight(node);
        RecalculateHeight(newRoot);

        return newRoot;
    }

    private INode<IMatch> FindSmallest(INode<IMatch> node)
    {
        if (node.Left != null)
        {
            return FindSmallest(node.Left);
        }
        else
        {
            return node;
        }
    }

    private int GetHeight(INode<IMatch> node)
    {
        if (node == null)
        {
            return 0;
        }
        return node.Height;
    }

    private void RecalculateHeight(INode<IMatch> node)
    {
        node.Height = 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));
    }

    public void Clean() {
        root?.Dispose();
    }
    
    public void Dispose()
    {
        root?.Dispose();
    }
}
