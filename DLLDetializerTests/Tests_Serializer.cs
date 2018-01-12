using NUnit.Framework;
using DLLSerializerTests;
using System.IO;
using System;

namespace DLLDetializerTests
{
    [TestFixture]
    public class Tests_Serializer
    {
        [TestCase("test_Serialize_4.txt", "{[3][1]}{[1][1]}{[2][2]}{[0][3]}")]
        [TestCase("test_Serialize_3.txt", "{[3][0]}{[1][1]}{[2][2]}{[0][3]}")]
        [TestCase("test_Serialize_2.txt", "{[3][1]}{[][1]}{[][2]}{[][3]}")]
        [TestCase("test_Serialize_1.txt", "{[3][0]}{[][1]}{[][2]}{[][3]}")]
        public void Test_Serialize(string path, string testCase)
        {
            string result = string.Empty;
            using (FileStream s = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                var testObj = new Test_ListSerializer().Deserialize(testCase);
                testObj.Serialize(s);
            }
            using (FileStream s = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                result = Test_FileWorker.Read(s).Replace(Environment.NewLine, "");
            }

            Assert.AreEqual(testCase, result);
        }

        [TestCase("test_Serialize_Crush_6.txt", "{[3][1]}{[][1]}{[][2]}{[][3]}", -2)]
        [TestCase("test_Serialize_Crush_5.txt", "{[3][1]}{[5][1]}{[1][2]}{[1][3]}", 10)]
        [TestCase("test_Serialize_Crush_4.txt", "{[3][1]}{[][1]}{[][2]}{[][3]}", 10)]
        [TestCase("test_Serialize_Crush_3.txt", "{[3][0]}{[][1]}{[][2]}{[][3]}", -2)]
        [TestCase("test_Serialize_Crush_2.txt", "{[3][0]}{[5][1]}{[1][2]}{[1][3]}", 10)]
        [TestCase("test_Serialize_Crush_1.txt", "{[3][0]}{[][1]}{[][2]}{[][3]}", 10)]
        public void Test_Serialize_Crush(string path, string testCase, int iterate)
        {
            bool assert = false;
            try
            {
                string result = string.Empty;
                using (FileStream s = new FileStream(path, FileMode.Create, FileAccess.Write))
                {
                    var testObj = new Test_ListSerializer().Deserialize(testCase);
                    testObj.Count += iterate;
                    testObj.Serialize(s);
                }
                using (FileStream s = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    result = Test_FileWorker.Read(s).Replace(Environment.NewLine, "");
                }
            }
            catch
            {
                assert = true;
            }

            Assert.True(assert);
        }

        [TestCase("test_Serialize_CrushRelations_2.txt", "{[3][1]}{[][1]}{[][2]}{[][3]}")]
        [TestCase("test_Serialize_CrushRelations_1.txt", "{[3][0]}{[][1]}{[][2]}{[][3]}")]
        public void Test_Serialize_CrushRelations(string path, string testCase)
        {
            bool assert = false;
            try
            {
                string result = string.Empty;
                using (FileStream s = new FileStream(path, FileMode.Create, FileAccess.Write))
                {
                    var testObj = new Test_ListSerializer().Deserialize(testCase);
                    testObj.Head.Next = testObj.Head.Next.Next;
                    testObj.Serialize(s);
                }
                using (FileStream s = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    result = Test_FileWorker.Read(s).Replace(System.Environment.NewLine, "");
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
