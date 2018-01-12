using System.IO;
using System.Text;

namespace DLLSerializerTests
{
    class Test_FileWorker 
    {
        public static void Write(FileStream s, string result)
        {
            using (var fw = new StreamWriter(s))
            {
                fw.WriteLine(result);
                fw.Flush();
            }
        }

        public static string Read(FileStream s)
        {
            byte[] bytes = new byte[s.Length];
            int numBytesToRead = (int)s.Length;
            int numBytesRead = 0;
            while (numBytesToRead > 0)
            {
                int n = s.Read(bytes, numBytesRead, numBytesToRead);
                if (n == 0)
                    break;

                numBytesRead += n;
                numBytesToRead -= n;
            }
            numBytesToRead = bytes.Length;
            return Encoding.UTF8.GetString(bytes);
        }
    }
}
