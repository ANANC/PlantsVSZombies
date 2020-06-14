using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class NodeBehavior
{
    public bool Complete { get; set; }
    public string JumpNodeName { get; set; }

    public BehaviorTree BehaviorTree;
    public List<LogicBehavior> LogicBehaviors = new List<LogicBehavior>();

    public void AddBehavior(LogicBehavior logic)
    {
        logic.Node = this;
        LogicBehaviors.Add(logic);
    }

    protected void EnterLogics()
    {
        for(int index = 0;index< LogicBehaviors.Count;index++)
        {
            LogicBehaviors[index].Enter();
        }
    }
    protected void ExecuteLogics()
    {
        for (int index = 0; index < LogicBehaviors.Count; index++)
        {
            LogicBehaviors[index].Execute();
        }
    }


    public virtual void Enter() { }
    public abstract void Execute();
}

public abstract class LogicBehavior
{
    public class LogicBehaviorInfo { }

    public NodeBehavior Node;
    public LogicBehaviorInfo Enviorment;

    public virtual void Enter() { }
    public abstract void Execute();
}

public interface IBehaviorEnvironmentInfo { }

public class BehaviorTree
{
    public enum NodeType
    {
        Serial,
        ParallelAnd,
        ParallelOr,
    }

    public class Node
    {
        public string Name;
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
            if(BehaviorEnvironmentInfoDict == null)
            {
                return default(T);
            }

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
    public EnvironmentInfo Environment = new EnvironmentInfo();

    private NodeType LastNodeType;
    private bool NodeFinish;
    private int AutoId = 0;


    public void AddBehavior(string name, NodeBehavior behavior, NodeType nodeType) 
    {
        Node node = new Node();
        node.Name = name;
        node.Id = AutoId;
        node.Type = nodeType;
        node.Behavior = behavior;
        node.Enter = false;

        AutoId += 1;

        BehaviorList.Add(node);
    }

    public void ForceFinish()
    {
        BehaviorList.Clear();
    }

    public void Execute()
    {

    }

    public bool Complete()
    {
        int nodeIndex = 0;
        while (BehaviorList.Count > 0)
        {
            Node node = BehaviorList[nodeIndex];

            if (!node.Enter)
            {
                node.Enter = true;
                node.Behavior.BehaviorTree = this;
                node.Behavior.Enter();
            }

            node.Behavior.Execute();

            bool finish = node.Behavior.Complete;
            if (nodeIndex == 0)
            {
                NodeFinish = finish;
            }
            else if (LastNodeType == NodeType.ParallelAnd)
            {
                NodeFinish = finish && NodeFinish;
            }
            else if (LastNodeType == NodeType.ParallelOr)
            {
                NodeFinish = finish || NodeFinish;
            }

            LastNodeType = node.Type;

            if (finish)
            {
                BehaviorList.RemoveAt(0);

                if (!string.IsNullOrEmpty(node.Behavior.JumpNodeName))
                {
                    int findIndex = -1;
                    for (int index = 0; index < BehaviorList.Count; index++)
                    {
                        if (BehaviorList[index].Name == node.Behavior.JumpNodeName)
                        {
                            findIndex = index;
                            break;
                        }
                    }
                    if (findIndex != -1)
                    {
                        BehaviorList.RemoveRange(0, findIndex);
                        break;
                    }
                }
            }
            else
            {
                nodeIndex += 1;
            }

            if (node.Type == NodeType.Serial)
            {
                break;
            }
            else if (NodeFinish)
            {
                BehaviorList.RemoveRange(0, nodeIndex);
                nodeIndex = 0;
            }
        }

        return BehaviorList.Count == 0;
    }
}
