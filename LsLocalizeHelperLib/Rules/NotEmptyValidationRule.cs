using System.Globalization;
using System.Windows.Controls;

namespace LsLocalizeHelperLib.Rules;

public class NotEmptyValidationRule : ValidationRule
{
    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        return string.IsNullOrWhiteSpace((value ?? "").ToString())
            ? new ValidationResult(isValid: false, errorContent: "Wert wird benötigt.")
            : ValidationResult.ValidResult;
    }
}
