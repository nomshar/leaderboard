namespace leaderboard.Avl;

using contracts;
using contracts.Avl;

public class TreeNode<T>: INode<T> where T:class
{
    public uint Score { get; set; }

    public Dictionary<T, bool> NodeIds { get; set; }

    public int NodesCount { get; set; }
    public int Height { get; set; }
    public INode<T>? Left { get; set; }
    public INode<T>? Right { get; set; }

    public TreeNode(uint score, Dictionary<T, bool> map, int count, int height, INode<T> left, INode<T> right) 
    {
        Score = score;
        NodeIds = map;
        NodesCount = count;
        Height = height;
        Left = left;
        Right = right;
    }

    public void Dispose()
    {
        NodeIds?.Clear();

        Left?.Dispose();
        Right?.Dispose();
    }
}
