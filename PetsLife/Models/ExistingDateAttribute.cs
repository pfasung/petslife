using System;
using System.ComponentModel.DataAnnotations;

namespace PetsLife.Models
{
    public class ExistingDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime d = Convert.ToDateTime(value);
            return d <= DateTime.Now;

        }
    }
}