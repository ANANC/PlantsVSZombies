using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorTree
{
    public enum NodeType
    {
        Serial,
        Parallel
    }

    public class Node
    {
        public int Id;
        public NodeType Type;
        public IBehavior Behavior;
        public Action LogicAction;

        public bool Enter;
    }

    public class NodeType<T>: Node where T : IBehavior
    {
        public T BehaviorInstance;
    }


    private List<Node> Behaviors = new List<Node>();
    private Dictionary<int , Node> BehaviorTypeDict = new Dictionary<int, Node>();

    private int AutoId = 0;


    public void AddBehavior<T>(IBehavior behavior, NodeType nodeType,Action logicAction) where T: IBehavior
    {
        NodeType<T> node = new NodeType<T>();
        node.Id = AutoId;
        node.Type = nodeType;
        node.Behavior = behavior;
        node.BehaviorInstance = (T)behavior;
        node.LogicAction = logicAction;
        node.Enter = false;

        AutoId += 1;

        Behaviors.Add(node);
        BehaviorTypeDict.Add(node.Id, node);
    }

    public bool Execute()
    {
        while (Behaviors.Count > 0)
        {
            Node node = Behaviors[0];

            if (!node.Enter)
            {
                node.Enter = true;
                node.Behavior.Enter();
            }

            node.LogicAction?.Invoke();
            bool finish = node.Behavior.Execute();

            if(finish)
            {
                node.Behavior.Exist();
                Behaviors.RemoveAt(0);
            }

            if(node.Type == NodeType.Serial)
            {
                break;
            }
        }

        return Behaviors.Count == 0;
    }
}
