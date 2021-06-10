using NUnit.Framework;
using System;
using IIG.FileWorker;

namespace lab2
{
    [TestFixture()]
    public class ReadLinesTests
    {
        [Test()]
        public void EmptyFile_ReadLines()
        {
            string path = "./../Mark.txt";
            string[] resulting = BaseFileWorker.ReadLines(path);
            Assert.IsNotNull(resulting);
        }
        [Test()]
        public void UnexistingFile_ReadLines()
        {
            string path = "/Users/markzegelman/Unreal.txt";
            string[] resulting = BaseFileWorker.ReadLines(path);
            Assert.Null(resulting);
        }

        [Test()]
        public void FileWithOneRow_ReadLines()
        {
            string path = "./Mark.txt";
            string[] resulting = BaseFileWorker.ReadLines(path);
            Assert.AreEqual("Mark", resulting[0]);
        }

        [Test()]
        public void FileWithSeveralRows_ReadLines()
        {
            string path = "/Users/markzegelman/Mark.txt";
            string[] resulting = BaseFileWorker.ReadLines(path);
            Assert.AreEqual("Hello", resulting[0]);
            Assert.AreEqual("Mark", resulting[1]);
        }

    }


    [TestFixture()]
    public class ReadAllTests
    {
        [Test()]
        public void EmptyFile_ReadAll()
        {
            string path = "./../Mark.txt";
            string resulting = BaseFileWorker.ReadAll(path);
            Assert.AreEqual("", resulting);
        }

        [Test()]
        public void UnexistingFile_ReadAll()
        {
            string path = "/Users/markzegelman/Unreal.txt";
            string resulting = BaseFileWorker.ReadAll(path);

            Assert.Null(resulting);
        }

        [Test()]
        public void FileWithOneRow_ReadAll()
        {
            string path = "./Mark.txt";
            string resulting = BaseFileWorker.ReadAll(path);
            Assert.AreEqual("Mark", resulting);
        }

        [Test()]
        public void FileWithSeveralRows_ReadAll()
        {
            string path = "/Users/markzegelman/Mark.txt";
            string resulting = BaseFileWorker.ReadAll(path);
            Assert.AreEqual("Hello\nMark", resulting);
        }

    }



    [TestFixture()]
    public class TryCopyTests
    {
        [Test()]
        public void WrongPathFrom_TryCopy()
        {
            try
            {
                string pathFrom = "./Unreal.txt";
                string pathTo = "./Mark1.txt";
                bool resulting = BaseFileWorker.TryCopy(pathFrom, pathTo, false, 2);
                Assert.Fail("Expected error");
            }
            catch (Exception)
            {
                Assert.True(true);
            }
        }

        [Test()]
        public void WrongPathTo_TryCopy()
        {
            try
            {
                string pathFrom = "./Mark.txt";
                string pathTo = "/Users/markzegelman/Unreal/Mark.txt";
                bool resulting = BaseFileWorker.TryCopy(pathFrom, pathTo, true, 2);
                Assert.False(resulting);
                Assert.Fail("Expected error");
            }
            catch (Exception)
            {
                Assert.True(true);
            }
        }

        [Test()]
        public void UncreatedFileTo_TryCopy()
        {
            //interesting that file is created
            string pathFrom = "/Users/markzegelman/Mark.txt";
            string pathTo = "./Mark2.txt";
            bool resulting = BaseFileWorker.TryCopy(pathFrom, pathTo, true, 3);
            Assert.True(resulting);
        }

        [Test()]
        public void AmountOfTries0_TryCopy()
        {
            string pathFrom = "/Users/markzegelman/Mark.txt";
            string pathTo = "./Mark1.txt";
            bool resulting = BaseFileWorker.TryCopy(pathFrom, pathTo, false, 0);
            Assert.False(resulting);
        }

        [Test()]
        public void CorrectTrueRewriteWithDefaultAttempts_TryCopy()
        {
            string pathFrom = "/Users/markzegelman/Mark.txt";
            string pathTo = "./Mark1.txt";
            bool resulting = BaseFileWorker.TryCopy(pathFrom, pathTo, true);
            Assert.True(resulting);
        }

        [Test()]
        public void CorrectFalseRewrite_TryCopy()
        {
            try
            {
                string pathFrom = "./Mark.txt";
                string pathTo = "./Mark1.txt";
                bool resulting = BaseFileWorker.TryCopy(pathFrom, pathTo, false, 3);
                Assert.Fail("Expected error");
            }
            catch (Exception)
            {
                Assert.True(true);
            }
        }

    }



