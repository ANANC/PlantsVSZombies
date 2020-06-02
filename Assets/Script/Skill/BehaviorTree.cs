using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class NodeBehavior
{
    public bool Complete { get; set; }

    public BehaviorTree.EnvironmentInfo GlobalEnvironmentInfo;
    public LogicBehavior LogicBehavior;

    public virtual void Enter() { }
    public virtual void Exist() { }
    public abstract void Execute();
}

public abstract class LogicBehavior
{
    public class LogicBehaviorInfo { }

    public NodeBehavior Node;
    public LogicBehaviorInfo Enviorment;

    public virtual void Enter() { }
    public virtual void Exist() { }
    public abstract void Execute();
}

public interface IBehaviorEnvironmentInfo { }

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
        public NodeBehavior Behavior;
        public bool Enter;
    }

    public class EnvironmentInfo
    {
        private Dictionary<string, IBehaviorEnvironmentInfo> BehaviorEnvironmentInfoDict;

        public void Add<T>(T info) where T : IBehaviorEnvironmentInfo
        {
            if (BehaviorEnvironmentInfoDict == null)
            {
                BehaviorEnvironmentInfoDict = new Dictionary<string, IBehaviorEnvironmentInfo>();
            }
            string typeName = info.GetType().Name;
            if (BehaviorEnvironmentInfoDict.ContainsKey(typeName))
            {
                BehaviorEnvironmentInfoDict.Remove(typeName);
            }
            BehaviorEnvironmentInfoDict.Add(typeName, info);
        }

        public T Get<T>() where T : IBehaviorEnvironmentInfo
        {
            IBehaviorEnvironmentInfo info;
            string typeName = typeof(T).Name;
            if (BehaviorEnvironmentInfoDict.TryGetValue(typeName, out info))
            {
                return (T)info;

            }
            return default(T);
        }
    }

    private List<Node> BehaviorList = new List<Node>();
    private EnvironmentInfo Environment;

    private int AutoId = 0;


    public void AddBehavior<T>(T behavior, NodeType nodeType) where T: NodeBehavior
    {
        Node node = new Node();
        node.Id = AutoId;
        node.Type = nodeType;
        node.Behavior = behavior;
        node.Enter = false;

        AutoId += 1;

        BehaviorList.Add(node);
    }

    public void AddEnvironmentInfo<T>(T info)where T: IBehaviorEnvironmentInfo
    {
        Environment.Add<T>(info);
    }

    public void Execute()
    {
        for(int index = 0;index< BehaviorList.Count;index++)
        {
            BehaviorList[index].Behavior.GlobalEnvironmentInfo = Environment;
        }
    }

    public bool Complete()
    {
        while (BehaviorList.Count > 0)
        {
            Node node = BehaviorList[0];

            if (!node.Enter)
            {
                node.Enter = true;
                node.Behavior.Enter();
            }

            node.Behavior.Execute();
            if (node.Behavior.LogicBehavior != null)
            {
                node.Behavior.LogicBehavior.Execute();
            }

            bool finish = node.Behavior.Complete;

            if (finish)
            {
                node.Behavior.Exist();
                BehaviorList.RemoveAt(0);
            }

            if(node.Type == NodeType.Serial)
            {
                break;
            }
        }

        return BehaviorList.Count == 0;
    }
}
