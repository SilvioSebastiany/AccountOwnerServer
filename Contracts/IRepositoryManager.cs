namespace Contracts;

public interface IRepositoryManager
{
    IAccountRepository Account { get; }
    IOwnerRepository Owner { get; }
    void Save();
}