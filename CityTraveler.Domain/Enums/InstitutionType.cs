namespace CityTraveler.Domain.Enums
{
    public class InstitutionType : Enumeration
    {
        public static readonly InstitutionType Restourant = new(1, "Restourant");
        public static readonly InstitutionType Museum = new(2, "Museum");
        public static readonly InstitutionType Cafe = new(3, "Cafe");
        public static readonly InstitutionType Hotel = new(4, "Hotel");
        public static readonly InstitutionType Theater = new(5, "Theater");
        public static readonly InstitutionType Hostel = new(6, "Hostel");
        public static readonly InstitutionType Cinema = new(7, "Cinema");
        public static readonly InstitutionType Shop = new(8, "Shop");
        public static readonly InstitutionType Supermarket = new(9, "Supermarket");
        public static readonly InstitutionType TransportStation = new(10, "TransportStation");

        protected InstitutionType(int id, string name) : base(id, name) { }
    }
}
