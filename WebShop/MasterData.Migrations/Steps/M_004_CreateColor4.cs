using System;
using System.Data;
using MasterData.Migrations.ExtensionMethods;
using MigSharp;

namespace MasterData.Migrations.Steps
{
    [MigrationExport(Tag = "Create table Color")]
    public class M_004_CreateColor4 : IReversibleMigration
    {
        private const string Color = "Color";

        public void Up(IDatabase db)
        {
            db.CreateTable(Color)
              .WithId()
              .WithNotNullableColumn("Name", DbType.AnsiString).OfSize(255).Unique()
              .WithNotNullableColumn("Red", DbType.Int32)
              .WithNotNullableColumn("Green", DbType.Int32)
              .WithNotNullableColumn("Blue", DbType.Int32)
              .WithNotNullableColumn("Alpha", DbType.Decimal).OfSize(19, 2);

            db.Execute(InsertColor("White", 255, 255, 255, 0));
            db.Execute(InsertColor("Black", 0, 0, 0, 0));
            db.Execute(InsertColor("Grey", 128, 128, 128, 0));
            db.Execute(InsertColor("Navy", 0, 0, 128, 0));
            db.Execute(InsertColor("Blue", 0, 0, 255, 0));
            db.Execute(InsertColor("Cyan", 0, 255, 255, 0));
            db.Execute(InsertColor("Green", 0, 128, 0, 0));
            db.Execute(InsertColor("Lime", 0, 255, 0, 0));
            db.Execute(InsertColor("Yellow", 255, 255, 0, 0));
            db.Execute(InsertColor("Maroon", 128, 0, 0, 0));
            db.Execute(InsertColor("Red", 255, 0, 0, 0));
            db.Execute(InsertColor("Orange", 255, 128, 0, 0));
            db.Execute(InsertColor("Magenta", 255, 0, 255, 0));
            db.Execute(InsertColor("Purple", 128, 0, 128, 0));
        }

        private static string InsertColor(string name, int red, int green, int blue, decimal alpha)
        {
            return string.Format("insert into Color(Id, Name, Red, Green, Blue, Alpha) values('{0}', '{1}', {2}, {3}, {4}, {5})",
               Guid.NewGuid(), name, red, green, blue, alpha);
        }

        public void Down(IDatabase db)
        {
            db.Tables[Color].Drop();
        }
    }
}