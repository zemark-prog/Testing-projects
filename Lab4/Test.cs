using NUnit.Framework;
using System;
using IIG.FileWorker;
using IIG.CoSFE.DatabaseUtils;
using IIG.BinaryFlag;
using System.Text;

namespace Lab4
{
    [TestFixture()]
    public class TestFileWorker
    {
        private const string Server = @"Zemark-MSSQLSERVER2";
        private const string Database = @"IIG.CoSWE.StorageDB";
        private const bool IsTrusted = true;
        private const string Login = @"sa";
        private const string Password = @"mzeg";
        private const int ConnectionTimeout = 75;

        StorageDatabaseUtils my = new StorageDatabaseUtils(Server, Database, IsTrusted, Login, Password, ConnectionTimeout);

        [Test()]
        public void TestEmptyFile()
        {
            string path = @"/Users/markzegelman/Mark.txt";
            string name = BaseFileWorker.GetFileName(path);
            string nameOld = name;
            string text = BaseFileWorker.ReadAll(path);
            byte[] textInBytes = Encoding.UTF8.GetBytes(text);
            bool added = my.AddFile(name, textInBytes);
            Assert.True(added);
            int? field = my.GetIntBySql("SELECT MAX(FileID) FROM Files");
            bool res = my.GetFile((int)field, out name, out textInBytes);
            Assert.True(res);
            string textNew = Encoding.UTF8.GetString(textInBytes);
            Assert.AreEqual(text, textNew);
            Assert.AreEqual(name, nameOld);
        }

        [Test()]
        public void TestFileWithText()
        {
            string path = @"/Users/markzegelman/Mark.txt";
            string name = BaseFileWorker.GetFileName(path);
            string nameOld = name;
            string toWrite = "Mark";
            BaseFileWorker.Write(toWrite, path);
            string text = BaseFileWorker.ReadAll(path);
            byte[] textInBytes = Encoding.UTF8.GetBytes(text);
            bool added = my.AddFile(name, textInBytes);
            Assert.True(added);
            int? field = my.GetIntBySql("SELECT MAX(FileID) FROM Files");
            bool res = my.GetFile((int)field, out name, out textInBytes);
            Assert.True(res);
            string textNew = Encoding.UTF8.GetString(textInBytes);
            Assert.AreEqual(text, textNew);
            Assert.AreEqual(name, nameOld);
        }

        [Test()]
        public void TestFileWithEmoji()
        {
            string path = @"/Users/markzegelman/Mark.txt";
            string name = BaseFileWorker.GetFileName(path);
            string nameOld = name;
            string toWrite = "😀😀😀";
            BaseFileWorker.TryWrite(toWrite, path, 3);
            string[] text = BaseFileWorker.ReadLines(path);
            byte[] textInBytes = Encoding.UTF8.GetBytes(text[0]);
            bool added = my.AddFile(name, textInBytes);
            Assert.True(added);
            int? field = my.GetIntBySql("SELECT MAX(FileID) FROM Files");
            bool res = my.GetFile((int)field, out name, out textInBytes);
            Assert.True(res);
            string textNew = Encoding.UTF8.GetString(textInBytes);
            Assert.AreEqual(text[0], textNew);
            Assert.AreEqual(name, nameOld);

            bool deleted = my.DeleteFile((int)field);
            Assert.True(deleted);
        }

        [Test()]
        public void TestFileWithHieroglyph()
        {
            string path = @"/Users/markzegelman/Mark.txt";
            string name = BaseFileWorker.GetFileName(path);
            string nameOld = name;
            string toWrite = "会意字會意字";
            BaseFileWorker.TryWrite(toWrite, path);
            string text = BaseFileWorker.ReadAll(path);
            byte[] textInBytes = Encoding.UTF8.GetBytes(text);
            bool added = my.AddFile(name, textInBytes);
            Assert.True(added);
            int? field = my.GetIntBySql("SELECT MAX(FileID) FROM Files");
            bool res = my.GetFile((int)field, out name, out textInBytes);
            Assert.True(res);
            string textNew = Encoding.UTF8.GetString(textInBytes);
            Assert.AreEqual(text, textNew);
            Assert.AreEqual(name, nameOld);

            bool deleted = my.DeleteFile((int)field);
            Assert.True(deleted);
        }

