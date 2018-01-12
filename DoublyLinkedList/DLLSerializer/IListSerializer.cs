namespace DoublyLinkedList
{
    interface IListSerializer
    {
        string Serialize(ListRand obj);
        ListRand Deserialize(string s);
    }
}
