using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Identifiers;

namespace Identifiers.Test
{
  [TestClass]
  public class Test_NationalHealthcareIdentifier
  {
    [TestMethod]
    public void Test_IHI_IsValid_True()
    {
      string ValidIhi = "8003608333428779";
      Australian.NationalHealthcareIdentifier oIhi = new Australian.NationalHealthcareIdentifier(ValidIhi);
      Assert.IsTrue(oIhi.IsValid());
    }

    [TestMethod]
    public void Test_HPI_I_IsValid_True()
    {
      string ValidIhi = "8003610001218573";
      Australian.NationalHealthcareIdentifier oIhi = new Australian.NationalHealthcareIdentifier(ValidIhi);
      Assert.IsTrue(oIhi.IsValid());
    }


    [TestMethod]
    public void Test_HPI_I_IsValid_CheckDigit_Fasle()
    {
      string ValidIhi = "8003610001218577";
      Australian.NationalHealthcareIdentifier oIhi = new Australian.NationalHealthcareIdentifier(ValidIhi);
      Assert.IsFalse(oIhi.IsValid());
    }

    [TestMethod]
    public void Test_IHI_IsValid_False()
    {
      //037 should be 036 
      string ValidIhi = "8003708333428779";
      Australian.NationalHealthcareIdentifier oIhi = new Australian.NationalHealthcareIdentifier(ValidIhi);
      Assert.IsFalse(oIhi.IsValid());
    }

    [TestMethod]
    public void Test_IHI_IsValidCheckDigit_True()
    {
      string ValidIhi = "8003608333428779";
      Australian.NationalHealthcareIdentifier oIhi = new Australian.NationalHealthcareIdentifier(ValidIhi);
      Assert.IsTrue(oIhi.IsValidCheckDigit());
    }

    [TestMethod]
    public void Test_IHI_IsValidCheckDigit_Fasle()
    {
      string ValidIhi = "8003608333428778";
      Australian.NationalHealthcareIdentifier oIhi = new Australian.NationalHealthcareIdentifier(ValidIhi);
      Assert.IsFalse(oIhi.IsValidCheckDigit());
    }

    [TestMethod]
    public void Test_IHI_HealthcareIdentifierType_IsIHI_True()
    {
      string ValidIhi = "8003608333428779";
      Australian.NationalHealthcareIdentifier oIhi = new Australian.NationalHealthcareIdentifier(ValidIhi);
      Assert.IsTrue(oIhi.HealthcareIdentifierType == Australian.NationalHealthcareIdentifierType.Individual);
    }

    [TestMethod]
    public void Test_IHI_HealthcareIdentifierType_IsHPI_I_True()
    {
      string ValidIhi = "8003618333428779";
      Australian.NationalHealthcareIdentifier oIhi = new Australian.NationalHealthcareIdentifier(ValidIhi);
      Assert.IsTrue(oIhi.HealthcareIdentifierType == Australian.NationalHealthcareIdentifierType.Provider);
    }

    [TestMethod]
    public void Test_IHI_HealthcareIdentifierType_IsHPI_O_True()
    {
      string ValidIhi = "8003628333428779";
      Australian.NationalHealthcareIdentifier oIhi = new Australian.NationalHealthcareIdentifier(ValidIhi);
      Assert.IsTrue(oIhi.HealthcareIdentifierType == Australian.NationalHealthcareIdentifierType.Orginisation);
    }

    [TestMethod]
    public void Test_IHI_HealthcareIdentifierType_IsWrong()
    {
      string ValidIhi = "8003638333428779";
      Australian.NationalHealthcareIdentifier oIhi = new Australian.NationalHealthcareIdentifier(ValidIhi);
      Assert.IsTrue(oIhi.HealthcareIdentifierType == Australian.NationalHealthcareIdentifierType.None);
    }

    [TestMethod]
    public void Test_IHI_HealthcareIdentifierType_IsWrong_IsValid_False()
    {
      string ValidIhi = "8003638333428779";
      Australian.NationalHealthcareIdentifier oIhi = new Australian.NationalHealthcareIdentifier(ValidIhi);
      Assert.IsFalse(oIhi.IsValid());
    }

    [TestMethod]
    public void Test_IHI_GenerateRandomIHI()
    {
      string NewRandom = Australian.NationalHealthcareIdentifier.GenerateRandomHealthcareIdentifier(Australian.NationalHealthcareIdentifierType.Individual);
      Assert.IsTrue(Australian.NationalHealthcareIdentifier.IsValid(NewRandom, Australian.NationalHealthcareIdentifierType.Individual));
    }

    [TestMethod]
    public void Test_IHI_GenerateRandomHPI_I()
    {
      string NewRandom = Australian.NationalHealthcareIdentifier.GenerateRandomHealthcareIdentifier(Australian.NationalHealthcareIdentifierType.Provider);
      bool Result = Australian.NationalHealthcareIdentifier.IsValid(NewRandom, Australian.NationalHealthcareIdentifierType.Provider);
      Assert.IsTrue(Result);
    }

