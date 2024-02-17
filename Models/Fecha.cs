using System.ComponentModel.DataAnnotations;

namespace pruebaSippina.Models;

public class Fecha(){
    
    [Key]
    public Int32 idfecha {get; set;}

    public Int32? dominio {get; set;}
    public String? mes {get; set;}


}