using DoublyLinkedList;
using System;
using System.Text.RegularExpressions;

namespace DLLSerializerTests
{
    partial class Test_ListSerializer
    {
        const int emptyParam = -1;

        const int countParam = 0;
        const int isCircuitParam = 1;

        const int rndParam = 0;
        const int data = 1;

        //input: {[count][isCyclic]}{[rnd][data]}{[rnd][data]}...
        public ListRand Deserialize(string s)
        {
            var firstGroup = Regex.Match(s, @"\{([^{}]*)\}").Groups[1].Value;
            var startParams = Regex.Matches(firstGroup, @"\[(([^[]]*)*)\]");
            int count = GetCount(startParams);
            var isCyclic = GetCyclicFlag(startParams);
            var list = ParseToArray(count, s);
            return BuildListRand(count, isCyclic, list);
        }

        int GetCount(MatchCollection startParams)
        {
            var count = 0;
            if (!Int32.TryParse(startParams[countParam].Groups[1].Value, out count))
                throw new Exception("Неверное значение count");
            return count;
        }

        bool GetCyclicFlag(MatchCollection startParams)
        {
            return startParams[isCircuitParam].Groups[1].Value == "1" ? true : false;
        }

        ListNode[] ParseToArray(int count, string s)
        {
            var nodes = new ListNode[count];
            for (int i = 0; i < count; i++)
            {
                s = s.Substring(s.IndexOf('}') + 1);
                var c = Regex.Match(s, @"\{([^{}]*)\}").Groups[1].Value;
                MatchCollection matches = Regex.Matches(c, @"\[([^[]]*)*\]");

                string[] objectParams = new string[4];
                int k = 0;
                foreach (Match match in matches)
                {
                    objectParams[k] = match.Groups[1].Value;
                    k++;
                }
                BuildObject(nodes, objectParams, i, count);

            }
            return nodes;
        }

        void BuildObject(ListNode[] list, string[] objectParams, int i, int count)
        {
            CreateNewIfNull(list, i);
            list[i].Data = objectParams[data];

            if (i - 1 >= 0)
            {
                CreateNewIfNull(list, i - 1);
                list[i].Prev = list[i - 1];
            }
            if (i + 1 < count)
            {
                CreateNewIfNull(list, i + 1);
                list[i].Next = list[i + 1];
            }
            var value = GetIntParam(objectParams[rndParam]);
            if (value >= count)
                throw new Exception("Неверная ссылка на rnd объект");
            if (value != emptyParam)
            {
                CreateNewIfNull(list, value);
                list[i].Rand = list[value];
            }
        }

        void CreateNewIfNull(ListNode[] list, int i)
        {
            if (list[i] == null)
                list[i] = new ListNode();
        }

        int GetIntParam(string value)
        {
            var result = emptyParam;
            if (value == string.Empty)
                return result;
            if (!Int32.TryParse(value, out result))
                throw new Exception("Ошибка входных данных");
            return result;

        }

        ListRand BuildListRand(int count, bool isCyclic, ListNode[] list)
        {
            var result = new ListRand();
            result.Count = count;
            result.Head = list[0];
            result.Tail = list[count - 1];
            if (isCyclic)
            {
                result.Head.Prev = result.Tail;
                result.Tail.Next = result.Head;
            }
            return result;
        }
    }
}
