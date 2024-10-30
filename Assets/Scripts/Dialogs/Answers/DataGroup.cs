using System.Collections.Generic;
using UnityEngine;

namespace Dialogs.Answers
{
    public class DataGroup<TDataType, TItemWidget>
        where TItemWidget : MonoBehaviour, IItemRenderer<TDataType>
    {
        protected Transform _container;
        protected TItemWidget _itemWidget;

        protected List<TItemWidget> _createdItems = new List<TItemWidget>();

        public DataGroup(Transform container, TItemWidget itemWidget)
        {
            _container = container;
            _itemWidget = itemWidget;
        }

        public virtual void SetData(TDataType[] data)
        {
            for (int i = _createdItems.Count; i < data.Length; i++)
            {
                TItemWidget item = Object.Instantiate(_itemWidget, _container);
                _createdItems.Add(item);
            }

            for (int i = 0; i < data.Length; i++)
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