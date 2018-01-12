namespace DoublyLinkedList
{
    class Generator
    {
        public ListRand Generate()
        {
            var test1 = new ListNode()
            {
                Data = "1"
            };
            var test2 = new ListNode()
            {
                Data = "2"
            };
            NodeGenerate(test2, test1);
            var test3 = new ListNode()
            {
                Data = "3"
            };
            NodeGenerate(test3, test2);
            var a = new ListRand()
            {
                Count = 3,
                Head = test1,
                Tail = test3
            };
            return a;
        }

        public void NodeGenerate(ListNode now, ListNode prev)
        {
            if (prev != null)
            {
                now.Prev = prev;
                prev.Next = now;
            }
        }
    }
}
