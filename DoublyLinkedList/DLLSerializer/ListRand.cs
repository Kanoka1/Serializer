using System.IO;

namespace DoublyLinkedList
{
    public class ListNode
    {
        public ListNode Prev { get; set; }
        public ListNode Next { get; set; }
        public ListNode Rand { get; set; } // произвольный элемент внутри списка
        public string Data { get; set; }
    }


    public class ListRand
    {
        IFileWorker fileWorker;
        IListSerializer listSerializer;

        public ListRand()
        {
            fileWorker = new FileWorker();
            listSerializer = new ListSerializer();
        }

        public ListNode Head { get; set; }
        public ListNode Tail { get; set; }
        public int Count { get; set; }

        public void Serialize(FileStream s)
        {
            var result = listSerializer.Serialize(this);
            fileWorker.Write(s, result);
        }

        public void Deserialize(FileStream s)
        {
            var read = fileWorker.Read(s);
            var obj = listSerializer.Deserialize(read);
            this.Count = obj.Count;
            this.Head = obj.Head;
            this.Tail = obj.Tail;
        }
    }
}
