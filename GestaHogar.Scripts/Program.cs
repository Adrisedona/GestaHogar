using MySql.Data.MySqlClient;

MySqlConnection connection = new("Server=localhost;Database=GestaHogarDb;User=root;Password=;");
try
{
    connection.Open();
    Console.WriteLine("Connection to the database opened");

    MySqlCommand command = new(
        "CREATE IF NOT EXISTS EVENT `update_stock` ON SCHEDULE EVERY 1 DAY DO UPDATE UserProduct SET CurrentStock = IF (CurrentStock < DailyUse, 0, CurrentStock - DailyUse ); SET GLOBAL event_scheduler = ON;",
        connection
    );

    command.ExecuteNonQuery();
    Console.WriteLine("Event added successfully");
}
catch (MySqlException ex)
{
    Console.WriteLine(ex);
}
finally
{
    connection.Close();
}
