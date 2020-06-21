namespace Api.models
{
    public class Image
    {
        public int Id { get; set; }
        public string ImageURL { get; set; }
        public int VehicleId { get; set; }  //meka thmai fk eka vihicle eke

    }
}