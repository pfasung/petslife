using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace PetsLife.Models
{
    [DataContract]
    public class PetApproval
    {
        [Key]
        [DataMember]
        public int Id { get; set; }

        [Required]
        [DataMember]
        public int PetId { get; set; }

        [ForeignKey("PetId")]
        public virtual Pet Pet { get; set; }

        [Required]
        [DataMember]
        public int WalkerId { get; set; }

        [ForeignKey("WalkerId")]
        public virtual PetWalker Walker { get; set; }

        [MaxLength(200)]
        [DataMember]
        public string Occurence { get; set; }

        [Required]
        [DataMember]
        public DateTime DateApproved { get; set; }
    }
}