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
      string MedicareNumber = "4950481651";
      string Irn = "1";
      Australian.MedicareNumber oMedNumber = new Australian.MedicareNumber(MedicareNumber, Irn);
      Assert.IsTrue(oMedNumber.IsValid());
    }

    [TestMethod]
    public void Test_ValidMedicareNumberNoIrn()
    {
      string MedicareNumber = "4950481651";      
      Australian.MedicareNumber oMedNumber = new Australian.MedicareNumber(MedicareNumber);
      Assert.IsTrue(oMedNumber.IsValid());
    }

    [TestMethod]
    public void Test_ValidMedicareNumberNoIrnStaticMethod()
    {
      string MedicareNumberNoIrn = "49504816511";      
      Assert.IsTrue(Australian.MedicareNumber.IsValid(MedicareNumberNoIrn));
    }

    [TestMethod]
    public void Test_ValidMedicareNumberWithIrnStaticMethod()
    {
      string MedicareNumberWithIrn = "49504816511";      
      Assert.IsTrue(Australian.MedicareNumber.IsValid(MedicareNumberWithIrn));
    }

    [TestMethod]
    public void Test_InValidMedicareNumberWithIrnStaticMethod()
    {
      string MedicareNumberWithIrn = "495048135195";
      Assert.IsFalse(Australian.MedicareNumber.IsValid(MedicareNumberWithIrn));
    }

    [TestMethod]
    public void Test_InValidMedicareNumberNoIrnStaticMethod()
    {
      string MedicareNumberNoIrn = "2950311931";
      Assert.IsFalse(Australian.MedicareNumber.IsValid(MedicareNumberNoIrn));
    }

  }
}
