using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Identifiers;

namespace Identifiers.Test
{
  [TestClass]
  public class Test_MedicareNumber
  {
    [TestMethod]
    public void Test_ValidMedicareNumberWithIrn()
    {
      string MedicareNumber = "6140523093";
      string Irn = "1";
      Australian.MedicareNumber oMedNumber = new Australian.MedicareNumber(MedicareNumber, Irn);
      Assert.IsTrue(oMedNumber.IsValid());
    }

    [TestMethod]
    public void Test_ValidMedicareNumberNoIrn()
    {
      string MedicareNumber = "6140523093";      
      Australian.MedicareNumber oMedNumber = new Australian.MedicareNumber(MedicareNumber);
      Assert.IsTrue(oMedNumber.IsValid());
    }

    [TestMethod]
    public void Test_ValidMedicareNumberNoIrnStaticMethod()
    {
      string MedicareNumberNoIrn = "6140523093";      
      Assert.IsTrue(Australian.MedicareNumber.IsValid(MedicareNumberNoIrn));
    }

    [TestMethod]
    public void Test_ValidMedicareNumberWithIrnStaticMethod()
    {
      string MedicareNumberWithIrn = "61405230931";      
      Assert.IsTrue(Australian.MedicareNumber.IsValid(MedicareNumberWithIrn));
    }

    [TestMethod]
    public void Test_InValidMedicareNumberWithIrnStaticMethod()
    {
      string MedicareNumberWithIrn = "61405230331";
      Assert.IsFalse(Australian.MedicareNumber.IsValid(MedicareNumberWithIrn));
    }

    [TestMethod]
    public void Test_InValidMedicareNumberNoIrnStaticMethod()
    {
      string MedicareNumberNoIrn = "6140523393";
      Assert.IsFalse(Australian.MedicareNumber.IsValid(MedicareNumberNoIrn));
    }

    [TestMethod]
    public void Test_MedicareNumberGenerationWithNoIRNStaticMethod()
    {
      for (int i = 0; i < 100000; i++)
      {
        string MedicareNumberNoIrn = Australian.MedicareNumber.GenerateRandomMedicareNumber(false);
        Assert.IsTrue(Australian.MedicareNumber.IsValid(MedicareNumberNoIrn));
      }
    }

    [TestMethod]
    public void Test_MedicareNumberGenerationWithIRNStaticMethod()
    {
      for (int i = 0; i < 100000; i++)
      {
        string MedicareNumberWithIrn = Australian.MedicareNumber.GenerateRandomMedicareNumber(true);
        Assert.IsTrue(Australian.MedicareNumber.IsValid(MedicareNumberWithIrn));
      }
    }
  }
}
