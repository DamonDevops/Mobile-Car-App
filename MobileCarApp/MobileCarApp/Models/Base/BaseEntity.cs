using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileCarApp.Models.Base;

public class BaseEntity
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
}
