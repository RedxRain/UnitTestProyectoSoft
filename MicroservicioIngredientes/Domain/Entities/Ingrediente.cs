namespace Domain.Entities
{
    public class Ingrediente
    {
        public int Id { get; set; }
        public int TipoIngredienteID { get; set; }
        public TipoIngrediente TipoIngrediente { get; set; }
        public int TipoMedidaID { get; set; }
        public TipoMedida TipoMedida { get; set; }
        public string Name { get; set; }
    }
}
