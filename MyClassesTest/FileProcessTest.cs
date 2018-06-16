using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyClasses;
using System.Configuration;
using System.IO;

namespace MyClassesTest
{
    [TestClass]
    public class FileProcessTest
    {
        private const string BadFileName = @"C:\Windows\BadFileName.bad";//created to prevent hard coding

        private string GoodFileName;//created to prevent hard coding


        [TestMethod]
        public void FileName()
        {
            //arrange
            FileProcess fp = new FileProcess();
            bool fromCall;

            //act
            SetGoodFileName();
            File.AppendAllText(GoodFileName, "Some text");
            fromCall = fp.FileExists(GoodFileName);
            File.Delete(GoodFileName);

            //assert
            Assert.IsTrue(fromCall);
        }


        //grabs key value "GoodFileName" and places into GoodFileName
        //then checks to see if it contains token "[AppPath]"
        //then replaces it with the ApplicationData folder
        //so when this runs, it will have a fully qualified path and file name in it
        public void SetGoodFileName()
        {
            GoodFileName = ConfigurationManager.AppSettings["GoodFileName"];
            if (GoodFileName.Contains("[AppPath]"))
            {
                GoodFileName = GoodFileName.Replace("[AppPath]",
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
            }
        }


        [TestMethod]
        public void FileNameDoesNotExist()
        {
            //arrange
            FileProcess fp = new FileProcess();
            bool fromCall;

            //act
            fromCall = fp.FileExists(BadFileName);

            //assert
            Assert.IsFalse(fromCall);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]//this works as the assert for checking a thrown arugment exception
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
