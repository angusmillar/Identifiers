using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Identifiers.Australian
{
  public class MedicareProviderNumber
  {
    //The Allowed Practice Location Values (PLV) and there positions
    private static char[] PracticeLocationValueChars = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
                                             'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K',
                                             'L', 'M', 'N', 'P', 'Q', 'R', 'T', 'U', 'V', 'W',
                                             'X', 'Y'};

    //The Allowed Check Characters and there positions
    private static char[] RemainderCheckCharacterChars = { 'Y', 'X', 'W', 'T', 'L', 'K', 'J', 'H', 'F', 'B', 'A' };

    public string Value { get; set; }
    /// <summary>
    /// The six digit Stem portion on the Medicare Provider Number
    /// </summary>
    public string Stem
    {
      get
      {
        if (this.IsValid())
          return this.Value.Substring(0, 6);
        else
          return string.Empty;
      }
    }
    /// <summary>
    /// The Location Character of the Medicare Provider Number
    /// </summary>
    public string LocationCharacter
    {
      get
      {
        if (this.IsValid())
          return this.Value.Substring(6, 1);
        else
          return string.Empty;
      }
    }
    /// <summary>
    /// The Check Character of the Medicare Provider Number
    /// </summary>
    public string CheckCharacter
    {
      get
      {
        if (this.IsValid())
          return this.Value.Substring(7, 1);
        else
          return string.Empty;
      }
    }
    /// <summary>
    /// Validate the format and Check Character of the Medicare Provider Number
    /// </summary>
    public bool IsValid()
    {
      if (this.Value.Length == 8)
      {
        if (Regex.IsMatch(this.Value.Substring(0, 6), @"^\d+$"))
        {
          int PLVInteger = Array.IndexOf(PracticeLocationValueChars, this.Value.Substring(6, 1).ToCharArray()[0]);
          if (PLVInteger > -1)
          {
            int One = Convert.ToInt32(this.Value.Substring(0, 1));
            int Two = Convert.ToInt32(this.Value.Substring(1, 1));
            int Three = Convert.ToInt32(this.Value.Substring(2, 1));
            int Four = Convert.ToInt32(this.Value.Substring(3, 1));
            int Five = Convert.ToInt32(this.Value.Substring(4, 1));
            int Six = Convert.ToInt32(this.Value.Substring(5, 1));
            int CheckDigit = ((One * 3) + (Two * 5) + (Three * 8) + (Four * 4) + (Five * 2) + (Six) + (PLVInteger * 6)) % 11;
            if (this.Value.Substring(7, 1) == RemainderCheckCharacterChars[CheckDigit].ToString())
              return true;
          }
        }
      }
      return false;
    }
    /// <summary>
    /// Validate the format and Check Character of the Medicare Provider Number
    /// </summary>
    /// <param name="MedicareProviderNumber"></param>
    /// <returns></returns>
    public static bool IsValid(string MedicareProviderNumber)
    {
      var ProviderNumber = new MedicareProviderNumber();
      ProviderNumber.Value = MedicareProviderNumber;
      return ProviderNumber.IsValid();
    }
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="MedicareProviderNumber"></param>
    public MedicareProviderNumber()
    {
    }
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="MedicareProviderNumber"></param>
    public MedicareProviderNumber(string MedicareProviderNumber)
    {
      this.Value = MedicareProviderNumber;
    }

    #region Public Static Properties

    public static string GenerateRandomMedicareProviderNumber()
    {
      Random Random = new Random();
      string Stem = Random.Next(0, 999999).ToString().PadLeft(6, '0'); ;
      int PLVInteger = Random.Next(0, 31);
      char PLVChar = PracticeLocationValueChars[PLVInteger];
      int One = Convert.ToInt32(Stem.Substring(0, 1));
      int Two = Convert.ToInt32(Stem.Substring(1, 1));
      int Three = Convert.ToInt32(Stem.Substring(2, 1));
      int Four = Convert.ToInt32(Stem.Substring(3, 1));
      int Five = Convert.ToInt32(Stem.Substring(4, 1));
      int Six = Convert.ToInt32(Stem.Substring(5, 1));
      int CheckDigit = ((One * 3) + (Two * 5) + (Three * 8) + (Four * 4) + (Five * 2) + (Six) + (PLVInteger * 6)) % 11;
      return Stem + PLVChar + RemainderCheckCharacterChars[CheckDigit].ToString();
    }

    /// <summary>
    /// Static properties for HL7 V2 Messages
    /// </summary>
    public static class Hl7V2
    {
      /// <summary>
      ///e.g: ORC-16.9.1 (Ordering Provider)
      /// </summary>
      public static string AssigningAuthority { get { return "AUSHICPR"; } }
    }

    #endregion
  }
}