        public void TestFileWithPathInsteadOfName()
        {
            string path = @"/Users/markzegelman/Mark.txt";
            string name = path;
            string toWrite = "Text";
            BaseFileWorker.TryWrite(toWrite, path);
            string text = BaseFileWorker.ReadAll(path);
            byte[] textInBytes = Encoding.UTF8.GetBytes(text);
            bool added = my.AddFile(name, textInBytes);
            Assert.True(added);
            int? field = my.GetIntBySql("SELECT MAX(FileID) FROM Files");
            bool res = my.GetFile((int)field, out name, out textInBytes);
            Assert.True(res);
            string textNew = Encoding.UTF8.GetString(textInBytes);
            Assert.AreEqual(text, textNew);
            Assert.AreEqual(name, path);

            bool deleted = my.DeleteFile((int)field);
            Assert.True(deleted);
        }

        [Test()]
        public void TestGetFiles()
        {
            string path = @"/Users/markzegelman/Mark.txt";
            string name = BaseFileWorker.GetFileName(path);
            string toWrite = "Hi";
            BaseFileWorker.TryWrite(toWrite, path);
            string text = BaseFileWorker.ReadAll(path);
            byte[] textInBytes = Encoding.UTF8.GetBytes(text);
            bool added = my.AddFile(name, textInBytes);
            Assert.True(added);

            System.Data.DataTable res = my.GetFiles(name);
            Assert.IsNotNull(res);
        }

        [Test()]
        public void TestWithStringEmptyName()
        {
            string path = @"/Users/markzegelman/Mark.txt";
            string name = "";
            string text = BaseFileWorker.ReadAll(path);
            byte[] textInBytes = Encoding.UTF8.GetBytes(text);
            bool added = my.AddFile(name, textInBytes);
            Assert.False(added);
        }

    }

    [TestFixture()]
    public class TestBinaryFlag
    {
        private const string Server = @"Zemark-MSSQLSERVER2";
        private const string Database = @"IIG.CoSWE.FlagpoleDB";
        private const bool IsTrusted = true;
        private const string Login = @"sa";
        private const string Password = @"mzeg";
        private const int ConnectionTimeout = 75;
        FlagpoleDatabaseUtils my = new FlagpoleDatabaseUtils(Server, Database, IsTrusted, Login, Password, ConnectionTimeout);

        [Test()]
        public void TestInitializationTrue()
        {
            MultipleBinaryFlag binaryFlag = new MultipleBinaryFlag(4);
            string flagStr = binaryFlag.ToString();
            string flagStrOld = flagStr;
            bool? boolean = binaryFlag.GetFlag();
            bool? added = my.AddFlag(flagStr, (bool)boolean);
            Assert.True(added);
            int? flagID = my.GetIntBySql("SELECT MAX(MultipleBinaryFlagID) FROM MultipleBinaryFlags");
            bool res = my.GetFlag((int)flagID, out flagStr, out boolean);
            Assert.True(res);
            Assert.AreEqual(flagStr, flagStrOld);
            Assert.True(boolean);
        }

        [Test()]
        public void TestInitializationFlase()
        {
            MultipleBinaryFlag binaryFlag = new MultipleBinaryFlag(39, false);
            string flagStr = binaryFlag.ToString();
            string flagStrOld = flagStr;
            bool? boolean = binaryFlag.GetFlag();
            bool? added = my.AddFlag(flagStr, (bool)boolean);
            Assert.True(added);
            int? flagID = my.GetIntBySql("SELECT MAX(MultipleBinaryFlagID) FROM MultipleBinaryFlags");
            bool res = my.GetFlag((int)flagID, out flagStr, out boolean);
            Assert.True(res);
            Assert.AreEqual(flagStr, flagStrOld);
            Assert.False(boolean);
        }

        [Test()]
        public void TestSet()
        {
            MultipleBinaryFlag binaryFlag = new MultipleBinaryFlag(1111, false);
            uint pos = 2;
            binaryFlag.SetFlag(pos);
            string flagStr = binaryFlag.ToString();
            string flagStrOld = flagStr;
            bool? boolean = binaryFlag.GetFlag();
            bool? added = my.AddFlag(flagStr, (bool)boolean);
            Assert.True(added);
            int? flagID = my.GetIntBySql("SELECT MAX(MultipleBinaryFlagID) FROM MultipleBinaryFlags");
            bool res = my.GetFlag((int)flagID, out flagStr, out boolean);
            Assert.True(res);
            string res2 = string.Join("", flagStr[(int)pos]);
            Assert.AreEqual("T", res2);
            Assert.AreEqual(flagStr, flagStrOld);
            Assert.False(boolean);
        }

