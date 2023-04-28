using MySql.Data.MySqlClient;

namespace SQLLekerdezesGyakorlas
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string kapcsolatLeiro = "datasource=127.0.0.1;port=3306;database=hardver;username=root;password=;Charset=utf8;";
            MySqlConnection SQLkapcsolat = new MySqlConnection(kapcsolatLeiro);

            try
            {
                SQLkapcsolat.Open();
            }
            catch (MySqlException)
            {
                Console.WriteLine("Nem lehet csatlakozni az adatbázishoz!");
                Environment.Exit(1);
            }

            string SQLLekerdezes = "SELECT DISTINCT Gyártó FROM termékek group by Gyártó having COUNT(Kategória = 'Monitor') > 39;";

            MySqlCommand SQLparancs = new MySqlCommand(SQLLekerdezes, SQLkapcsolat);

            MySqlDataReader eredmenyolvaso = SQLparancs.ExecuteReader();

            while (eredmenyolvaso.Read())
            {
                Console.WriteLine(eredmenyolvaso.GetString("Gyártó"));
            }

            eredmenyolvaso.Close();

            SQLkapcsolat.Close();
        }
    }
}