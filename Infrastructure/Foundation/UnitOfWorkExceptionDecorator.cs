using Domain.Foundation;
using Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace Infrastructure.Foundation
{
    public class UnitOfWorkExceptionDecorator : IUnitOfWork
    {
        private readonly IUnitOfWork _unitOfWork;

        public UnitOfWorkExceptionDecorator( IUnitOfWork unitOfWork )
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CommitAsync()
        {
            try
            {
                await _unitOfWork.CommitAsync();
            }
            catch ( DbUpdateConcurrencyException ex )
            {
                throw new ConcurrencyConflictException( "Resource was modified by another process.", ex );
            }
            catch ( DbUpdateException ex ) when ( IsUniqueConstraintViolation( ex ) )
            {
                throw new UniqueConstraintViolationException( "Duplicate value violates unique constraint.", ex );
            }
            catch ( DbUpdateException ex ) when ( IsDatabaseUnavailable( ex ) )
            {
                throw new DatabaseUnavailableException( "Database is unavailable.", ex );
            }
            catch ( Exception ex )
            {
                throw new Exception( "Unexpected infrastructure error occurred.", ex );
            }
        }


        private static bool IsUniqueConstraintViolation( DbUpdateException ex )
        {
            return ex.InnerException is MySqlException { Number: 1062 };
        }

        private static bool IsDatabaseUnavailable( DbUpdateException ex )
        {
            return ex.InnerException is MySqlException { Number: 1049 or 1045 or 2002 or 2003 };
        }
    }
}