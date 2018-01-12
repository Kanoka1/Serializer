using System.IO;

namespace DoublyLinkedList
{
    interface IFileWorker
    {
        void Write(FileStream s, string result);
        string Read(FileStream s);
    }
}
