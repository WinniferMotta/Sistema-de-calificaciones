using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Materia
{
    [Key] // Agregado para que sea explícito que esta es la clave primaria

    [Required]
    public int MateriaID { get; set; }

    [Required]
    [StringLength(100)]
    public string Nombre { get; set; }

    public List<Nota> Notas { get; set; }
}
