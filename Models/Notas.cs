using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Nota {


    [Key] // Agregado para que sea explícito que esta es la clave primaria
    [Required]
    public int NotasID { get; set; }


    [Required]
    public int EstudianteID { get; set; }
    public Estudiante Estudiante { get; set; }

    [Required]
    public int MateriaID { get; set; }
    public Materia Materia { get; set; }

    [Range(0, 100)]
    [Column("Calificacion1")]
    public decimal Cal1 { get; set; }

    [Range(0, 100)]
    [Column("Calificacion2")]
    public decimal Cal2 { get; set; }

    [Range(0, 100)]
    [Column("Calificacion3")]
    public decimal Cal3 { get; set; }

    [Range(0, 100)]
    [Column("Calificacion4")]
    public decimal Cal4 { get; set; }

    [Range(0, 100)]
    public decimal Examen { get; set; }

    [NotMapped]
    public decimal Total =>
        Math.Round((((Cal1 + Cal2 + Cal3 + Cal4) / 4) * 0.7M) + (Examen * 0.3M), 2);

    [NotMapped]
    public string Clasificacion =>
        Total >= 90 ? "A" :
        Total >= 80 ? "B" :
        Total >= 70 ? "C" : "F";

    [NotMapped]
    public string Estado => Total >= 70 ? "Aprobado" : "Reprobado";
}
