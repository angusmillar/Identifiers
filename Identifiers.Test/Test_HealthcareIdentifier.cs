using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Identifiers;

namespace Identifiers.Test
{
  [TestClass]
  public class Test_HealthcareIdentifier
  {
    [TestMethod]
    public void Test_IHI_IsValid_True()
    {
      string ValidIhi = "8003608333428779";
      Australian.HealthcareIdentifier oIhi = new Australian.HealthcareIdentifier(ValidIhi);
      Assert.IsTrue(oIhi.IsValid());
    }

    [TestMethod]
    public void Test_IHI_IsValid_False()
    {
      //037 should be 036 
      string ValidIhi = "8003708333428779";
      Australian.HealthcareIdentifier oIhi = new Australian.HealthcareIdentifier(ValidIhi);
      Assert.IsFalse(oIhi.IsValid());
    }

    [TestMethod]
    public void Test_IHI_IsValidCheckDigit_True()
    {
      string ValidIhi = "8003608333428779";
      Australian.HealthcareIdentifier oIhi = new Australian.HealthcareIdentifier(ValidIhi);
      Assert.IsTrue(oIhi.IsValidCheckDigit());
    }

    [TestMethod]
    public void Test_IHI_IsValidCheckDigit_Fasle()
    {
      string ValidIhi = "8003608333428778";
      Australian.HealthcareIdentifier oIhi = new Australian.HealthcareIdentifier(ValidIhi);
      Assert.IsFalse(oIhi.IsValidCheckDigit());
    }

    [TestMethod]
    public void Test_IHI_HealthcareIdentifierType_IsIHI_True()
    {
      string ValidIhi = "8003608333428779";
      Australian.HealthcareIdentifier oIhi = new Australian.HealthcareIdentifier(ValidIhi);
      Assert.IsTrue(oIhi.HealthcareIdentifierType == Australian.HealthcareIdentifierType.Individual);
    }

    [TestMethod]
    public void Test_IHI_HealthcareIdentifierType_IsHPI_I_True()
    {
      string ValidIhi = "8003618333428779";
      Australian.HealthcareIdentifier oIhi = new Australian.HealthcareIdentifier(ValidIhi);
      Assert.IsTrue(oIhi.HealthcareIdentifierType == Australian.HealthcareIdentifierType.Provider);
    }

    [TestMethod]
    public void Test_IHI_HealthcareIdentifierType_IsHPI_O_True()
    {
      string ValidIhi = "8003628333428779";
      Australian.HealthcareIdentifier oIhi = new Australian.HealthcareIdentifier(ValidIhi);
      Assert.IsTrue(oIhi.HealthcareIdentifierType == Australian.HealthcareIdentifierType.Orginisation);
    }

    [TestMethod]
    public void Test_IHI_HealthcareIdentifierType_IsWrong()
    {
      string ValidIhi = "8003638333428779";
      Australian.HealthcareIdentifier oIhi = new Australian.HealthcareIdentifier(ValidIhi);
      Assert.IsTrue(oIhi.HealthcareIdentifierType == Australian.HealthcareIdentifierType.None);
    }

    [TestMethod]
    public void Test_IHI_HealthcareIdentifierType_IsWrong_IsValid_False()
    {
      string ValidIhi = "8003638333428779";
      Australian.HealthcareIdentifier oIhi = new Australian.HealthcareIdentifier(ValidIhi);
      Assert.IsFalse(oIhi.IsValid());
    }

    [TestMethod]
    public void Test_IHI_HealthcareIdentifierType_Error()
    {
      string ValidIhi = "800363833";
      Australian.HealthcareIdentifier oIhi = new Australian.HealthcareIdentifier(ValidIhi);
      Assert.IsFalse(oIhi.IsValid());
    }

    [TestMethod]
    public void Test_IHI_GenerateRandomIHI()
    {
      string ValidIhi = "8003608333428779";
      Australian.HealthcareIdentifier oIhi = new Australian.HealthcareIdentifier(ValidIhi);
      string NewRandom = oIhi.GenerateRandomHealthcareIdentifier(Australian.HealthcareIdentifierType.Individual);
      Assert.IsTrue(Australian.HealthcareIdentifier.IsValid(NewRandom, Australian.HealthcareIdentifierType.Individual));
    }

    [TestMethod]
    public void Test_IHI_GenerateRandomHPI_I()
    {
      string ValidIhi = "8003608333428779";
      Australian.HealthcareIdentifier oIhi = new Australian.HealthcareIdentifier(ValidIhi);
      string NewRandom = oIhi.GenerateRandomHealthcareIdentifier(Australian.HealthcareIdentifierType.Provider);
      bool Result = Australian.HealthcareIdentifier.IsValid(NewRandom, Australian.HealthcareIdentifierType.Provider);
      Assert.IsTrue(Result);
    }

    [TestMethod]
    public void Test_IHI_GenerateRandomHPI_O()
    {
      string ValidIhi = "8003608333428779";
      Australian.HealthcareIdentifier oIhi = new Australian.HealthcareIdentifier(ValidIhi);
      string NewRandom = oIhi.GenerateRandomHealthcareIdentifier(Australian.HealthcareIdentifierType.Orginisation);
      Assert.IsTrue(Australian.HealthcareIdentifier.IsValid(NewRandom, Australian.HealthcareIdentifierType.Orginisation));
    }
  }
}