    [TestFixture()]
    public class TryWriteTests
    {
        string str = "Mark hello";
        [Test()]
        public void WrongPath_TryWrite()
        {
            //file wan't created but there wasn't error like in TryCopy
            try
            {
                string path = "/Users/markzegelman/Unreal/Mark.txt";
                bool resulting = BaseFileWorker.TryWrite(str, path);
                Assert.False(resulting);
                //Assert.Fail("Expected error file wasn't created");
            }
            catch (Exception)
            {
                Assert.True(true);
            }
        }

        [Test()]
        public void UncreatedFile_TryWrite()
        {
            string path = "./Mark3.txt";
            bool resulting = BaseFileWorker.TryWrite(str, path, 2);
            Assert.True(resulting);
        }

        [Test()]
        public void AmountOfTries0_TryWrite()
        {
            string path = "./Mark1.txt";
            bool resulting = BaseFileWorker.TryWrite(str, path, 0);
            Assert.False(resulting);
        }

        [Test()]
        public void CorrectWithEmptyString_TryWrite()
        {
            string str = "";
            string path = "./Mark1.txt";
            bool resulting = BaseFileWorker.TryWrite(str, path, 3);
            Assert.True(resulting);

        }

    }


    [TestFixture()]
    public class WriteTests
    {
        string str = "Mark hello";
        [Test()]
        public void WrongPath_Write()
        {
            //file wan't created but there wasn't error like in TryCopy
            try
            {
                string path = "/Users/markzegelman/Unreal/Mark.txt";
                bool resulting = BaseFileWorker.Write(str, path);
                Assert.False(resulting);
                //Assert.Fail("Expected error file wasn't created");
            }
            catch (Exception)
            {
                Assert.True(true);
            }
        }

        [Test()]
        public void UncreatedFile_Write()
        {
            string path = "./Mark4.txt";
            bool resulting = BaseFileWorker.Write(str, path);
            Assert.True(resulting);
        }

        [Test()]
        public void CorrectWithEmptyString_Write()
        {
            string str = "";
            string path = "./Mark1.txt";
            bool resulting = BaseFileWorker.TryWrite(str, path);
            Assert.True(resulting);

        }

    }


    [TestFixture()]
    public class GetFileNameTests
    {
        [Test()]
        public void ExistingFullPath_GetFileName()
        {
            string path = "/Users/markzegelman/Mark.txt";
            string liba = BaseFileWorker.GetFileName(path);
            Assert.AreEqual("Mark.txt", liba);
        }

        [Test()]
        public void UnexistingFullPath_GetFileName()
        {
            string path = "/Users/markzegelman/Unreal.txt";
            string liba = BaseFileWorker.GetFileName(path);
            Assert.Null(liba);
        }

        [Test()]
        public void ExistingRelativePath_GetFileName()
        { 
            string path = "./Mark.txt";
            string liba = BaseFileWorker.GetFileName(path);
            Assert.AreEqual("Mark.txt", liba);
        }

        [Test()]
        public void UnExistingRelativePath_GetFileName()
        {
            string path = "./Unreal.txt";
            string liba = BaseFileWorker.GetFileName(path);
            Assert.Null(liba);
        }

        [Test()]
        public void ExistingFullPathWrongRegister_GetFileName() //is working but wasn't expected
        {
            string path = "/Users/Markzegelman/Mark.txt";
            string liba = BaseFileWorker.GetFileName(path);
            Assert.Null(liba);
        }

        [Test()]
        public void FolderPath_GetFileName()
        {
            string path = "/Users/markzegelman/Projects-testing/lab2/lab2/bin/Debug";
            string liba = BaseFileWorker.GetFileName(path);
            Assert.Null(liba);
        }

    }


    [TestFixture()]
    public class GetPathTests
    {
        [Test()]
        public void ExistingFullPath_GetPath()
        {
            string path = "/Users/markzegelman/Mark.txt";
            string liba = BaseFileWorker.GetPath(path);
            Assert.AreEqual("/Users/markzegelman", liba);
        }

        [Test()]
        public void UnexistingFullPath_GetPath()
        {
            string path = "/Users/markzegelman/Unreal.txt";
            string liba = BaseFileWorker.GetPath(path);
            Assert.Null(liba);
        }

