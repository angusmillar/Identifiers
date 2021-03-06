﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Identifiers.Australian.DepartmentVeteransAffairs;

namespace Identifiers.Test
{
  [TestClass]  
  public class Test_DVANumber
  {
    [TestMethod]
    [TestCategory("DVA Number")]
    public void Test_Valid_DVA_Number_1()
    {
      string Number = "N ABC1234C";
     
      IDVANumberParser Parser = new DVANumberParser();
      IDVANumber DVA;      
      Assert.IsTrue(Parser.TryParse(Number, out DVA));
      Assert.AreEqual("NABC1234C", DVA.Value);
      Assert.AreEqual("N", DVA.StateCode);
      Assert.AreEqual("ABC", DVA.WarCode);
      Assert.AreEqual("1234", DVA.Number);
      Assert.AreEqual("C", DVA.SegmentLink);
      Assert.AreEqual("ABC1234", DVA.FileNumber);
    }

    [TestMethod]
    [TestCategory("DVA Number")]
    public void Test_Valid_DVA_Number_2()
    {
      string Number = "V A123456";

      IDVANumberParser Parser = new DVANumberParser();
      IDVANumber DVA;
      Assert.IsTrue(Parser.TryParse(Number, out DVA));
      Assert.AreEqual("VA123456", DVA.Value);
      Assert.AreEqual("V", DVA.StateCode);
      Assert.AreEqual("A", DVA.WarCode);
      Assert.AreEqual("123456", DVA.Number);
      Assert.AreEqual("", DVA.SegmentLink);
      Assert.AreEqual("A123456", DVA.FileNumber);
    }

    [TestMethod]
    [TestCategory("DVA Number")]
    public void Test_Valid_DVA_Number_3()
    {
      string Number = "Q ZX12345Z";

      IDVANumberParser Parser = new DVANumberParser();
      IDVANumber DVA;
      Assert.IsTrue(Parser.TryParse(Number, out DVA));
      Assert.AreEqual("QZX12345Z", DVA.Value);
      Assert.AreEqual("Q", DVA.StateCode);
      Assert.AreEqual("ZX", DVA.WarCode);
      Assert.AreEqual("12345", DVA.Number);
      Assert.AreEqual("Z", DVA.SegmentLink);
      Assert.AreEqual("ZX12345", DVA.FileNumber);
    }

    [TestMethod]
    [TestCategory("DVA Number")]
    public void Test_Negative_DVA_Number_BadStateCode()
    {
      string Number = "Z ZX12345Z";

      IDVANumberParser Parser = new DVANumberParser();
      IDVANumber DVA;
      Assert.IsFalse(Parser.TryParse(Number, out DVA));      
    }

    [TestMethod]
    [TestCategory("DVA Number")]
    public void Test_Negative_DVA_Number_ToManyNumber()
    {
      string Number = "N ZX123456Z";

      IDVANumberParser Parser = new DVANumberParser();
      IDVANumber DVA;
      Assert.IsFalse(Parser.TryParse(Number, out DVA));
    }

    [TestMethod]
    [TestCategory("DVA Number")]
    public void Test_Negative_DVA_SegmentLinkNotAnAlpha()
    {
      string Number = "N ZX123458";

      IDVANumberParser Parser = new DVANumberParser();
      IDVANumber DVA;
      Assert.IsFalse(Parser.TryParse(Number, out DVA));
    }

    [TestMethod]
    [TestCategory("DVA Number")]
    public void Test_Negative_DVA_AlphaAfterWarCode()
    {
      string Number = "N Z123S56A";

      IDVANumberParser Parser = new DVANumberParser();
      IDVANumber DVA;
      Assert.IsFalse(Parser.TryParse(Number, out DVA));
    }
  }
}
