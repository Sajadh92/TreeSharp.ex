# TreeSharp.ex

TreeSharp is a C# class library that simplifies working with tree structures in your code. With TreeSharp, you can easily build a list of items as a tree, and retrieve all children, parents, and leaves of a specific item within that tree.

## Features

- `GetTree` : builds a tree from a list of items
- `GetChildren` : retrieves all children of a specific item in the tree
- `GetParents` : retrieves all parents of a specific item in the tree
- `GetLeaves` : retrieves all leaves (nodes with no children) of the tree

## Installation

1. You can install TreeSharp via NuGet package manager. Simply search for "TreeSharp.ex" and click "Install".
2. You can install TreeSharp via command in the Package Manager Console: `Install-Package TreeSharp.ex`

## Usage

1. Import the TreeSharp namespace in your C# project:

    `using TreeSharp;`

2. To build a tree from a list of items, use the `GetTree` extension method:

    `var tree = items.GetTree(selector: item => item.Id, parent_selector: item => item.ParentId);`

3. To retrieve all children of a specific item, use the `GetChildren` extension method:

    `var children = item.GetChildren(selector: node => node.Id, parent_selector: node => node.ParentId);`

4. To retrieve all parents of a specific item, use the `GetParents` extension method:

    `var parents = item.GetParents(selector: node => node.Id, parent_selector: node => node.ParentId);`

5. To retrieve all leaves of the tree, use the `GetLeaves` extension method:

    `var leaves = tree.GetLeaves(selector: node => node.Id, parent_selector: node => node.ParentId);`

Required Parameters:

- `selector` : represents the unique identifier of the node.
- `parent_selector` : represents the identifier of the parent node.

Optional Parameters:

- `start`: Represents the root node (could be null) or a node to start from.
- `include_start_node`: Flag to determine whether to include the start value with the result or not.
- `depth`: The depth level of building the tree or retrieving parents/children.

## Contributing

Contributions are always welcome! If you find a bug or have a feature request, please open an issue. Pull requests are also accepted.

## License

TreeSharp is Free Forever
