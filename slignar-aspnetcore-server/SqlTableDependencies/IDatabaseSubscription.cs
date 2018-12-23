using System;

namespace slignaraspnetcoreserver.SqlTableDependencies
{
    public interface IDatabaseSubscription
    {
        void Configure(string connectionString);
    }
}
