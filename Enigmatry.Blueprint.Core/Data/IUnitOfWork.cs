namespace Enigmatry.Blueprint.Core.Data
{
    public interface IUnitOfWork
    {
        void SaveChanges();
        void CancelSaving();
    }
}