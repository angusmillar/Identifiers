﻿namespace Identifiers.Australian.MedicareNumber
{
  public interface IMedicareMedicareNumberGenerator
  {
    string Generate(bool WithIRN = false);
  }
}