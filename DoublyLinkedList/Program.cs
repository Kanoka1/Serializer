using System.IO;

namespace DoublyLinkedList
{
    class Program
    {
        static void Main(string[] args)
        {
            string pathNew = @"test.txt";

            var a = new Generator().Generate();
            using (FileStream fsNew = new FileStream(pathNew,
                FileMode.Create, FileAccess.Write))
            {
                a.Serialize(fsNew);
            }
            var b = new ListRand();
            using (FileStream fsSource = new FileStream(pathNew,
            FileMode.Open, FileAccess.Read))
            {
                b.Deserialize(fsSource);
            }
        }
    }
}