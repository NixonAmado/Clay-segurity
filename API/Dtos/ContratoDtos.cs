namespace API.Dtos;
public class G_contratoDto
{
    public DateTime FechaContrato { get; set; }
    public DateTime FechaFin { get; set; }
    public int Cliente_id { get; set; }
    public int Empleado_id { get; set; }
    public int Estado_id { get; set; }

}

public class ContratoEstadoDto
{
    public int Id { get; set; }
    public string Cliente {get;set;}
    public string Empleado { get; set; }

}
