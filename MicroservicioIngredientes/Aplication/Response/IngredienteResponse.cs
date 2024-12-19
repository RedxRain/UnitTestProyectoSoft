namespace Application.Response
{
    public class IngredienteResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TipoIngredienteGetResponse TipoIngrediente { get; set; }
        public TipoMedidaGetResponse TipoMedida { get; set; }
    }

    public class TipoMedidaGetResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class TipoIngredienteGetResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
