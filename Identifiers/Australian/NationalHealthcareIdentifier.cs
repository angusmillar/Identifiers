﻿using System;
using System.ComponentModel;
using System.Text;
using System.Text.RegularExpressions;

namespace Identifiers.Australian
{
  /// <summary>
  /// Australian Healthcare Identifier Types (IHI, HPI-I, HPI-O)
  /// </summary>
  public enum NationalHealthcareIdentifierType
  {
    /// <summary>
    /// Individual Healthcare Identifier (IHI)
    /// </summary>
    Individual,
    /// <summary>
    /// Healthcare Provider Identifier-Individual (HPI-I)
    /// </summary>
    Provider,
    /// <summary>
    /// Healthcare Provider Identifier-Orginisation (HPI-O)
    /// </summary>
    Orginisation,
    /// <summary>
    /// Not a known Identifier type
    /// </summary>
    None,

  }
  /// <summary>
  /// Australian Healthcare Identifiers (IHI, HPI-I, HPI-O)
  /// </summary>
  public class NationalHealthcareIdentifier
  {
    #region Private properties
        
    private string _ValidatationErrorMessage = string.Empty;
    private static readonly string _ValidIndustryCode = "80";
    private static readonly string _ValidCountryCode = "036";
    

    #endregion

    #region Public properties

    /// <summary>
    /// 2 char fixed mandatory numeric
    /// ISO 7812 mandates the value for Healthcare is “80”. 
    /// The first character, the MII, of “8” also includes telecommunications 
    /// and other future industry assignments
    /// </summary>
    public string IndustryCode { get; }

    /// <summary>
    /// 3 char fixed mandatory numeric
    /// ISO 3166-1 (Codes for the representation of names of countries and their 
    /// subdivisions — Part 1: Country codes). Australia is “036”.
    /// </summary>
    public string CountryCode { get; }

    /// <summary>
    /// 1 char fixed mandatory numeric
    /// The Department of Human Services has registered as “0”, “1” , “2” 
    /// Where:
    /// “0” represents IHI
    /// “1” represents HPI-I 
    /// “2” represents HPI-O
    /// </summary>
    public string NumberIssuerCode { get;}

    /// <summary>
    /// 9 char fixed mandatory numeric
    /// The unique reference number will be a unique 9 digit number that will be randomly 
    /// generated by the HI Service.It will need to cater for the Australian 
    /// population(20 million plus) and allow for growth over the life of the HI service.
    /// </summary>
    public string UniqueReference { get;}

    /// <summary>
    /// 1 char fixed mandatory numeric
    /// ISO 7812-1 mandates the use of the Luhn Algorithm to add a check digit to all identification numbers.
    /// </summary>
    public string CheckDigit { get; }

    /// <summary>
    /// If an invalid identifer is passed in on creation which exceeds 16 charaters then the 
    /// content past the 16th charater will be here.
    /// A valid Healthcare Identifier will have no content in this proptery.
    /// </summary>
    public string UnknownContent { get; }

    /// <summary>
    /// Returns the entire Healthcare Identifier value
    /// </summary>
    public string Value
    {
      get
      {
        return IndustryCode + CountryCode + NumberIssuerCode + UniqueReference + CheckDigit + UnknownContent;
      }
    }

    public string ValueWithRootOID
    {
      get
      {
        return NationalHealthcareIdentifier.RootHealthcareIdentifierOid + "." + IndustryCode + CountryCode + NumberIssuerCode + UniqueReference + CheckDigit + UnknownContent;
      }
    }



    /// <summary>
    /// Returns the type of Healthcare Identifier 
    /// Where:
    /// Individual = IHI, 
    /// Provider = HPI-I, 
    /// Orginisation = HPI-O
    /// </summary>
    public NationalHealthcareIdentifierType HealthcareIdentifierType
    {
      get
      {
        if (NumberIssuerCode == "0")
        {
          return NationalHealthcareIdentifierType.Individual;
        }
        else if (NumberIssuerCode == "1")
        {
          return NationalHealthcareIdentifierType.Provider;
        }
        else if (NumberIssuerCode == "2")
        {
          return NationalHealthcareIdentifierType.Orginisation;
        }
        else
        {
          return NationalHealthcareIdentifierType.None;
        }
      }
    }
    #endregion
    
    #region Public Constructors

