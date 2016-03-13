using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace PetsLife.Models
{
    [DataContract]
    public class Pet
    {
        [Key]
        [DataMember]
        public int Id { get; set; }

        [Required(ErrorMessage = "Pet Name is required")]
        [MinLength(4), MaxLength(20)]
        [DataMember]
        public string Name { get; set; }

        [Required(ErrorMessage = "Date of birth is required")]
        [DataType(DataType.Date)]
        [ExistingDate(ErrorMessage = "Date of birth must be existing date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataMember]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Pet Owner is required")]
        [DataMember]
        public int OwnerId { get; set; }


        [ForeignKey("OwnerId")]
        public virtual PetOwner Owner { get; set; }

        [InverseProperty("Pet")]
        [DataMember]
        public virtual ICollection<PetApproval> Approvals { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [DataMember]
        public int Age { get { return GetAge(BirthDate); } }

        private static int GetAge(DateTime birthDate)
        {
            var today = DateTime.Today;
            int age = today.Year - birthDate.Year;

            if (birthDate > today.AddYears(-age))
                age--;

            return age;
        }
    }
}