namespace FoodApp.Models
{
	public class Receta
	{
		public int id { get; set; }
		public string nombre { get; set; }
		public string imagen { get; set; }
		public string categoria { get; set; }
		public string descripcion { get; set; }
		public string tiempo { get; set; }
		public string ingredientes { get; set; }
		public string pasos { get; set; }
		public int corazones { get; set; }
		public string corazon { get; set; }
	}
}
