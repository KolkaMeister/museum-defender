using System.Collections.Generic;
using UnityEngine;

namespace Dialogs.Answers
{
    public class AnswerGroup : PredefinedDataGroup<string, AnswerView>
    {
        public AnswerGroup(Transform container) : base(container)
        {
        }

        public List<AnswerView> GetItems() => _createdItems;
    }
}