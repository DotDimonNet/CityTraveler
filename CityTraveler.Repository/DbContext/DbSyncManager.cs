using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Repository.DbContext
{
    /*public class DbSyncManager : IDbSyncManager
    {
        public bool IsSynchronized => SyncTables.All(x => x.IsSynchronized);

        public bool IsFirst { get; set; }
        //private IDbRequestManager<IDbData> RequestManager { get; set; }
        private IEnumerable<ISyncModel> SyncTables { get; set; } = new List<SyncModel>();

        public DbSyncManager(string connectionString)
        {
            //RequestManager = new DbRequestManager<IDbData>(connectionString);
            var tables = new List<SyncModel>();
            tables.Add(new SyncModel()
            {
                TableName = nameof(IDbContext.Trips),
                FieldsToSync = new List<string>() { }
            });

            SyncTables = tables;
        }

        public async Task CreateTables(string schema)
        {
            var query = GenerateTableScript(schema, nameof(IDbContext.Trips), new List<(string Name, string Type, string isNullable)>()
            {
                (nameof(ITrip.TripStart), "datetime", "not null"),
                (nameof(ITrip.TripEnd), "datetime", "not null"),
                (nameof(ITrip.Owner), "varchar(64)", "not null"),
                (nameof(ITrip.Id), "uniqueidentifier PRIMARY KEY", "not null"),
                (nameof(ITrip.Created), "datetime", "not null"),
                (nameof(ITrip.Modified), "datetime", "not null"),
                (nameof(ITrip.TemplateId), "varchar(64)", "not null"),
                (nameof(ITrip.Price), "varchar(64)", "not null"),
                (nameof(ITrip.Title), "varchar(64)", "not null"),
                (nameof(ITrip.Description), "varchar(64)", "not null"),
                (nameof(ITrip.AverageRating), "real", "not null"),
            });

            await RequestManager.SendRequestAsync(query, null, false);
        }

        private string GenerateTableScript(string schema, string tableName, List<(string Name, string Type, string isNullable)> fields)
        {
            return $@"
                IF NOT EXISTS ( 
                    SELECT * FROM sys.tables t 
                    JOIN sys.schemas s ON (t.schema_id = s.schema_id) 
                    WHERE s.name = '{schema}' and t.name = '{tableName}'
                ) 	CREATE TABLE {schema}.{tableName} (         
                        {string.Join(",\n", fields.Select(x => $"{x.Name} {x.Type} {x.isNullable}"))} 
                )
                ";
        }

        public void SetSource(IEnumerable<ISyncModel> tablesToSync, IDbRequestManager<IDbData> requestManager)
        {
            RequestManager = requestManager;
            SyncTables = tablesToSync;
        }

        public async Task StartSync()
        {
            var checkQuery = $@"";

            var tables = await RequestManager.CommonQueryAsync(checkQuery, null, false);

            if (!tables.Any())
            {
                IsFirst = true;
                return;
            }

            var query = $@"";
            var result = await RequestManager.CommonQueryAsync(query, null, false);
            SyncTables = result.Select(x =>
                new SyncModel
                {
                    ExtraFields = x.ExtraFields,
                    FieldsToSync = x.FieldsToSync,
                    IsSynchronized = x.IsSynchronized,
                    TableName = x.TableName,
                    MissedFields = x.MissedFields
                });
        }
    }*/
}
