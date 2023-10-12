namespace PetsServer.Locality.Model
{
    public class LocalityModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public LocalityModel(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}