using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace authApi.Models
{
    public class User
    {
		[Key] [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Guid id { get; set; }

		[Required]
		public string userName { get; set; }

		[Required]
		public string fullName { get; set; }

		[Required]
		public string email { get; set; }

		[Required]
		public string password { get; set; }

		public string imageUrl { get; set; }

		[Required]
		public bool isVerified { get; set; }

		public string token { get; set; }
	}
}
