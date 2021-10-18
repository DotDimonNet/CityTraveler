using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Repository.DbContext
{
    public class DbContext //: IDbContext
    {
        /*private readonly string _connectionString;
        private readonly IDbSyncManager _dbSync;

        public DbContext(string connectionString, IDbSyncManager dbSync)
        {
            _connectionString = connectionString;
            _dbSync = dbSync;
        }

        public async Task InitializeContext()
        {
            await _dbSync.CreateTables("dbo");
            /*await _dbSync.StartSync();

            if (_dbSync.IsFirst)
            {
                await _dbSync.CreateTables("dbo");
            }

            Trips = new DbCollection<ITrip>(_connectionString);
            Users = new DbCollection<IUser>(_connectionString);
            Routes = new DbCollection<IRoute>(_connectionString);
            Events = new DbCollection<IEvent>(_connectionString);
            Images = new DbCollection<IImage>(_connectionString);
            Prices = new DbCollection<IPrice>(_connectionString);
            Streets = new DbCollection<IStreet>(_connectionString);
            Ratings = new DbCollection<IRating>(_connectionString);
            Reviews = new DbCollection<IReview>(_connectionString);
            Comments = new DbCollection<IComment>(_connectionString);
            Addresses = new DbCollection<IAddress>(_connectionString);
            Landskapes = new DbCollection<ILandskape>(_connectionString);
            Institutions = new DbCollection<IInstitution>(_connectionString);

            if (_dbSync.IsSynchronized)
            {
                await DataLoadAsync();
            }
            else 
            {
                throw new ArgumentException("Database is not synchronized with current context.");
            }
        }

        private async Task DataLoadAsync()
        {
            await Trips.Load(nameof(Trips));
            await Users.Load(nameof(Users));
            await Routes.Load(nameof(Routes));
            await Events.Load(nameof(Events));
            await Images.Load(nameof(Images));
            await Prices.Load(nameof(Prices));
            await Streets.Load(nameof(Streets));
            await Ratings.Load(nameof(Ratings));
            await Reviews.Load(nameof(Reviews));
            await Comments.Load(nameof(Comments));
            await Addresses.Load(nameof(Addresses));
            await Landskapes.Load(nameof(Landskapes));
            await Institutions.Load(nameof(Institutions));
        }

        public IDbCollection<ITrip> Trips { get; set; }
        public IDbCollection<IUser> Users { get; set; }
        public IDbCollection<IPrice> Prices { get; set; }
        public IDbCollection<IRoute> Routes { get; set; }
        public IDbCollection<IEvent> Events { get; set; }
        public IDbCollection<IImage> Images { get; set; }
        public IDbCollection<IRating> Ratings { get; set; }
        public IDbCollection<IReview> Reviews { get; set; }
        public IDbCollection<IStreet> Streets { get; set; }
        public IDbCollection<IComment> Comments { get; set; }
        public IDbCollection<IAddress> Addresses { get; set; }
        public IDbCollection<ILandskape> Landskapes { get; set; }
        public IDbCollection<IInstitution> Institutions { get; set; }*/
    }
}
