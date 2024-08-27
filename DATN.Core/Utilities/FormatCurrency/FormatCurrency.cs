using System.Globalization;

namespace DATN.Core.Utitlities.FormatCurrency;

public class FormatCurrency
{

    public string GetCurrency(decimal value, string culture="vi-VN")
    {
        CultureInfo vietnameseCulture = new CultureInfo(culture);

        string formattedCurrency = string.Format(vietnameseCulture, "{0:C}", value);

        return formattedCurrency;
    }
}