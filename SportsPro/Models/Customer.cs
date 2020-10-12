using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace SportsPro.Models
{
	public class Customer
	{
		public int CustomerID { get; set; }

		[Required(ErrorMessage = "Please enter your first name.")]
		[MinLength(1), MaxLength(50)]
		public string FirstName { get; set; }

		[Required(ErrorMessage = "Please enter your last name.")]
		[MinLength(1), MaxLength(50)]
		public string LastName { get; set; }

		[Required(ErrorMessage = "Please enter your address.")]
		[MinLength(1), MaxLength(50)]
		public string Address { get; set; }

		[Required(ErrorMessage = "Please enter your city.")]
		[MinLength(1), MaxLength(50)]
		public string City { get; set; }

		[Required(ErrorMessage = "Please enter your state.")]
		[MinLength(1), MaxLength(50)]
		public string State { get; set; }

		[Required(ErrorMessage = "Please enter your postal code.")]
		[RegularExpression(@"^\d$")]
		[StringLength(20)]
		[MinLength(1), MaxLength(20)]
		public string PostalCode { get; set; }

		[Required]
		public string CountryID { get; set; }
		public Country Country { get; set; }

		[Required(ErrorMessage = "Phone number must be in (999)999-9999 format")]
		[RegularExpression(@"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}")]
		public string Phone { get; set; }

		[Required(ErrorMessage = "Please enter a valid email address.")]
		[MinLength(1), MaxLength(50)]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }
		public ICollection<Registration> Registrations { get; set; }

		public string FullName => FirstName + " " + LastName;   // read-only property
	}
}