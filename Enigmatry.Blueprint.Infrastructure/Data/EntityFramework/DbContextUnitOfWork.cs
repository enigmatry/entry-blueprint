using Enigmatry.Blueprint.Core.Data;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace Enigmatry.Blueprint.Infrastructure.Data.EntityFramework
{
    //TODO cleanup
    [UsedImplicitly]
    public class DbContextUnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;
        private bool _cancelSaving;

        public DbContextUnitOfWork(DbContext context)
        {
            _context = context;
        }

        public void SaveChanges()
        {
            if (_cancelSaving)
            {
             //   _log.Warn("Not saving database changes since saving was cancelled.");
                return;
            }
            /*try
            {*/
                var numberOfChanges = _context.SaveChanges();
               // _log.DebugFormat("{0} of changed were saved to database {1}", numberOfChanges,
                 //   _context.Database.Connection.Database);
            /*}
            catch (DbEntityValidationException ex)
            {
                ProcessEntityValidationException(ex);
            }*/
        }

        public void CancelSaving()
        {
            _cancelSaving = true;
        }

        /*private void ProcessEntityValidationException(DbEntityValidationException ex)
        {
            // Retrieve the error messages as a list of strings.
            var errorMessages = ex.EntityValidationErrors
                .SelectMany(x => x.ValidationErrors)
                .Select(x => x.ErrorMessage);

            // Join the list to a single string.
            var fullErrorMessage = string.Join("; ", errorMessages);

            // Combine the original exception message with the new one.
            var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);
            _log.ErrorFormat("Message:\n{0}", exceptionMessage);
            // Throw a new DbEntityValidationException with the improved exception message.
            throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
        }*/
    }
}