using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Identifiers.Australian
{
  public class MedicareNumber
  {
    /// <summary>
    /// The whole Medicare Number value
    /// </summary>
    public string Value { get; set; }

    /// <summary>
    /// 8-digits in total, the first digit should be in the range 2-6
    /// </summary>
    public string Identifer {get; set; }

    /// <summary>
    /// Checksum Digits are weighted (1,3,7,9,1,3,7,9)
    /// </summary>
    public string Checksum { get; set; }

    /// <summary>
    /// Indicates how many times the card has been issued (1-digit)
    /// </summary>
    public string IssueNumber { get; set; }

    /// <summary>
    /// The IRN appears on the left of the cardholder's name on the medicare card 
    /// and distinguishes the individuals named on the card.
    /// </summary>
    public string IRN { get; set; }

    /// <summary>
    /// The expiry date of the Medicare Card
    /// Defaults to DateTime.MinValue if not set
    /// </summary>
    public DateTime Expiry { get; set; }
    
    /// <summary>
    /// Validates the Medicare Number by calculating it's check digit
    /// returns true if check digit is correct  
    /// </summary>
    public bool IsValidOld()
    {
      if (!string.IsNullOrEmpty(this.Value))
      {
        if (this.Value.Length == 10)
        {
          if (Regex.IsMatch(this.Value, @"^\d+$"))
          {
            int One = Convert.ToInt32(this.Value.Substring(0, 1));
            //The first Char must be in the range 2-6
            if (One > 1 && One < 7)
            {
              int Two = Convert.ToInt32(this.Value.Substring(1, 1));
              int Three = Convert.ToInt32(this.Value.Substring(2, 1));
              int Four = Convert.ToInt32(this.Value.Substring(3, 1));
              int Five = Convert.ToInt32(this.Value.Substring(4, 1));
              int Six = Convert.ToInt32(this.Value.Substring(5, 1));
              int Seven = Convert.ToInt32(this.Value.Substring(6, 1));
              int Eight = Convert.ToInt32(this.Value.Substring(7, 1));
              int Nine = Convert.ToInt32(this.Value.Substring(8, 1));
              int CheckDigit = ((One) + (Two * 3) + (Three * 7) + (Four * 9) + (Five) + (Six * 3) + (Seven * 7) + (Eight * 9));
              CheckDigit = CheckDigit % 10;
              if (Nine == CheckDigit)
              {
                int TempInt;
                if (!string.IsNullOrWhiteSpace(this.IRN))
                {
                  if (this.IRN.Length == 1 && int.TryParse(this.IRN, out TempInt))
                  {
                    return true;
                  }
                  else
                  {
                    return false;
                  }
                }
                else
                {
                  return true;
                }               
              }                
            }
          }
        }
      }
      return false;
    }

    public bool IsValid()
    {
      if (string.IsNullOrEmpty(this.Value))
      {
        return false;
      }
      if (!Regex.IsMatch(this.Value, @"^\d+$"))
      {
        return false;
      }
      int TempInteger;
      if (int.TryParse(this.Identifer.Substring(0, 1), out TempInteger))
      {
        if (TempInteger < 2 )        
          return false;
        
        if (TempInteger > 6)
          return false;
      }
      if (this.Checksum != GenerateCheckDigit(this.Identifer))
        return false;
      if (this.IssueNumber.Length != 1)
        return false;

      if (this.IRN != string.Empty)
      {
        if (!Regex.IsMatch(this.IRN, @"^\d+$"))
        {
          return false;
        }
        if (this.IRN.Length > 1)
          return false;
      }

      return true;
      
    }

    /// <summary>
    /// MedicareNumber Constructor
    /// </summary>
    public MedicareNumber()
    {
      this.Expiry = DateTime.MinValue;
    }
    /// <summary>
    /// MedicareNumber Constructor
    /// </summary>
    /// <param name="Value"></param>
    /// <param name="Irn"></param>
    public MedicareNumber(string Value)
    {
      this.Value = Value;
      if (Value.Length >= 8)
        this.Identifer = Value.Substring(0, 8);
      if (Value.Length >= 9)
        this.Checksum = Value.Substring(8, 1);
      if (Value.Length >= 10)
        this.IssueNumber = Value.Substring(9, 1);
      this.IRN = string.Empty;
      this.Expiry = DateTime.MinValue;
    }    
    /// <summary>
    /// MedicareNumber Constructor
    /// </summary>
    /// <param name="Value"></param>
    /// <param name="Irn"></param>
    /// <param name="Type"></param>
    /// <param name="AssigningAuthority"></param>
    public MedicareNumber(string Value, string Irn)
    {
      this.Value = Value;
      if (Value.Length >= 8)
        this.Identifer = Value.Substring(0, 8);
      if (Value.Length >= 9)
        this.Checksum = Value.Substring(8, 1);
      if (Value.Length >= 10)
        this.IssueNumber = Value.Substring(9, 1);
      this.IRN = Irn;
      this.Expiry = DateTime.MinValue;
    }
    /// <summary>
    /// MedicareNumber Constructor
    /// </summary>
    /// <param name="Value"></param>
    /// <param name="Irn"></param>
    /// <param name="Expiry"></param>
    public MedicareNumber(string Value, string Irn, DateTime Expiry)
    {
      this.Value = Value;
      if (Value.Length >= 8)
        this.Identifer = Value.Substring(0, 8);
      if (Value.Length >= 9)
        this.Checksum = Value.Substring(8, 1);
      if (Value.Length >= 10)
        this.IssueNumber = Value.Substring(9, 1);
      this.IRN = Irn;
      this.Expiry = Expiry;
    }
    /// <summary>
    /// Validate a Medicare Number
    /// </summary>
    /// <param name="MedicareNumber"></param>
    /// <returns></returns>
    public static bool IsValid(string MedicareNumber)
    {
      MedicareNumber Id = null;
      string Number = string.Empty;
      string Irn = string.Empty;
      if (MedicareNumber.Length == 11)
      {
        Number = MedicareNumber.Substring(0, 10);
        Irn = MedicareNumber.Substring(10, 1);
        Id = new MedicareNumber(Number, Irn);
        return Id.IsValid();
      }
      else if (MedicareNumber.Length == 10)
      {
        Id = new MedicareNumber(MedicareNumber);
        return Id.IsValid();
      }
      else
      {
        return false;
      }
    }
   

    private static string GenerateCheckDigit(string Item)
    {
      if (!string.IsNullOrEmpty(Item))
      {
        if (Item.Length == 8)
        {
          if (Regex.IsMatch(Item, @"^\d+$"))
          {
            int One = Convert.ToInt32(Item.Substring(0, 1));
            //The first Char must be in the range 2-6
            if (One > 1 && One < 7)
            {
              int Two = Convert.ToInt32(Item.Substring(1, 1));
              int Three = Convert.ToInt32(Item.Substring(2, 1));
              int Four = Convert.ToInt32(Item.Substring(3, 1));
              int Five = Convert.ToInt32(Item.Substring(4, 1));
              int Six = Convert.ToInt32(Item.Substring(5, 1));
              int Seven = Convert.ToInt32(Item.Substring(6, 1));
              int Eight = Convert.ToInt32(Item.Substring(7, 1));
              
              int CheckDigit = ((One) + (Two * 3) + (Three * 7) + (Four * 9) + (Five) + (Six * 3) + (Seven * 7) + (Eight * 9));
              CheckDigit = CheckDigit % 10;
              return CheckDigit.ToString();
            }
          }
        }
      }
      throw new FormatException("Can not create Checkdigit for given value. Must be 8 digits and the first digit between 2-6.");
    }
    #region Public Static Properties



    public static string GenerateRandomMedicareNumber(bool WithIRN)
    {
      Random Random = new Random();
      int FirstChar = Random.Next(2, 6);
      int Number = Random.Next(0, 9999999);
      string IdNumber = (FirstChar.ToString() + Number.ToString().PadLeft(7, '0'));
      string Checkdigit = GenerateCheckDigit(FirstChar.ToString() + Number.ToString().PadLeft(7, '0'));
      int IssueNum = Random.Next(0, 9);
      if (WithIRN)
      {
        return $"{IdNumber}{Checkdigit}{IssueNum}{Random.Next(1, 9)}";
      }
      else
      { 
        return $"{IdNumber}{Checkdigit}{IssueNum}";
      }      
    }
    /// <summary>
    /// Static properties for HL7 V2 Messages
    /// </summary>
    public static class Hl7V2
    {
      /// <summary>
      ///e.g: PID-3.4 (Ordering Provider)
      /// </summary>
      public static string AssigningAuthority { get { return "AUSHIC"; } }
      /// <summary>
      ///e.g: PID-3.5 (Ordering Provider)
      /// </summary>
      public static string IdentifierTypeCode { get { return "MC"; } }
    }

    #endregion
  }
}
