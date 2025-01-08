using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace InventoryManagement.Validation
{
    public class NumericValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string input = value as string;
            if (!int.TryParse(input, out _))
            {
                return new ValidationResult(false, "This field must be a valid number.");
            }
            return ValidationResult.ValidResult;
        }
    }
}
