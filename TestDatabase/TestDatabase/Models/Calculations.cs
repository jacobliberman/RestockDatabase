using CsvHelper.Configuration;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;

namespace TestDatabase.Models
{
    [Table("calculations")]
    public class Calculations
    {
        //[PrimaryKey, MaxLength(250), Unique]
        public string ProductName;

        //[MaxLength(2)]
        public string EquationSymbol;

        public int Amount;



    }

    public class CalculationsMap : ClassMap<Calculations>
    {
        public CalculationsMap()
        {
            Map(m => m.ProductName).Name("Product Name");
            Map(m => m.EquationSymbol).Name("Equation Symbol");
            Map(m => m.Amount).Name("Amount");
            
        }
    }
}
