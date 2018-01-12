using DLLSerializerTests;
using DoublyLinkedList;
using NUnit.Framework;
using System.IO;

namespace DLLDetializerTests
{
    [TestFixture]
    class Tests_Deserializer
    {
        [TestCase("test_Deserialize_4.txt", "{[3][1]}{[1][1]}{[2][2]}{[0][3]}")]
        [TestCase("test_Deserialize_3.txt", "{[3][0]}{[1][1]}{[2][2]}{[0][3]}")]
        [TestCase("test_Deserialize_2.txt", "{[3][1]}{[][1]}{[][2]}{[][3]}")]
        [TestCase("test_Deserialize_1.txt", "{[3][0]}{[][1]}{[][2]}{[][3]}")]
        public void Test_Deserialize(string path, string testCase)
        {
            string result = string.Empty;
            using (FileStream s = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                Test_FileWorker.Write(s, testCase);
            }
                            
            using (FileStream s = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                var resultObj = new ListRand();
                resultObj.Deserialize(s);
                result = new Test_ListSerializer().Serialize(resultObj);
            }

            Assert.AreEqual(testCase, result);
        }
        
        [TestCase("test_Deserialize_Crush_4.txt", "{[3][0]}{[sdfsdf][1]}{[2][2]}{[1][3]}")]
        [TestCase("test_Deserialize_Crush_3.txt", "{[sdfsd][0]}{[][1]}{[2][2]}{[1][3]}")]
        [TestCase("test_Deserialize_Crush_2.txt", "{[3][0]}{[5][1]}{[2][2]}{[1][3]}")]
        [TestCase("test_Deserialize_Crush_1.txt", "{[4][0]}{[][1]}{[][2]}{[][3]}")]
        public void Test_Deserialize_Crush(string path, string testCase)
        {
            bool assert = false;
            try
            {
                string result = string.Empty;
                using (FileStream s = new FileStream(path, FileMode.Create, FileAccess.Write))
                {
                    Test_FileWorker.Write(s, testCase);
                }

                using (FileStream s = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    var resultObj = new ListRand();
                    resultObj.Deserialize(s);
                }
            }
            catch
            {
                assert = true;
            }

            Assert.True(assert);
        }
    }
}
