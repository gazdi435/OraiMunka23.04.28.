using MySql.Data.MySqlClient;

namespace SQLLekerdezesGyakorlas
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string kapcsolatLeiro = "datasource=127.0.0.1;port=3306;database=hardver;username=root;password=;charset=utf8";

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

            string strLekerdezes = "SELECT  DISTINCT * FROM termékek WHERE Ár > (SELECT ár FROM termékek WHERE Ár = (SELECT MAX(Ár) FROM termékek WHERE Kategória LIKE 'winchester%' AND Gyártó LIKE 'seagate%')) AND Kategória LIKE 'winchester%' ORDER BY termékek.Ár ASC;";

            MySqlCommand sqlParancs= new MySqlCommand(strLekerdezes, SQLkapcsolat);

            MySqlDataReader sqlEredmenyolvaso = sqlParancs.ExecuteReader();

            while (sqlEredmenyolvaso.Read())
            {
                Console.WriteLine($"{sqlEredmenyolvaso.GetString("Cikkszám")} | {sqlEredmenyolvaso.GetString("Kategória")} | {sqlEredmenyolvaso.GetString("Gyártó")} | {sqlEredmenyolvaso.GetString("Név")} | {sqlEredmenyolvaso.GetString("Ár")} | {sqlEredmenyolvaso.GetString("Garidő")} | {sqlEredmenyolvaso.GetString("Készlet")} | {sqlEredmenyolvaso.GetString("Súly")}");
            }

            sqlEredmenyolvaso.Close();
            SQLkapcsolat.Close();

        }
    }
}