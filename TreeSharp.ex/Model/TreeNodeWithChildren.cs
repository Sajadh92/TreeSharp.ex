namespace TreeSharp;

public class TreeNodeWithChildren<T>
{
    public T Node { get; set; }
    public List<TreeNodeWithChildren<T>> Children { get; set; } = new();
}