        [Test()]
        public void TestDoubleSet()
        {
            MultipleBinaryFlag binaryFlag = new MultipleBinaryFlag(1111, false);
            uint pos = 110;
            binaryFlag.SetFlag(pos);
            binaryFlag.SetFlag(pos);
            string flagStr = binaryFlag.ToString();
            string flagStrOld = flagStr;
            bool? boolean = binaryFlag.GetFlag();
            bool? added = my.AddFlag(flagStr, (bool)boolean);
            Assert.True(added);
            int? flagID = my.GetIntBySql("SELECT MAX(MultipleBinaryFlagID) FROM MultipleBinaryFlags");
            bool res = my.GetFlag((int)flagID, out flagStr, out boolean);
            Assert.True(res);
            string res2 = string.Join("", flagStr[(int)pos]);
            Assert.AreEqual("T", res2);
            Assert.AreEqual(flagStr, flagStrOld);
            Assert.False(boolean);
        }


        [Test()]
        public void TestReset()
        {
            MultipleBinaryFlag binaryFlag = new MultipleBinaryFlag(7, true);
            uint pos = 4;
            binaryFlag.ResetFlag(pos);
            string flagStr = binaryFlag.ToString();
            string flagStrOld = flagStr;
            bool? boolean = binaryFlag.GetFlag();
            bool? added = my.AddFlag(flagStr, (bool)boolean);
            Assert.True(added);
            int? flagID = my.GetIntBySql("SELECT MAX(MultipleBinaryFlagID) FROM MultipleBinaryFlags");
            bool res = my.GetFlag((int)flagID, out flagStr, out boolean);
            Assert.True(res);
            string res2 = string.Join("", flagStr[(int)pos]);
            Assert.AreEqual("F", res2);
            Assert.AreEqual(flagStr, flagStrOld);
            Assert.False(boolean);
        }

        [Test()]
        public void TestDoubleReset()
        {
            MultipleBinaryFlag binaryFlag = new MultipleBinaryFlag(100, true);
            uint pos = 19;
            binaryFlag.ResetFlag(pos);
            binaryFlag.ResetFlag(pos);
            string flagStr = binaryFlag.ToString();
            string flagStrOld = flagStr;
            bool? boolean = binaryFlag.GetFlag();
            bool? added = my.AddFlag(flagStr, (bool)boolean);
            Assert.True(added);
            int? flagID = my.GetIntBySql("SELECT MAX(MultipleBinaryFlagID) FROM MultipleBinaryFlags");
            bool res = my.GetFlag((int)flagID, out flagStr, out boolean);
            Assert.True(res);
            string res2 = string.Join("", flagStr[(int)pos]);
            Assert.AreEqual("F", res2);
            Assert.AreEqual(flagStr, flagStrOld);
            Assert.False(boolean);
        }

        [Test()]
        public void TestSetAfterReset()
        {
            MultipleBinaryFlag binaryFlag = new MultipleBinaryFlag(32, true);
            uint pos = 8;
            binaryFlag.ResetFlag(pos);
            binaryFlag.SetFlag(pos);
            string flagStr = binaryFlag.ToString();
            string flagStrOld = flagStr;
            bool? boolean = binaryFlag.GetFlag();
            bool? added = my.AddFlag(flagStr, (bool)boolean);
            Assert.True(added);
            int? flagID = my.GetIntBySql("SELECT MAX(MultipleBinaryFlagID) FROM MultipleBinaryFlags");
            bool res = my.GetFlag((int)flagID, out flagStr, out boolean);
            Assert.True(res);
            string res2 = string.Join("", flagStr[(int)pos]);
            Assert.AreEqual("T", res2);
            Assert.AreEqual(flagStr, flagStrOld);
            Assert.True(boolean);
        }

        [Test()]
        public void TestDispose()
        {
            MultipleBinaryFlag binaryFlag = new MultipleBinaryFlag(65, true);
            binaryFlag.Dispose();
            bool boolean = binaryFlag.GetFlag() ?? false;
            string flagStr = binaryFlag.ToString();
            bool? added = my.AddFlag(flagStr, boolean);
            Assert.False(added);
        }

    }

}
