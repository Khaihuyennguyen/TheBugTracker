using Microsoft.EntityFrameworkCore;
using TheBugTracker.Data;

namespace TheBugTracker.Helpers
{
    public static class DataHelper
    {
        public static async Task ManageDataAsync(IServiceProvider svcProvider)
        {
            var dbContextScv = svcProvider.GetRequiredService<ApplicationDbContext>();
            await dbContextScv.Database.MigrateAsync(); // means updating database
        }
    }
}
