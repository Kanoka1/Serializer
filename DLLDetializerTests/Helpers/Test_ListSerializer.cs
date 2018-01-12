using DoublyLinkedList;
using System;
using System.Collections.Generic;

namespace DLLSerializerTests
{
    partial class Test_ListSerializer 
    {
        ListRand _obj;
        ListNode[] nodes;
        Dictionary<ListNode, int> dictionary;

        //output: {[count][isCyclic]}{[rnd][data]}{[rnd][data]}...
        public string Serialize(ListRand obj)
        {
            Initialize(obj);
            bool isCyclic = CheckListForCyclicity();
            FromListToArray();
            return FromArrayToString(isCyclic);
        }

        void Initialize(ListRand obj)
        {
            _obj = obj;
            dictionary = new Dictionary<ListNode, int>();
            nodes = new ListNode[obj.Count];
        }

        bool CheckListForCyclicity()
        {
            bool isCyclic = false;
            if (_obj.Head.Prev == _obj.Tail)
            {
                if (_obj.Tail.Next != _obj.Head)
                    throw new Exception("Ошибка в Head/Tail ссылках");
                else isCyclic = true;
            }
            return isCyclic;
        }

        void FromListToArray()
        {
            nodes[0] = _obj.Head;
            dictionary.Add(nodes[0], 0);
            for (var i = 1; i < _obj.Count; i++)
            {
                nodes[i] = nodes[i - 1].Next;
                dictionary.Add(nodes[i], i);
                if (nodes[i].Prev != nodes[i - 1])
                    throw new Exception("Нарушена связность списка. Неверная prev ссылка узла i");
            }
            if (nodes[_obj.Count - 1] != _obj.Tail)
                throw new Exception("Указанное число элементов не соответствует действительности");
        }

        string FromArrayToString(bool isCyclic)
        {
            string result = $"{{[{_obj.Count}][{Convert.ToInt32(isCyclic)}]}}";
            for (var i = 0; i < _obj.Count; i++)
            {
                var prev = nodes[i].Prev != null
                    ? i - 1 >= 0 ? (i - 1).ToString() : (_obj.Count - 1).ToString()
                    : string.Empty;
                var next = nodes[i].Next != null
                    ? i + 1 < _obj.Count ? (i + 1).ToString() : 0.ToString()
                    : string.Empty;
                var rnd = nodes[i].Rand != null ? dictionary[nodes[i].Rand].ToString() : string.Empty;
                var data = nodes[i].Data;
                result += $"{{[{rnd}][{data}]}}";
            }
            return result;
        }
    }
}
