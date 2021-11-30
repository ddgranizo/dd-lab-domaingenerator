using System;
using System.Collections.Generic;
using System.Text;

namespace DomainGeneratorUI.Models.Conditions
{
    public class GroupNodeExpression
    {
        public enum FilterOperator
        {
            And = 1,
            Or = 2
        }

        public enum NodeType
        {
            Condition = 1,
            Group = 2
        }

        public FilterOperator Operator { get; set; }
        public NodeType Type { get; set; }

        public List<GroupNodeExpression> ChildNodes { get; set; }
        public ConditionNodeExpression Condition { get; set; }

        public GroupNodeExpression(NodeType type)
        {
            ChildNodes = new List<GroupNodeExpression>();
            Type = type;
            if (Type == NodeType.Condition)
            {
                Condition = new ConditionNodeExpression();
            }
        }

    }
}
