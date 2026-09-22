using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace SintomasDoencas;

[Table("pessoas")]   //Tabela no banco associada à classe
public class Pessoa
{
    [PrimaryKey,AutoIncrement]
    public int Id { get; set; }

    [MaxLength(100), Unique]  //Sem registros repetidos
    public string Nome { get; set; } = string.Empty;

    [MaxLength(16)]  //Sem registros repetidos
    public string Password { get; set; } = string.Empty;
}
