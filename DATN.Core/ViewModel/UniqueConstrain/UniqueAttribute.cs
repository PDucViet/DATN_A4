using DATN.Core.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.ViewModel.UniqueConstrain
{
    public class UniqueAttribute<T> : ValidationAttribute where T : class
    {
        private readonly string _propertyName;


        public UniqueAttribute(string propertyName)
        {
            _propertyName = propertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult($"{_propertyName} is required.");
            }

            var dbContext = validationContext.GetService(typeof(DATNDbContext)) as DATNDbContext;
            if (dbContext == null)
            {
                throw new ArgumentException("DbContext is not available");
            }

            var entity = dbContext.Set<T>().AsQueryable()
                .FirstOrDefault(e => EF.Property<string>(e, _propertyName) == value.ToString());

            if (entity != null)
            {
                return new ValidationResult($"The {_propertyName} must be unique.");
            }

            return ValidationResult.Success;
        }
    }
}
