using DD.Lab.Wpf.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using static DomainGeneratorUI.Models.Conditions.GroupNodeExpression;

namespace DomainGeneratorUI.Viewmodels.Conditions
{
    public class GroupNodeExpressionViewModel: BaseViewModel
    {
        public FilterOperator Operator { get { return GetValue<FilterOperator>(); } set { SetValue(value); } }
        public NodeType Type { get { return GetValue<NodeType>(); } set { SetValue(value); } }

        public List<GroupNodeExpressionViewModel> ChildNodes { get { return GetValue<List<GroupNodeExpressionViewModel>>(); } set { SetValue(value); } }
        public NodeConditionExpressionViewModel Condition { get { return GetValue<NodeConditionExpressionViewModel>(); } set { SetValue(value); } }
        
    }
}
