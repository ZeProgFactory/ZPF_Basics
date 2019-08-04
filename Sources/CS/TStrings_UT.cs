﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZPF;

namespace ZPF
{
   // http://www.scribdbook.com/unit-testing-frameworks-in-c-comparing-xunit-nunit-and-visual-studio/
   // https://blogs.msdn.microsoft.com/visualstudio/2016/11/18/live-unit-testing-visual-studio-2017-rc/
   // https://ict.ken.be/unit-testing-with-mstest

   [TestClass]
   public class TStrings_UT
   {
      [TestMethod]
      public void TStrings_FromJSon_Simple_A()
      {
         string JSON = @"{'firstName': 'John','lastName': 'Smith','age': 25}".Replace("'", "\"");

         TStrings json = TStrings.FromJSon(JSON);

         Assert.IsTrue(json.Count == 3 && json["age"] == "25");
      }

      [TestMethod]
      public void TStrings_FromJSon_Simple_B()
      {
         string JSON = @"[{'firstName': 'John','lastName': 'Smith','age': 25}]".Replace("'", "\"");

         TStrings json = TStrings.FromJSon(JSON);

         Assert.IsTrue(json.Count == 3 && json["age"] == "25");
      }

      [TestMethod]
      public void TStrings_FromJSon_Test_A()
      {
         string JSON = "[{\"1\":\"Log\"}, {\"2\":\"Warning\"}, {\"3\":\"Error\"}, {\"4\":\"Critical\"} ]";

         TStrings json = TStrings.FromJSon(JSON);

         Assert.IsTrue(json.Count == 4 && json["3"] == "Error");
      }

      [TestMethod]
      public void TStrings_FromJSon_Test_B()
      {
         string JSON = "[{\"1\" :\"Log\"}, {\"2\": \"Warning\"}, {\"3\" : \"Error\"}, { \"4\":\"Critical\" } ]";

         TStrings json = TStrings.FromJSon(JSON);

         Assert.IsTrue( json.Count == 4 && json["3"]=="Error");
      }

      [TestMethod]
      public void TStrings_FromJSon_Test_C()
      {
         string JSON = "[{\"1\":11 }, {\"2\":22}, {\"3\": 33}, {\"4\": 44 } ]";

         TStrings json = TStrings.FromJSon(JSON);

         Assert.IsTrue(json.Count == 4 && json["3"] == "33");
      }

      string FileName = "";

      [TestMethod]
      public void TStrings_Save_Test_1()
      {
         FileName = System.IO.Path.GetTempFileName() + ".txt";

         TStrings file = new TStrings();
         file.Add(DateTime.Now.ToString());
         file.Add("123");
         file.SaveToFile(FileName);

         bool Exist = System.IO.File.Exists(FileName);

         Assert.AreEqual(true, Exist);
      }

      [TestMethod]
      public void TStrings_Load_Test_1()
      {
         TStrings_Save_Test_1();

         TStrings file = new TStrings();
         file.LoadFromFile(FileName);

         Assert.AreEqual("123", file[1]);
      }

      [TestMethod]
      public void TStrings_Insert_0_1()
      {
         TStrings file = new TStrings();
         file.Add("1");
         file.Add("2");
         file.Add("3");
         file.Insert(0, "0");

         Assert.AreEqual("2", file[2]);
      }

      [TestMethod]
      public void _00_NameValue_01()
      {
         TStrings file = new TStrings();
         file["A"] = "1";
         file["B"] = "2";
         file["C"] = "3";

         Assert.AreEqual("2", file["B"]);
      }

      [TestMethod]
      public void _00_NameValue_02()
      {
         TStrings file = new TStrings();
         file["A"] = "1";
         file["B"] = "2";
         file["C"] = "3";
         file["B"] = "4";

         Assert.AreEqual(3, file.Count );
      }

      [TestMethod]
      public void _00_NameValue_03()
      {
         TStrings file = new TStrings();
         file["A"] = "1";
         file["B"] = "2";
         file["C"] = "3";
         file["B"] = "4";

         Assert.AreEqual("4", file["B"]);
      }
   }
}
