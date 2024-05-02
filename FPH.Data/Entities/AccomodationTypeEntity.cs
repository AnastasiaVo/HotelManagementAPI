namespace FPH.Data.Entities
{
    public class AccomodationTypeEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int NumberOfBeds { get; set; }

        public virtual List<HotelRoomEntity> HotelRoomEntities { get; set; } = new();
    }
}
