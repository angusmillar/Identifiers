using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Identifiers;

namespace Identifiers.Test
{
  [TestClass]
  public class Test_MedicareProviderNumber
  {
    [TestMethod]
    public void Test_ValidMedicareProviderNumber()
    {
      string MedicareProviderNumber = "2940675Y";      
      Australian.MedicareProviderNumber oMedicareProviderNumber = new Australian.MedicareProviderNumber(MedicareProviderNumber);
      Assert.IsTrue(oMedicareProviderNumber.IsValid());
    }

    [TestMethod]
    public void Test_InValidMedicareProviderNumber()
    {
      string MedicareProviderNumber = "2940975Y";
      Australian.MedicareProviderNumber oMedicareProviderNumber = new Australian.MedicareProviderNumber(MedicareProviderNumber);
      Assert.IsFalse(oMedicareProviderNumber.IsValid());
    }

    [TestMethod]
    public void Test_InValidMedicareProviderNumberStaticMethod()
    {
      string MedicareProviderNumber = "2940975Y";
      Assert.IsFalse(Australian.MedicareProviderNumber.IsValid(MedicareProviderNumber));      
    }

    [TestMethod]
    public void Test_ValidMedicareProviderNumberStaticMethod()
    {
      string MedicareProviderNumber = "2940675Y";
      Assert.IsTrue(Australian.MedicareProviderNumber.IsValid(MedicareProviderNumber));
    }


  }
}
