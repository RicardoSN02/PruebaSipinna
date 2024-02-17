using System.ComponentModel.DataAnnotations;

namespace pruebaSippina.Models;

public class Edades(){
    
    [Key]
    public Int32 idedades {get; set;}

    public String? rangoEdades {get; set;}


}