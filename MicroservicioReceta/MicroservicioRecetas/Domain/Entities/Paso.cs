namespace Domain.Entities
{
    public class Paso
    {
        public int PasoId { get; set; }
        public Guid RecetaId { get; set; }
        public Receta Receta { get; set; }
        public int Orden { get; set; }
        public string Descripcion { get; set; }
        public string Foto { get; set; }
    }
}
