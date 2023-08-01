using System;
using Microsoft.AspNetCore.Identity;

namespace Mercury.Shared.Models.Mercury
{
	public partial class Person
	{
		public Person(IdentityUser appuser)
		{
			Emails.Add(new Email(){ EmailAddress = appuser.Email, IsDefault = true});
			AppUserID = appuser.Id;
			Culture = "en";
			
		}
	}
}

