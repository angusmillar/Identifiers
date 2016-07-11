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
    /// The Individual Reference Number
    /// It identifies each person on the card
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
    public bool IsValid
    {
      get
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
                  return true;
              }
            }
          }
        }
        return false;
      }
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
    /// <param name="Type"></param>
    /// <param name="AssigningAuthority"></param>
    public MedicareNumber(string Value, string Irn)
    {
      this.Value = Value;
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
      this.IRN = Irn;
      this.Expiry = Expiry;
    }
  }
}
