using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace PetsLife.Models
{
    [DataContract]
    public class PetWalker
    {
        [Key]
        [DataMember]
        public int Id { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        [MinLength(2), MaxLength(20)]
        [DataMember]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [MinLength(2), MaxLength(20)]
        [DataMember]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email address is required")]
        [DataType(DataType.EmailAddress)]
        [DataMember]
        public string EmailAddress { get; set; }

        [DataType(DataType.PhoneNumber)]
        [DataMember]
        public string PhoneNumber { get; set; }

        [InverseProperty("Walker")]
        public virtual ICollection<PetApproval> Approvals { get; set; }
    }
}