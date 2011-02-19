using System.Linq;

namespace Evil.Users
{
    public static class AccountQueries
    {
        public static Account ByEmailAddress(this IQueryable<Account> query, string emailAddress)
        {
            return query.FirstOrDefault(m => m.EmailAddress == emailAddress);
        }
    }
}