        [Test()]
        public void ExistingRelarivePath_GetPath()
        {
            string path = "./Mark.txt";
            string liba = BaseFileWorker.GetPath(path);
            Assert.AreEqual("/Users/markzegelman/Projects-testing/lab2/lab2/bin/Debug", liba);
        }

        [Test()]
        public void UnExistingRelativePath_GetPath()
        {
            string path = "./Unreal.txt";
            string liba = BaseFileWorker.GetPath(path);
            Assert.Null(liba);
        }

        [Test()]
        public void ExistingFullPathWrongRegister_GetPath() 
        {
            //is working but wasn't expected
            string path = "/Users/Markzegelman/Mark.txt";
            string liba = BaseFileWorker.GetPath(path);
            Assert.Null(liba);
        }

        [Test()]
        public void FolderPath_GetPath()
        {
            string path = "/Users/markzegelman";
            string liba = BaseFileWorker.GetPath(path);
            Assert.Null(liba);
        }

    }


    [TestFixture()]
    public class GetFullPathTests
    {
        [Test()]
        public void ExistingFullPath_GetFullPath()
        {
            string path = "/Users/markzegelman/Mark.txt";
            string liba = BaseFileWorker.GetFullPath(path);
            Assert.AreEqual(path, liba);
        }

        [Test()]
        public void UnexistingFullPath_GetFullPath()
        {
            string path = "/Users/markzegelman/file.txt";
            string liba = BaseFileWorker.GetFullPath(path);
            Assert.Null(liba);
        }

        [Test()]
        public void ExistingRelarivePath_GetFullPath()
        {
            string path = "./Mark.txt";
            string liba = BaseFileWorker.GetFullPath(path);
            Assert.AreEqual("/Users/markzegelman/Projects-testing/lab2/lab2/bin/Debug/Mark.txt", liba);
        }

        [Test()]
        public void UnExistingRelativePath_GetFullPath()
        {
            string path = "./Unreal.txt";
            string liba = BaseFileWorker.GetFullPath(path);
            Assert.Null(liba);
        }

        [Test()]
        public void ExistingFullPathWrongRegister_GetFullPath() 
        {
            //is working but wasn't expected
            string path = "/Users/Markzegelman/Mark.txt";
            string liba = BaseFileWorker.GetFullPath(path);
            Assert.Null(liba);
        }

        [Test()]
        public void FolderPath_GetFullPath()
        {
            string path = "/Users/markzegelman/Projects-testing/lab2/lab2/bin/Debug";
            string liba = BaseFileWorker.GetFullPath(path);
            Assert.Null(liba);
        }

    }


    [TestFixture()]
    public class MkDirTests   //were really created
    {
        [Test()]
        public void ExistingFullPath_MkDir()
        {
            string path = "/Users/markzegelman/MyDir";
            string liba = BaseFileWorker.MkDir(path);
            Assert.AreEqual(path, liba);
        }

        [Test()]
        public void UnexistingFullPath_MkDir()
        {
            // was expected null but the path is saved to liba but the dir isn't created
            string path = "/Users/markzegelman/MyDir2/Unreal";
            string liba = BaseFileWorker.MkDir(path);
            Assert.Null(liba);
        }

        [Test()]
        public void ExistingRelarivePath_MkDir()
        {
            string path = "./MyDir";
            string liba = BaseFileWorker.MkDir(path);
            Assert.AreEqual("/Users/markzegelman/Projects-testing/lab2/lab2/bin/Debug/MyDir", liba);
        }

        [Test()]
        public void UnExistingRelativePath_MkDir()
        {
            // was expected null but the path is saved to liba but the dir isn't created
            string path = "./MyDir2/Unreal";
            string liba = BaseFileWorker.MkDir(path);
            Assert.Null(liba);
        }

        [Test()]
        public void ExistingFullPathWrongRegister_MkDir() //is working but wasn't expected
        {
            string path = "/Users/Markzegelman/MyDir1"; //was also created
            string liba = BaseFileWorker.MkDir(path);
            Assert.Null(liba);
        }

        [Test()]
        public void FolderPath_MkDir()
        {
            string path = "/Users/markzegelman/Projects-testing/lab2/lab2/bin/Debug";
            string liba = BaseFileWorker.GetFullPath(path);
            Assert.Null(liba);
        }

    }

}
