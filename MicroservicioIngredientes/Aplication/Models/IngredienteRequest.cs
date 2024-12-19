namespace Application.Models
{
    public class IngredienteRequest
    {
        public int Id { get; set; }
        public int TipoIngredienteID { get; set; }
        public int TipoMedidaID { get; set; }
        public string Nombre { get; set; }
    }
}
