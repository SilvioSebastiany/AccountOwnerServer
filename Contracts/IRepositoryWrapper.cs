namespace Contracts;

public interface IRepositoryWrapper
{
    IAccountRepository Account { get; }
    IOwnerRepository Owner { get; }
    void Save();
}
