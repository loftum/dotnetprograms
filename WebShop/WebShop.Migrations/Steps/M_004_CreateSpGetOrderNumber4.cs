using MigSharp;

namespace WebShop.Migrations.Steps
{
    [MigrationExport(Tag = "Create SP GetNextOrderNumber")]
    public class M_004_CreateSpGetOrderNumber4 : IReversibleMigration
    {
        public void Up(IDatabase db)
        {
            db.Execute(
                @"CREATE PROCEDURE [dbo].[GetNextOrderNumber]
                AS  
                BEGIN  
                    BEGIN TRANSACTION  
                        DECLARE @CurrentValue bigint
                        UPDATE SequenceCounter
                        with(rowlock)
                        SET @CurrentValue = CurrentValue = CurrentValue + 1
                        WHERE Name = 'OrderNumber'
                        SELECT @CurrentValue
                    COMMIT TRANSACTION
                END");
        }

        public void Down(IDatabase db)
        {
            db.Execute("drop procedure [dbo].[GetNextOrderNumber]");
        }
    }
}