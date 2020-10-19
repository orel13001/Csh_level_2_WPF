using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.ObjectModel;
using System.Windows;


namespace Csh_level_2_WPF
{
    public enum TypeGenObject
    {
        St = 1,
        GTP = 2,
        EGO = 3
    }

    class FillFromDB
    {
        internal static ObservableCollection<Station> GetStationsFromDB()
        {

            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;


            ObservableCollection<Station> stations = new ObservableCollection<Station>();
            using (SqlConnection connectionSt = new SqlConnection(connectionString))
            {
                connectionSt.Open();
                string queryStations = $"SELECT Id, Name, Code FROM GenObject Where TypeId={(int)TypeGenObject.St}";
                var commandSt = new SqlCommand(queryStations, connectionSt);
                SqlDataReader readerSt = commandSt.ExecuteReader();
                
                while (readerSt.Read())
                {
                    stations.Add(new Station(Convert.ToInt32(readerSt.GetValue(0)), readerSt.GetString(1), readerSt.GetString(2)));
                    
                    string queryGTP = $"SELECT Id, Name, Code FROM GenObject Where TypeId={(int)TypeGenObject.GTP} And ParentId={readerSt.GetValue(0)}";

                    using (SqlConnection connectionGTP = new SqlConnection(connectionString))
                    {
                        connectionGTP.Open();
                        var commandGTP = new SqlCommand(queryGTP, connectionGTP);
                        SqlDataReader readerGTP = commandGTP.ExecuteReader();
                        while (readerGTP.Read())
                        {
                            stations[stations.Count - 1].AddGTP(new GTP(Convert.ToInt32(readerGTP.GetValue(0)), readerGTP.GetString(1), readerGTP.GetString(2)));

                            string queryEGO = String.Format(@"SELECT gobj.Id AS GenObjId, gobj.ParentId, gobj.Name AS NameEGO, gobj.Code, ep.Id AS ParamsId, ep.Pmax, ep.Pmin
                                            FROM
                                            (SELECT *
                                                FROM GenObject
                                                WHERE TypeId = {1} AND ParentId = {0}) AS gobj
                                            LEFT JOIN EGO_Params AS ep ON gobj.Id = ep.EGO_Id", readerGTP.GetValue(0), (int)TypeGenObject.EGO);
                            using (SqlConnection connectionEGO = new SqlConnection(connectionString))
                            {
                                connectionEGO.Open();
                                var commandEGO = new SqlCommand(queryEGO, connectionEGO);
                                SqlDataReader readerEGO = commandEGO.ExecuteReader();

                                while (readerEGO.Read())
                                {
                                    stations[stations.Count - 1].gtps[stations[stations.Count - 1].gtps.Count - 1].AddEGO(new EGO(Convert.ToInt32(readerEGO.GetValue(0)), readerEGO.GetString(2), readerEGO.GetString(3), Convert.ToDouble(readerEGO.GetValue(5)), Convert.ToDouble(readerEGO.GetValue(6))));
                                }
                            }
                        }
                    }
                }
            }
            return stations;
        }
        

        internal static bool AddGenObject(WinAdd winAdd)
        {
            string Name = winAdd.tbName.Text;
            string Code = winAdd.tbCode.Text;
            string parentId = "NULL";
            string typeGenObj = "";
            string Pmax = winAdd.tbPmax.Text;
            string Pmin = winAdd.tbPmin.Text;
            bool isEGO = false;
            switch (winAdd.cbType.SelectedIndex)
            {
                case (0):
                    parentId = "NULL";
                    typeGenObj = Convert.ToInt32(TypeGenObject.St).ToString();
                    break;
                case (1):
                    parentId = MainWindow.stations[winAdd.cbStation.SelectedIndex].Id.ToString();
                    typeGenObj = Convert.ToInt32(TypeGenObject.GTP).ToString();
                    break;
                case (2):
                    parentId = MainWindow.stations[winAdd.cbStation.SelectedIndex].gtps[winAdd.cbGTP.SelectedIndex].Id.ToString();
                    typeGenObj = Convert.ToInt32(TypeGenObject.EGO).ToString();
                    isEGO = true;
                    break;
            }            

            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string sqlQuery = $"INSERT INTO [dbo].[GenObject] ([ParentId], [Code], [Name], [TypeId]) VALUES ({parentId}, N'{Code}', N'{Name}', {typeGenObj})";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlQuery, connection);
                    command.ExecuteNonQuery();
                    if (isEGO)
                    {
                        sqlQuery = "SELECT IDENT_CURRENT('[GenObject]')";
                        command = new SqlCommand(sqlQuery, connection);
                        int res = Convert.ToInt32(command.ExecuteScalar());
                        
                        sqlQuery = $"INSERT INTO [dbo].[EGO_Params] ([EGO_Id], [Pmax], [Pmin]) VALUES ({res}, CAST({Pmax} AS Decimal(7, 3)), CAST({Pmin} AS Decimal(7, 3)))";
                        command = new SqlCommand(sqlQuery, connection);
                        command.ExecuteNonQuery();
                    }
                }
                MainWindow.stations = FillFromDB.GetStationsFromDB();
                return true;
            }
            catch (SqlException)
            {
                MessageBox.Show("Ошибка при заполнении БД");
                return false;
            }
            catch (Exception)
            {
                MessageBox.Show("Проверте корректность заполнения полей");
                return false;
            }

            
        }
    }
}
