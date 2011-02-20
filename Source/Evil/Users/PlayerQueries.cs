using System.Linq;

namespace Evil.Users
{
    public static class PlayerQueries
    {
        public static Player CurrentPlayerFor(this IQueryable<Player> query, Account account)
        {
            return query.FirstOrDefault(m => m.Account == account);
        }
    }
}