    [TestMethod]
    public void Test_IHI_GenerateRandomHPI_O()
    {
      string NewRandom = Australian.NationalHealthcareIdentifier.GenerateRandomHealthcareIdentifier(Australian.NationalHealthcareIdentifierType.Orginisation);
      Assert.IsTrue(Australian.NationalHealthcareIdentifier.IsValid(NewRandom, Australian.NationalHealthcareIdentifierType.Orginisation));
    }

    [TestMethod]
    public void Test_IHIWhichIsToLong_IsValid_False()
    {
      string InValidIhi = "8003608333428779333";
      Australian.NationalHealthcareIdentifier oIhi = new Australian.NationalHealthcareIdentifier(InValidIhi);
      Assert.IsFalse(oIhi.IsValid());
    }

    [TestMethod]
    public void Test_IHIWhichIstoShort_False()
    {
      string InValidIhi = "8003608333429";      
      Australian.NationalHealthcareIdentifier oIhi = new Australian.NationalHealthcareIdentifier(InValidIhi);
      Assert.IsFalse(oIhi.IsValid());
    }

    [TestMethod]
    public void Test_GetCheckDigit_Returns_Correct_CheckDigit()
    {
      string ValidIhiNoCheckDigit = "800360833342877";
      string ValidIhi = ValidIhiNoCheckDigit  + Australian.NationalHealthcareIdentifier.GetCheckDigit(ValidIhiNoCheckDigit);
      Australian.NationalHealthcareIdentifier oIhi = new Australian.NationalHealthcareIdentifier(ValidIhi);
      Assert.IsTrue(oIhi.IsValid());
    }

    [TestMethod]
    public void Test_ValueWithRootOID_ReturnsCorrect()
    {      
      string ValidIhi = "8003608333428779";
      Australian.NationalHealthcareIdentifier oIhi = new Australian.NationalHealthcareIdentifier(ValidIhi);
      string RootOid = "1.2.36.1.2001.1003.0";
      Assert.AreEqual(oIhi.ValueWithRootOID, RootOid + "." + ValidIhi);
    }

    [TestMethod]
    public void Test_OID_For_NationalIdentifers()
    {
      Assert.AreEqual(Australian.NationalHealthcareIdentifier.RootHealthcareIdentifierOid, "1.2.36.1.2001.1003.0");
    }

    [TestMethod]
    public void Test_IHI_Hl7_V2_IdentifierTypeCode()
    {     
      Assert.AreEqual(Australian.NationalHealthcareIdentifier.Hl7V2.Ihi.IdentifierTypeCode, "NI");
    }

    [TestMethod]
    public void Test_IHI_Hl7_V2_AssigningAuthority()
    {
      Assert.AreEqual(Australian.NationalHealthcareIdentifier.Hl7V2.Ihi.AssigningAuthority, "AUSHIC");
    }

    [TestMethod]
    public void Test_HPI_I_Hl7_V2_AssigningAuthority()
    {
      Assert.AreEqual(Australian.NationalHealthcareIdentifier.Hl7V2.Hpi_I.AssigningAuthority, "AUSHIC");
    }

    [TestMethod]
    public void Test_HPI_I_Hl7_V2_AssigningFacility()
    {
      Assert.AreEqual(Australian.NationalHealthcareIdentifier.Hl7V2.Hpi_I.AssigningFacility, "NPI");
    }

    [TestMethod]
    public void Test_HPI_O_Hl7_V2_IdentifierTypeCode()
    {
      Assert.AreEqual(Australian.NationalHealthcareIdentifier.Hl7V2.Hpi_O.IdentifierTypeCode, "NOI");
    }

    [TestMethod]
    public void Test_HPI_O_Hl7_V2_AssigningFacilityID()
    {
      Assert.AreEqual(Australian.NationalHealthcareIdentifier.Hl7V2.Hpi_O.AssigningFacilityID, "AUSHIC");
    }

    [TestMethod]
    public void Test_IHI_Hl7_CDA_AssigningFacilityID()
    {
      Assert.AreEqual(Australian.NationalHealthcareIdentifier.Hl7Cda.Ihi.AssigningAuthorityName, "IHI");
    }

    [TestMethod]
    public void Test_HPI_I_Hl7_CDA_AssigningFacilityID()
    {
      Assert.AreEqual(Australian.NationalHealthcareIdentifier.Hl7Cda.Hpi_I.AssigningAuthorityName, "HPI-I");
    }

    [TestMethod]
    public void Test_HPI_O_Hl7_CDA_AssigningFacilityID()
    {
      Assert.AreEqual(Australian.NationalHealthcareIdentifier.Hl7Cda.Hpi_O.AssigningAuthorityName, "HPI-O");
    }
  }
}
