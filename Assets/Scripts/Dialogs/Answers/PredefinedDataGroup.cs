using System.Collections.Generic;
using UnityEngine;

namespace Dialogs.Answers
{
    public class PredefinedDataGroup<TDataType, TItemWidget> : DataGroup<TDataType, TItemWidget>
        where TItemWidget : MonoBehaviour, IItemRenderer<TDataType>
    {
        public PredefinedDataGroup(Transform container) : base(container, null)
        {
            _container = container;
            UpdateGroup();
        }

        private void UpdateGroup()
        {
            var items = _container.GetComponentsInChildren<TItemWidget>();
            _createdItems = new List<TItemWidget>();
            _createdItems.AddRange(items);
        }

        public override void SetData(TDataType[] data)
        {
            for (int i = 0; i < data.Length && i < _createdItems.Count; i++)
            {
                _createdItems[i].gameObject.SetActive(true);
                _createdItems[i].SetData(data[i], i);
            }

            for (int i = data.Length; i < _createdItems.Count; i++)
            {
                _createdItems[i].gameObject.SetActive(false);
            }
        }
    }
}