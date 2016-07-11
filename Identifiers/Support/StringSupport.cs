﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Identifiers.Support
{
  public static class StringSupport
  {
    public static bool IsDigitsOnly(string str)
    {
      foreach (char c in str)
      {
        if (c < '0' || c > '9')
          return false;
      }
      return true;
    }
  }
}