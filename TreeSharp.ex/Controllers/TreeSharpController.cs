using Microsoft.AspNetCore.Mvc;

namespace TreeSharp.Controllers;

[Route("api/[action]")]
[ApiController]
public class TreeSharpController : ControllerBase
{ 
    private readonly List<Node> nodes = new();

    private static Node NewNode(int Id, int? ParentId, string Value) => new()
    {
        Id = Id,
        ParentId = ParentId,
        Value = Value
    };

    public TreeSharpController()
    {
        {
            nodes.Add(NewNode(1, null, "1"));
            {
                nodes.Add(NewNode(2, 1, "1.2"));
                {
                    nodes.Add(NewNode(3, 2, "1.2.3"));
                    {
                        nodes.Add(NewNode(4, 3, "1.2.3.4"));
                        nodes.Add(NewNode(5, 3, "1.2.3.5"));
                    }
                    nodes.Add(NewNode(6, 2, "1.2.6"));
                    {
                        nodes.Add(NewNode(7, 6, "1.2.6.7"));
                    }
                }
                nodes.Add(NewNode(8, 1, "1.8"));
                {
                    nodes.Add(NewNode(9, 8, "1.8.9"));
                    {
                        nodes.Add(NewNode(10, 9, "1.8.9.10"));
                        {
                            nodes.Add(NewNode(11, 10, "1.8.9.10.11"));
                            {
                                nodes.Add(NewNode(12, 11, "1.8.9.10.11.12"));
                                nodes.Add(NewNode(13, 11, "1.8.9.10.11.13"));
                            }
                            nodes.Add(NewNode(14, 10, "1.8.9.10.14"));
                        }
                    }
                    nodes.Add(NewNode(15, 8, "1.8.15"));
                }
                nodes.Add(NewNode(16, 1, "1.16"));
                {
                    nodes.Add(NewNode(17, 16, "1.16.17"));
                    {
                        nodes.Add(NewNode(18, 17, "1.16.17.18"));
                        nodes.Add(NewNode(19, 17, "1.16.17.19"));
                    }
                }
                nodes.Add(NewNode(20, 1, "1.20"));
                {
                    nodes.Add(NewNode(21, 20, "1.20.21"));
                    {
                        nodes.Add(NewNode(22, 21, "1.20.21.22"));
                        {
                            nodes.Add(NewNode(23, 22, "1.20.21.22.23"));
                            {
                                nodes.Add(NewNode(24, 23, "1.20.21.22.23.24"));
                                {
                                    nodes.Add(NewNode(25, 24, "1.20.21.22.23.24.25"));
                                    {
                                        nodes.Add(NewNode(26, 25, "1.20.21.22.23.24.25.26"));
                                        {
                                            nodes.Add(NewNode(27, 26, "1.20.21.22.23.24.25.26.27"));
                                        }
                                    }
                                }
                                nodes.Add(NewNode(28, 23, "1.20.21.22.23.28"));
                            }
                        }
                    }
                    nodes.Add(NewNode(29, 20, "1.20.29"));
                    {
                        nodes.Add(NewNode(30, 29, "1.20.29.30"));
                        {
                            nodes.Add(NewNode(31, 30, "1.20.29.30.31"));
                            {
                                nodes.Add(NewNode(32, 31, "1.20.29.30.31.32"));
                                {
                                    nodes.Add(NewNode(33, 32, "1.20.29.30.31.32.33"));
                                }
                            }
                        }
                    }
                    nodes.Add(NewNode(34, 20, "1.20.34"));
                    {
                        nodes.Add(NewNode(35, 34, "1.20.34.35"));
                    }
                }
                nodes.Add(NewNode(36, 1, "1.36"));
            }
            nodes.Add(NewNode(37, null, "37"));
            {
                nodes.Add(NewNode(38, 37, "37.38"));
                {
                    nodes.Add(NewNode(39, 38, "37.38.39"));
                    nodes.Add(NewNode(40, 38, "37.38.40"));
                    {
                        nodes.Add(NewNode(41, 40, "37.38.40.41"));
                        nodes.Add(NewNode(42, 40, "37.38.40.42"));
                        {
                            nodes.Add(NewNode(43, 42, "37.38.40.42.43"));
                            {
                                nodes.Add(NewNode(44, 43, "37.38.40.42.43.44"));
                                {
                                    nodes.Add(NewNode(45, 44, "37.38.40.42.43.44.45"));
                                    {
                                        nodes.Add(NewNode(46, 45, "37.38.40.42.43.44.45.46"));
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    [HttpGet]
    public IActionResult GetList()
    {
        return Ok(nodes);
    }

    [HttpGet]
    public IActionResult GetTree(int? start, int? depth, bool include_start_node = false)
    {
        return Ok(nodes.GetTree(x => x.Id, x => x.ParentId, start, include_start_node, depth));
    }

    [HttpGet]
    public IActionResult GetParents(int? start, int? depth, bool include_start_node = false)
    {
        return Ok(nodes.GetParents(x => x.Id, x => x.ParentId, start, include_start_node, depth));
    }

    [HttpGet]
    public IActionResult GetChildren(int? start, int? depth, bool include_start_node = false)
    {
        return Ok(nodes.GetChildren(x => x.Id, x => x.ParentId, start, include_start_node, depth));
    }

    [HttpGet]
    public IActionResult GetLeaves(int? start)
    {
        return Ok(nodes.GetLeaves(x => x.Id, x => x.ParentId, start));
    }
}