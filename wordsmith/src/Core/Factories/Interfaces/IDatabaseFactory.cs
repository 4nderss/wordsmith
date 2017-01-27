using NPoco;

namespace WordSmith.Core.Factories.Interfaces
{
    public interface IDatabaseFactory {
        IDatabase GetDatabase();
    }
}
