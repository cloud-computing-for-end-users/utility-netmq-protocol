using custom_message_based_implementation.model;

namespace custom_message_based_implementation.interfaces
{
    public interface IDatabaseServermodule
    {
        PrimaryKey Login(Email email, Password password);
        PrimaryKey CreateAccount(Email email, Password password);
    }
}