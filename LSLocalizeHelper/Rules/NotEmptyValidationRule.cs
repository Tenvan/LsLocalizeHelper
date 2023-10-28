using System.Globalization;
using System.Windows.Controls;

namespace LSLocalizeHelper.Rules;

public class NotEmptyValidationRule : ValidationRule
{
    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        return string.IsNullOrWhiteSpace((value ?? "").ToString())
            ? new ValidationResult(false, "Wert wird benötigt.")
            : ValidationResult.ValidResult;
    }
}
