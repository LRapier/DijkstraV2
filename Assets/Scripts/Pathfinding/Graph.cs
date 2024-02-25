using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph
{
    List<Connection> m_connections = new();
    public Connection[] GetConnections(Node node)
    {
        List<Connection> connections = new List<Connection>();
        foreach (Connection c in m_connections)
        {
            if (c.GetFromNode() == node)
            {
                connections.Add(c);
            }
        }
        return connections.ToArray();
    }

    public void Build(MeshFilter playerShape)
    {
        m_connections = new List<Connection>();

        Node[] nodes = GameObject.FindObjectsByType<Node>(FindObjectsSortMode.None);
        foreach (Node fromNode in nodes)
        {
            foreach (Node toNode in fromNode.ConnectsTo)
            {
                float cost = (toNode.transform.position - fromNode.transform.position).magnitude;
                if (playerShape.mesh.name == toNode.GetComponent<MeshFilter>().mesh.name)
                    cost -= 100f;
                Connection c = new Connection(cost, fromNode, toNode);
                m_connections.Add(c);
            }
        }
    }
}
