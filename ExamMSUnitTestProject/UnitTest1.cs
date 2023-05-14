using ExamWPFTestingHtml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;

namespace ExamMSUnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
        }
        
        //
        [TestMethod]
        public void TestMethodHtml()
        {
            TestingHtml testingHtml = new TestingHtml();

            testingHtml.Parser("https://rog.asus.com/motherboards/rog-strix/rog-strix-b560-g-gaming-wifi-model/spec/");

            if (testingHtml.tagFrequency["html"] != 1)
                Assert.Fail("The test is FAILED! Add method is incorrect.");
        }

        [TestMethod]
        public void TestMethodSave()
        {
            TestingHtml testingHtml = new TestingHtml();

            if(testingHtml.Save(@"C:\\Temp\\Test\\TestHtml.txt") == false) 
                Assert.Fail("The test is FAILED! Sum method is incorrect.");
        }

        // тест на идентичность парсера и чтение из сохранненого файла
        [TestMethod]
        public void TestMethodReader()
        {
            TestingHtml testingHtml = new TestingHtml();
            
            testingHtml.Parser("https://rog.asus.com/motherboards/rog-strix/rog-strix-b560-g-gaming-wifi-model/spec/");
            testingHtml.Save(@"C:\\Temp\\Test\\TestHtml.txt");

            var items = File.ReadAllText(@"C:\\Temp\\Test\\TestHtml.txt").Split(new char[] { ':', ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            
            for (int i = 0; i < items.Length; i+=2)
            {
                Assert.AreEqual(testingHtml.tagFrequency[items[i]], int.Parse(items[i + 1]));
            }

        }

        // тест на TestFraquency.Count кол-во тегов в парсинге адреса и в кол-ве в файле
        [TestMethod]
        public void TestMethodCount()
        {
            TestingHtml testingHtml = new TestingHtml();

            testingHtml.Parser("https://rog.asus.com/motherboards/rog-strix/rog-strix-b560-g-gaming-wifi-model/spec/");
            testingHtml.Save(@"C:\\Temp\\Test\\TestHtml.txt");
            
            Assert.AreEqual(testingHtml.tagFrequency.Count, File.ReadAllLines(@"C:\\Temp\\Test\\TestHtml.txt").Length);
        }

        // тест на TestFraquency.Count кол-во тегов в парсинге адреса и в кол-ве в файле
        [TestMethod]
        public void TestMethodThrowsException()
        {
            TestingHtml testingHtml = new TestingHtml();

            testingHtml.Parser("https://rog.asus.com/motherboards/rog-strix/rog-strix-b560-g-gaming-wifi-model/spec/");
            testingHtml.Save(@"C:\\Temp\\Test\\TestHtml.txt");
            testingHtml.ReadFrequencyDictionaryFromFile(@"C:\\Temp\\Test\\TestHtml.txt");

            //Assert.ThrowsException<DirectoryNotFoundException>((() =>testingHtml.Parser(
            //        "https://rog.asus.com/motherboards/rog-strix/rog-strix-b560-g-gaming-wifi-model/spec/")));
        }

        // тест на TestFraquency.Count кол-во тегов в парсинге адреса и в кол-ве в файле
        [TestMethod]
        public void TestMethodEdentity()
        {
            TestingHtml testingHtml = new TestingHtml();

            testingHtml.Parser("https://rog.asus.com/motherboards/rog-strix/rog-strix-b560-g-gaming-wifi-model/spec/");
            testingHtml.ReadFrequencyDictionaryFromFile(@"C:\\Temp\\Test\\TestHtml.txt");
            
               // Assert.AreNotEqual(testingHtml.tagFrequency.Keys, testingHtml.frequencyDictionary.Keys);
                Assert.AreEqual(testingHtml.tagFrequency.Count, testingHtml.frequencyDictionary.Count);
        }

        
        [TestMethod]
        public void TestMethodfrequencyDictionaryHtml()
        {
            TestingHtml testingHtml = new TestingHtml();
            testingHtml.Parser("https://rog.asus.com/motherboards/rog-strix/rog-strix-b560-g-gaming-wifi-model/spec/");
            testingHtml.Save(@"C:\\Temp\\Test\\TestHtml.txt");
            testingHtml.ReadFrequencyDictionaryFromFile(@"C:\\Temp\\Test\\TestHtml.txt");

            if (testingHtml.frequencyDictionary["html"] != 1)
                Assert.Fail("The test is FAILED! Add method is incorrect.");
        }

        [TestMethod]
        public void TestMethodEmptyTagFrequency()
        {
            TestingHtml testingHtml = new TestingHtml();
            testingHtml.Parser("https://rog.asus.com/motherboards/rog-strix/rog-strix-b560-g-gaming-wifi-model/spec/");
            
                Assert.AreNotEqual(0, testingHtml.tagFrequency.Count);
        }

        [TestMethod]
        public void TestMethodEmptyFrequencyDictionary()
        {
            TestingHtml testingHtml = new TestingHtml();
            testingHtml.Parser("https://rog.asus.com/motherboards/rog-strix/rog-strix-b560-g-gaming-wifi-model/spec/");
            testingHtml.Save(@"C:\\Temp\\Test\\TestHtml.txt");
            testingHtml.ReadFrequencyDictionaryFromFile(@"C:\\Temp\\Test\\TestHtml.txt");

            Assert.AreNotEqual(0, testingHtml.frequencyDictionary.Count);
        }
    }
}
