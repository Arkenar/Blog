namespace Blog.DataAccess.Interfaces;

public interface IPhoneValidation
{
    public void PhoneValidAndFormatted(string prefix, string number, out string validPrefix, out string validNbr);
}