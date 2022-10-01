using Ardalis.GuardClauses;
using Blog.DataAccess.Interfaces;
using PhoneNumbers;

namespace Blog.DataAccess.Services;

public class PhoneValidation : IPhoneValidation
{
    public void PhoneValidAndFormatted(string prefix, string number, out string validPrefix, out string validNbr)
    {
        Guard.Against.NullOrWhiteSpace(number, nameof(number));
        Guard.Against.NullOrWhiteSpace(prefix, nameof(prefix));
        
        var sanitized = number.StartsWith("0") ? number.Substring(1).Replace(" ", "") : number.Replace(" ", "");
        var formatted = prefix + sanitized;

        var utils = PhoneNumberUtil.GetInstance();
        var proto = utils.Parse(formatted, "ZZ");

        Guard.Against.Null(proto, nameof(proto));
        validPrefix = proto.CountryCode.ToString();
        validNbr = utils.Format(proto, PhoneNumberFormat.INTERNATIONAL);
    }
}