    /// <summary>
    /// Surply a Healthcare Identifier as a string
    /// </summary>
    /// <param name="HealthcareIdentifierValue"></param>
    public NationalHealthcareIdentifier(string HealthcareIdentifierValue)
    {            
      if (HealthcareIdentifierValue.Length > 0 && HealthcareIdentifierValue.Length >= 2 )      
        this.IndustryCode = HealthcareIdentifierValue.Substring(0, 2);
      if (HealthcareIdentifierValue.Length > 2 && HealthcareIdentifierValue.Length >= 4)
        this.CountryCode = HealthcareIdentifierValue.Substring(2, 3);
      if (HealthcareIdentifierValue.Length > 4 && HealthcareIdentifierValue.Length >= 5)
        this.NumberIssuerCode = HealthcareIdentifierValue.Substring(5, 1);
      if (HealthcareIdentifierValue.Length > 5 && HealthcareIdentifierValue.Length >= 15)
        this.UniqueReference = HealthcareIdentifierValue.Substring(6, 9);
      if (HealthcareIdentifierValue.Length > 15 && HealthcareIdentifierValue.Length >= 16)
        this.CheckDigit = HealthcareIdentifierValue.Substring(15, 1);
      if (HealthcareIdentifierValue.Length > 16)
        this.UnknownContent = HealthcareIdentifierValue.Substring(16, HealthcareIdentifierValue.Length - 16);
      IsValid();
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Validates that the check digit in the 16 position is correct for the Healthcare Identifier Value
    /// </summary>
    /// <returns></returns>
    public bool IsValidCheckDigit()
    {
      if (this.CheckDigit == CheckDigitAlgorithm.Luhn.GetCheckDigit(this.Value.Substring(0, this.Value.Length - 1)))
        return true;
      else
        return false;
    }

    /// <summary>
    /// Validates the identifer is valid, asumes the Healthcare Identifier Type is correct.
    /// HealthcareIdentifierType of 'None' will always return false.
    /// Only validates the syntax does not call the national web services 
    /// </summary>
    /// <returns></returns>
    public bool IsValid()
    {
      return this.IsValid(this.HealthcareIdentifierType, this.Value);
    }
    /// <summary>
    /// Validates the identifer is valid, for the type given.
    /// Only validates the syntax does not call the national web services 
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public bool IsValid(NationalHealthcareIdentifierType type)
    {
      return IsValid(type, this.Value);
    }
    
    /// <summary>
    /// Error message explianing why the validation failed 
    /// </summary>
    /// <returns></returns>
    public string ValidatationErrorMessage()
    {
      this.IsValid();
      return _ValidatationErrorMessage;
    }

    /// <summary>
    /// Generate a random Healthcare Identifier for the type given
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static string GenerateRandomHealthcareIdentifier(NationalHealthcareIdentifierType type)
    {      
      Random Random = new Random();
      int RandomUniqueReference = Random.Next(0, 999999999);
      string UniqueReferenceNumber = RandomUniqueReference.ToString().PadLeft(9, '0');
      string CheckDigit = CheckDigitAlgorithm.Luhn.GetCheckDigit(_ValidIndustryCode + _ValidCountryCode + GetNumberIssuerCodeForHealthcareIdentifierType(type) + UniqueReferenceNumber);
      var oID = new NationalHealthcareIdentifier(_ValidIndustryCode + _ValidCountryCode + GetNumberIssuerCodeForHealthcareIdentifierType(type) + UniqueReferenceNumber + CheckDigit);     
      return oID.Value;
    }

    #endregion

    #region Public Static Properties
    /// <summary>
    /// Static properties for HL7 V2 Messages
    /// </summary>
    public static class Hl7V2
    {
      public static class Ihi
      {
        /// <summary>
        ///e.g: PID-3.4 (Ordering Provider)
        /// </summary>
        public static string AssigningAuthority { get { return "AUSHIC"; } }
        /// <summary>
        ///e.g: PID-3.5 (Ordering Provider)
        /// </summary>
        public static string IdentifierTypeCode { get { return "NI"; } }        
      }

      public static class Hpi_I
      {
                /// <summary>
                ///e.g: OBR-16.9 (Ordering Provider)
                /// </summary>
                public static string AssigningAuthority => "AUSHIC";         
                /// <summary>
                /// e.g OBR-16.14 (Ordering Provider)
                /// </summary>
                public static string AssigningFacility => "NPI";
            }

      public static class Hpi_O
      {
        /// <summary>
        ///e.g: ORC-21.7 (Ordering Facility Name)
        /// </summary>
        public static string IdentifierTypeCode { get { return "NOI"; } }
        /// <summary>
        /// e.g: ORC-21.8 (Ordering Facility Name)
        /// </summary>
        public static string AssigningFacilityID { get { return "AUSHIC"; } }
      }
    }

    /// <summary>
    /// Static properties for HL7 CDA Documents
    /// </summary>
    public static class Hl7Cda
    {
      public static class Ihi
      {
        /// <summary>
        ///root="1.2.36.1.2001.1003.0.8003608000094961" assigningAuthorityName="IHI"
        /// </summary>
        public static string AssigningAuthorityName { get { return "IHI"; } }

      }

      public static class Hpi_I
      {
        /// <summary>
        ///root="1.2.36.1.2001.1003.0.8003615833340784" assigningAuthorityName="HPI-I"
        /// </summary>
        public static string AssigningAuthorityName { get { return "HPI-I"; } }
      }

      public static class Hpi_O
      {
        /// <summary>
        ///root="1.2.36.1.2001.1003.0.8003623233356541" assigningAuthorityName="HPI-O"
        /// </summary>
        public static string AssigningAuthorityName { get { return "HPI-O"; } }
      }

    }

    /// <summary>
    /// Same root OID is used for (IHI, HPI-I & HPI-O)
    /// </summary>
    public static string RootHealthcareIdentifierOid { get { return "1.2.36.1.2001.1003.0"; } }
    
    #endregion

    #region Public Static Methods

    /// <summary>
    /// Checks that the value given is a valid Healthcare Identifier of the type given. 
    /// Only validates the syntax does not call the national web services 
    /// </summary>
    /// <param name="value"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    public static bool IsValid(string value, NationalHealthcareIdentifierType type)
    {
      var oId = new NationalHealthcareIdentifier(value);
      return oId.IsValid();
    }

    /// <summary>
    /// Generate the Healthcare Identifiers check digit 
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string GetCheckDigit(string value)
    {
      if (value.Length != 15)
        throw new FormatException("All National Healthcare Identifiers (IHI, HPI-I and HPI-O) with no check digit must be 15 digits in length, 16 once a check digit is added.");
      return CheckDigitAlgorithm.Luhn.GetCheckDigit(value);
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Convertes the HealthcareIdentifierType enum to the apropirate interger required for the Identifier value
    /// </summary>
    /// <param name="Type"></param>
    /// <returns></returns>
    private static string GetNumberIssuerCodeForHealthcareIdentifierType(NationalHealthcareIdentifierType Type)
    {
      switch (Type)
      {
        case NationalHealthcareIdentifierType.Individual:
          return "0";
        case NationalHealthcareIdentifierType.Provider:
          return "1";          
        case NationalHealthcareIdentifierType.Orginisation:
          return "2";
        case NationalHealthcareIdentifierType.None:
          return string.Empty;
        default:
          throw new InvalidEnumArgumentException(Type.ToString(), (int)Type, typeof(NationalHealthcareIdentifierType));
      }
    }

    /// <summary>
    /// Validates the identifer is valid for the given type.
    /// Type: None will always return false.
    /// </summary>
    /// <returns></returns>
    private bool IsValid(NationalHealthcareIdentifierType type, string Idneitfier)
    {
      if (String.IsNullOrWhiteSpace(IndustryCode))
      {
        _ValidatationErrorMessage = String.Format("The charaters 1-2 must be the Industry Code of '{0}'", _ValidIndustryCode);
        return false;
      }
      if (IndustryCode != _ValidIndustryCode)
      {
        _ValidatationErrorMessage = String.Format("The charaters 1-2 must be the Industry Code of '{0}'", _ValidIndustryCode);
        return false;
      }
      if (String.IsNullOrWhiteSpace(CountryCode))
      {
        _ValidatationErrorMessage = String.Format("The charaters 3-5 must be the Country Code of '{0}'", _ValidCountryCode);
        return false;
      }
      if (CountryCode != _ValidCountryCode)
      {
        _ValidatationErrorMessage = String.Format("The charaters 3-5 must be the Country Code of '{0}'", _ValidCountryCode);
        return false;
      }
      if (String.IsNullOrWhiteSpace(NumberIssuerCode))
      {
        _ValidatationErrorMessage = String.Format("The charaters 6-6 must be a Number Issuer Code of '0, 1 or 2' indicating a IHI, HPI-I or HPI-O ");
        return false;
      }
      if (type == NationalHealthcareIdentifierType.None)
      {
        _ValidatationErrorMessage = String.Format("The charaters 6-6 must be a Number Issuer Code of '0, 1 or 2' indicating a IHI, HPI-I or HPI-O ");
        return false;
      }
      if (type != NationalHealthcareIdentifierType.None)
      {
        if (type != this.HealthcareIdentifierType)
        {
          _ValidatationErrorMessage = String.Format("The charaters 6-6 must be a Number Issuer Code of '{0}' indicating the Healthcare Identifier Type of '{1}'", GetNumberIssuerCodeForHealthcareIdentifierType(this.HealthcareIdentifierType), this.HealthcareIdentifierType.ToString());
          return false;
        }
      }
      if (String.IsNullOrWhiteSpace(CheckDigit))
      {
        _ValidatationErrorMessage = String.Format("The charaters 16-16 must a single numeric check digit.");
        return false;
      }
      if (!String.IsNullOrWhiteSpace(UnknownContent))
      {
        _ValidatationErrorMessage = String.Format("The Healthcare Identifier must be exactly 16 digits long found extra 'UnknownContent' of '{0}'.", UnknownContent);
        return false;
      }
      if (!Support.StringSupport.IsDigitsOnly(this.Value))
      {
        _ValidatationErrorMessage = String.Format("The Healthcare Identifier must be only contain numeric digits [0-9].");
        return false;
      }
      if (!IsValidCheckDigit())
      {
        _ValidatationErrorMessage = String.Format("The Healthcare Identifier's check digit fails validation by the Luhn Algorithm.");
        return false;
      }
      //No errors found.
      _ValidatationErrorMessage = "The Healthcare Identifier is Valid.";
      return true;
    }


    #endregion
  }

}

