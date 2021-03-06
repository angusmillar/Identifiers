﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Identifiers.Australian.NationalHealthcareIdentifier
{
  public class HealthcareProviderIdentifierOrganisationGenerator : IHealthcareProviderIdentifierOrganisationGenerator
  {
    public string Generate()
    {
      return NationalHealthcareIdentifierGenerator.GenerateRandomHealthcareIdentifier(NationalHealthcareIdentifierParser.NationalHealthcareIdentifierType.Orginisation);
    }
  }
}
