using Domain.Entities;

namespace API.Dtos;
public class G_PersonaDto
{
    public string Nombre {get;set;}
    public DateTime DateReg {get;set;}
}

public class TPersonaDto
{
    public string Nombre {get;set;}
    public DateTime DateReg {get;set;}
    public string TipoPersona {get;set;}
}

public class PersonaNumerosDto
{
    public List<G_contactoPersonaDto> Contactos {get;set;}
}

