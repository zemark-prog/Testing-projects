using NUnit.Framework;
using System;
using IIG.BinaryFlag;

namespace Lab3
{
    [TestFixture()]
    public class ConstructorTesting
    {
        [Test()]
        public void UnderMinTrue()  //way 0->1->5 true (less than min)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                MultipleBinaryFlag testBinaryFlag = new MultipleBinaryFlag(1, true);
            });
        }

        [Test()]
        public void UnderMinFalse()  //way 0->1->5 false (less than min)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                MultipleBinaryFlag testBinaryFlag = new MultipleBinaryFlag(1, false);
            });
        }

        [Test()]
        public void AboveMaxTrue()  //way 0->1->5 true (above max)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                MultipleBinaryFlag testBinaryFlag = new MultipleBinaryFlag(17179868705, true);
            });
        }

        [Test()]
        public void AboveMaxFalse()  //way 0->1->5 false (above max)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                MultipleBinaryFlag testBinaryFlag = new MultipleBinaryFlag(17179868705, false);
            });
        }

        [Test()]
        public void Under33True()   //way 0->2->5 true 
        {
            MultipleBinaryFlag flag = new MultipleBinaryFlag(32);
            Assert.True(flag is MultipleBinaryFlag);

        }

        [Test()]
        public void Under33False()   //way 0->2->5 false
        {
            MultipleBinaryFlag flag = new MultipleBinaryFlag(32, false);
            Assert.True(flag is MultipleBinaryFlag);

        }

        [Test()]
        public void Under65True()   //way 0->3->5 true
        {
            MultipleBinaryFlag flag = new MultipleBinaryFlag(64);
            Assert.True(flag is MultipleBinaryFlag);
        }

        [Test()]
        public void Under65False()   //way 0->3->5 false
        {
            MultipleBinaryFlag flag = new MultipleBinaryFlag(64, false);
            Assert.True(flag is MultipleBinaryFlag);
        }

        [Test()]
        public void Over65True()   //way 0->4->5 true
        {
            MultipleBinaryFlag flag = new MultipleBinaryFlag(66, true);
            Assert.True(flag is MultipleBinaryFlag);
        }

        [Test()]
        public void Over65False()   //way 0->4->5 false
        {
            MultipleBinaryFlag flag = new MultipleBinaryFlag(66, true);
            Assert.True(flag is MultipleBinaryFlag);
        }
    }


    [TestFixture()]
    public class DisposeTesting
    {
        [Test()]
        public void Disposed()   //way 0->1->2->3
        {
            MultipleBinaryFlag testBinaryFlag = new MultipleBinaryFlag(11, false);
            testBinaryFlag.Dispose();
            Assert.Null(testBinaryFlag.GetFlag());
        }

        [Test()]
        public void DisposedDisposed()  //way 0->3
        {
            MultipleBinaryFlag testBinaryFlag = new MultipleBinaryFlag(65, true);
            testBinaryFlag.Dispose();
            testBinaryFlag.Dispose();
            Assert.Null(testBinaryFlag.GetFlag());
        }
    }


    [TestFixture()]
    public class SetFlagTesting
    {
        [Test()]
        public void SetFlagToImpossiblePosition()   //way 0->1->6
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                MultipleBinaryFlag testBinaryFlag = new MultipleBinaryFlag(8, true);
                testBinaryFlag.SetFlag(9);
            });
        }

        [Test()]
        public void SetFlagDisposedLengthUnder33()  //way 0->5->6 (length less 33)
        {
            MultipleBinaryFlag testBinaryFlag = new MultipleBinaryFlag(4, false);
            testBinaryFlag.Dispose();
            testBinaryFlag.SetFlag(1);
            Assert.Null(testBinaryFlag.GetFlag());
        }

        [Test()]
        public void SetFlagLengthUnder33()  //way 0->2->6
        {
            MultipleBinaryFlag testBinaryFlag = new MultipleBinaryFlag(2, false);
            testBinaryFlag.SetFlag(0);
            testBinaryFlag.SetFlag(1);
            string res1 = string.Join("", testBinaryFlag.ToString()[0]);
            string res2 = string.Join("", testBinaryFlag.ToString()[1]);
            Assert.AreEqual("T", res1);
            Assert.AreEqual("T", res2);
        }

        [Test()]
        public void SetFlagDisposedLengthUnder65()  //way 0->5->6 (length less 65)
        {
            MultipleBinaryFlag testBinaryFlag = new MultipleBinaryFlag(54, false);
            testBinaryFlag.Dispose();
            testBinaryFlag.SetFlag(3);
            Assert.Null(testBinaryFlag.GetFlag());
        }

        [Test()]
        public void SetFlagLengthUnder65()  //way 0->3->6
        {
            MultipleBinaryFlag testBinaryFlag = new MultipleBinaryFlag(35, false);
            uint num = 8;
            testBinaryFlag.SetFlag(num);
            testBinaryFlag.SetFlag(num);
            string res = string.Join("", testBinaryFlag.ToString()[8]);
            Assert.AreEqual("T", res);
        }

        [Test()]
        public void SetFlagDisposedLengthAbove65()  //way 0->5->6 (length more 65)
        {
            MultipleBinaryFlag testBinaryFlag = new MultipleBinaryFlag(111, false);
            testBinaryFlag.Dispose();
            testBinaryFlag.SetFlag(77);
            Assert.Null(testBinaryFlag.GetFlag());
        }

        [Test()]
        public void SetFlagLengtAbove65()  //way 0->4->6
        {
            MultipleBinaryFlag testBinaryFlag = new MultipleBinaryFlag(1200, false);
            testBinaryFlag.SetFlag(1000);
            string res = string.Join("", testBinaryFlag.ToString()[1000]);
            Assert.AreEqual("T", res);
        }

    }


    [TestFixture()]
    public class ResetFlagTesting
    {
        [Test()]
        public void ResetFlagToImpossiblePosition()   //way 0->1->6
        {

            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                MultipleBinaryFlag testBinaryFlag = new MultipleBinaryFlag(8, false);
                testBinaryFlag.ResetFlag(9);
            });
        }

        public void ResetFlagDisposedLengthUnder33()   //way 0->5->6 (length less 33)
        {
            MultipleBinaryFlag testBinaryFlag = new MultipleBinaryFlag(4, true);
            testBinaryFlag.Dispose();
            testBinaryFlag.ResetFlag(1);
            Assert.Null(testBinaryFlag.GetFlag());
        }

        [Test()]
        public void ResetFlagLengthUnder33() //way 0->2->6
        {
            MultipleBinaryFlag testBinaryFlag = new MultipleBinaryFlag(2, true);
            testBinaryFlag.ResetFlag(0);
            testBinaryFlag.ResetFlag(1);
            string res1 = string.Join("", testBinaryFlag.ToString()[0]);
            string res2 = string.Join("", testBinaryFlag.ToString()[1]);
            Assert.AreEqual("F", res1);
            Assert.AreEqual("F", res2);
        }

        public void ResetFlagDisposedLengthUnder65()   //way 0->5->6 (length less 65)
        {
            MultipleBinaryFlag testBinaryFlag = new MultipleBinaryFlag(62, true);
            testBinaryFlag.Dispose();
            testBinaryFlag.ResetFlag(33);
            Assert.Null(testBinaryFlag.GetFlag());
        }

        [Test()]
        public void ResetFlagLengthUnder65() //way 0->3->6
        {
            MultipleBinaryFlag testBinaryFlag = new MultipleBinaryFlag(35, true);
            uint num = 8;
            testBinaryFlag.ResetFlag(num);
            testBinaryFlag.ResetFlag(num);
            string res = string.Join("", testBinaryFlag.ToString()[8]);
            Assert.AreEqual("F", res);
        }

        public void ResetFlagDisposedLengthAbove65()   //way 0->5->6 (length more 65)
        {
            MultipleBinaryFlag testBinaryFlag = new MultipleBinaryFlag(2321, true);
            testBinaryFlag.Dispose();
            testBinaryFlag.ResetFlag(1111);
            Assert.Null(testBinaryFlag.GetFlag());
        }

        [Test()]
        public void ResetFlagLengtAbove65() //way 0->4->6
        {
            MultipleBinaryFlag testBinaryFlag = new MultipleBinaryFlag(1200, true);
            testBinaryFlag.ResetFlag(1000);
            string res = string.Join("", testBinaryFlag.ToString()[1000]);
            Assert.AreEqual("F", res);
        }

    }


    [TestFixture()]
    public class GetFlagTesting
    {
        [Test()]
        public void GetFlagDisposedLengthUnder33()  //way 0->5->6 (length less 33)
        {
            MultipleBinaryFlag testBinaryFlag = new MultipleBinaryFlag(4, true);
            testBinaryFlag.Dispose();
            Assert.Null(testBinaryFlag.GetFlag());
        }

        [Test()]
        public void GetFlagLengthUnder33()  //way 0->2->6
        {
            MultipleBinaryFlag testBinaryFlag = new MultipleBinaryFlag(2, false);
            testBinaryFlag.SetFlag(0);
            testBinaryFlag.SetFlag(1);
            Assert.True(testBinaryFlag.GetFlag());
        }

        [Test()]
        public void GetFlagDisposedLengthUnder65()  //way 0->5->6 (length less 65)
        {
            MultipleBinaryFlag testBinaryFlag = new MultipleBinaryFlag(44, false);
            testBinaryFlag.Dispose();
            Assert.Null(testBinaryFlag.GetFlag());
        }

        [Test()]
        public void GetFlagLengthUnder65()  //way 0->3->6
        {
            MultipleBinaryFlag testBinaryFlag = new MultipleBinaryFlag(42, true);
            uint num = 0;
            testBinaryFlag.ResetFlag(num);
            testBinaryFlag.SetFlag(num);
            testBinaryFlag.ResetFlag(num);
            Assert.False(testBinaryFlag.GetFlag());
        }

        [Test()]
        public void GetFlagDisposedLengthAbove65()  //way 0->5->6 (length more 65)
        {
            MultipleBinaryFlag testBinaryFlag = new MultipleBinaryFlag(11100, false);
            testBinaryFlag.Dispose();
            Assert.Null(testBinaryFlag.GetFlag());
        }

        [Test()]
        public void GetFlagLengthAbove65()    //way 0->4->6
        {
            MultipleBinaryFlag testBinaryFlag = new MultipleBinaryFlag(234, true);
            uint num = 0;
            testBinaryFlag.ResetFlag(num);
            testBinaryFlag.SetFlag(num);
            testBinaryFlag.SetFlag(num);
            Assert.True(testBinaryFlag.GetFlag());
        }

    }

}
