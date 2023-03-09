namespace TreeSharp.ex;

public static class TreeSharpExtensions
{
    private static void CheckParams<T, K>(List<T> collection, Func<T, K> selector, Func<T, K> parent_selector)
    {
        if (collection == null)
        {
            throw new ArgumentNullException(nameof(collection));
        }

        if (selector == null)
        {
            throw new ArgumentNullException(nameof(selector));
        }

        if (parent_selector == null)
        {
            throw new ArgumentNullException(nameof(parent_selector));
        }
    }

    private static IEnumerable<TreeNodeWithChildren<T>> BuildeTree<T, K>(List<TreeNodeWithChildren<T>> roots, List<T> collection, 
        Func<T, K> selector, Func<T, K> parent_selector, int? depth = default)
    {
        foreach (TreeNodeWithChildren<T> item in roots)
        {
            List<TreeNodeWithChildren<T>> sub_roots = collection.GetTree(selector, parent_selector, selector(item.Node), false, 1);

            yield return new TreeNodeWithChildren<T>
            {
                Node = item.Node,
                Children = depth != null && depth <= 0 ? new() : BuildeTree(sub_roots, collection, selector, parent_selector, depth == null ? depth : depth - 1).ToList()
            };
        }
    }

    public static List<TreeNodeWithChildren<T>> GetTree<T, K>(this List<T> collection, Func<T, K> selector, Func<T, K> parent_selector, 
        K? start = default, bool include_start_node = false, int? depth = default)
    {
        CheckParams(collection, selector, parent_selector);

        List<TreeNodeWithChildren<T>> roots = new();

        bool roots_found = false;

        if (include_start_node)
        {
            List<T> root_nodes = collection.Where(x => EqualityComparer<K>.Default.Equals(selector(x), start)).ToList();

            if (root_nodes.Any())
            {
                roots_found = true;

                root_nodes.ForEach(node => roots.Add(new TreeNodeWithChildren<T> { Node = node }));
            }
        }

        if (depth != null && depth <= 0)
        {
            return roots;
        }

        if (!roots_found)
        {
            collection.Where(x => EqualityComparer<K>.Default.Equals(parent_selector(x), start)).ToList()
                      .ForEach(node => roots.Add(new TreeNodeWithChildren<T> { Node = node }));
        }

        return BuildeTree(roots, collection, selector, parent_selector, depth == null ? depth : depth - 1).ToList();
    }

    public static List<T> GetParents<T, K>(this List<T> collection, Func<T, K> selector, Func<T, K> parent_selector,
        K? start = default, bool include_start_node = false, int? depth = default)
    {
        CheckParams(collection, selector, parent_selector);

        List<T> inner = new();

        T? start_node = collection.FirstOrDefault(x => EqualityComparer<K>.Default.Equals(selector(x), start));

        if (start_node == null)
        {
            return inner;
        }

        if (include_start_node)
        {
            inner.Add(start_node);

            depth--;
        }

        if (depth != null && depth <= 0)
        {
            return inner;
        }

        foreach (T node in collection.Where(x => EqualityComparer<K>.Default.Equals(selector(x), parent_selector(start_node))))
        {
            inner.Add(node); inner = inner.Union(collection.GetParents(selector, parent_selector, selector(node), false, depth == null ? depth : depth - 1)).ToList();
        }

        return inner;
    }

    public static List<T> GetChildren<T, K>(this List<T> collection, Func<T, K> selector, Func<T, K> parent_selector, 
        K? start = default, bool include_start_node = false, int? depth = default)
    {
        CheckParams(collection, selector, parent_selector);

        List<T> inner = new();

        if (include_start_node)
        {
            T? start_node = collection.FirstOrDefault(x => EqualityComparer<K>.Default.Equals(selector(x), start));

            if (start_node != null)
            {
                inner.Add(start_node);
            }

            depth--;
        }

        if (depth != null && depth <= 0)
        {
            return inner;
        }

        foreach (T node in collection.Where(x => EqualityComparer<K>.Default.Equals(parent_selector(x), start)))
        {
            inner.Add(node); inner = inner.Union(collection.GetChildren(selector, parent_selector, selector(node), false, depth == null ? depth : depth - 1)).ToList();
        }

        return inner;
    }

    public static List<T> GetLeaves<T, K>(this List<T> collection, Func<T, K> selector, Func<T, K> parent_selector,
        K? start = default)
    {
        return collection.GetLeavesPrivate(selector, parent_selector, start)
                         .Where(x => !EqualityComparer<K>.Default.Equals(selector(x), start))
                         .ToList();
    }

    private static List<T> GetLeavesPrivate<T, K>(this List<T> collection, Func<T, K> selector, Func<T, K> parent_selector,
        K? start = default)
    {
        CheckParams(collection, selector, parent_selector);

        List<T> inner = new();

        if (collection.GetChildren(selector, parent_selector, start, false, 1).Any())
        {
            foreach (T node in collection.GetChildren(selector, parent_selector, start, false, 1))
            {
                inner = inner.Union(collection.GetLeavesPrivate(selector, parent_selector, selector(node))).ToList();
            }
        }
        else
        {
            T? node = collection.FirstOrDefault(x => EqualityComparer<K>.Default.Equals(selector(x), start));

            if (node is not null)
            {
                inner.Add(node);
            }
        }

        return inner;
    }
}