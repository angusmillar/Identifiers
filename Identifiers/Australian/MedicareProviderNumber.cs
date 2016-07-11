﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Identifiers.Australian
{
  public class MedicareProviderNumber
  {
    //The Allowed Practice Location Values (PLV) and there positions
    private char[] PracticeLocationValueChars = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 
                                             'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 
                                             'L', 'M', 'N', 'P', 'Q', 'R', 'T', 'U', 'V', 'W', 
                                             'X', 'Y'};
    
    //The Allowed Check Characters and there positions
    private char[] RemainderCheckCharacterChars = { 'Y', 'X', 'W', 'T', 'L', 'K', 'J', 'H', 'F', 'B', 'A' };
    
    public string Value { get; set; }
    /// <summary>
    /// The six digit Stem portion on the Medicare Provider Number
    /// </summary>
    public string Stem
    {
      get
      {
        if (this.IsValid)
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
        if (this.IsValid)
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
        if (this.IsValid)
          return this.Value.Substring(7, 1);
        else
          return string.Empty;
      }
    }
    /// <summary>
    /// Validate the format and Check Character of the Medicare Provider Number
    /// </summary>
    public bool IsValid
    {
      get
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
    }                                           
  }
}