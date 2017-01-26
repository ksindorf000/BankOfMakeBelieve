using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankOfMakeBelieve.MethodClasses
{
    class CreateNewUser
    {
        // --------------- Use to check if entered username is unique ------------------
        // https://www.simple-talk.com/dotnet/net-framework/catching-bad-data-in-entity-framework/
        
        //    public class MyDbContext : DbContext
        //    {
        //        public DbSet<Tag> Tags { get; set; }
        //        //...etc.
        //        protected override DbEntityValidationResult ValidateEntity(DbEntityEntry entityEntry,
        //            IDictionary<object, object> items)
        //        {

        //            if (entityEntry.Entity is Tag &&
        //                        (entityEntry.State == EntityState.Added
        //                          || entityEntry.State == EntityState.Modified))
        //            {
        //                var tagToCheck = ((Tag)entityEntry.Entity);

        //                //check for uniqueness of Tag's Slug 
        //                if (Tags.Any(x => x.TagId != tagToCheck.TagId && x.Slug == tagToCheck.Slug))
        //                    return
        //                           new DbEntityValidationResult(entityEntry,
        //                         new List<DbValidationError>
        //                             {
        //                         new DbValidationError( "Slug",
        //                             string.Format( "The Slug on tag '{0}' must be unique.", tagToCheck.Name))
        //                             });
        //            }

        //            return base.ValidateEntity(entityEntry, items);
        //        }
    }
}
