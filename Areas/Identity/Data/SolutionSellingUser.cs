using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace SolutionSelling.Areas.Identity.Data;

// ADD FIRST AND LAST NAME TO USER
public class SolutionSellingUser : IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}

