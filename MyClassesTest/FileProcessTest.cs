using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyClasses;

namespace MyClassesTest
{
    [TestClass]
    public class FileProcessTest
    {
        [TestMethod]
        public void FileName()
        {
            //arrange
            FileProcess fp = new FileProcess();
            bool fromCall;

            //act
            fromCall = fp.FileExists(@"C:\Windows\Regedit.exe");

            //assert
            Assert.IsTrue(fromCall);
        }

        [TestMethod]
        public void FileNameDoesNotExist()
        {
            //arrange
            FileProcess fp = new FileProcess();
            bool fromCall;

            //act
            fromCall = fp.FileExists(@"C:\Windows\BadFileName.bad");

            //assert
            Assert.IsFalse(fromCall);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]//this works as the assert for checking a thrown arugment exception
        public void FileNameNullOrEmpty_ThrowsArgumentNullException()
        {
            //arrange
            FileProcess fp = new FileProcess();

            //act
            fp.FileExists("");
            //set the debugger on line 46, right click inside the test and select debug test
            //then step through to see the exception get thrown
        }

        [TestMethod]
        public void FileNameNullOrEmpty_ThrowsArgumentNullException_UsingTryCatch()
        {
            //arrange
            FileProcess fp = new FileProcess();

            //act
            try
            {
                fp.FileExists("");
            }
            catch (ArgumentNullException)//if caught, then test is a success
            {
                //therefore, all we need is a return statement
                return;
            }

            //assert
            Assert.Fail("Call to FileExists did NOT thrown an ArgumentNullException");
        }
    }
}
