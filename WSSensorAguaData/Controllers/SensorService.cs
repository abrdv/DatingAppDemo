using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WSSensorAguaData.Interfaces;
using log4net; // Добавляем пространство имен log4net
using log4net.Config;
using WSSensorAguaData.Entities; 

namespace WSSensorAguaData
{
    public class SensorService : ISensorService
    {
        private string connectionString;
        private ILoggerService logger;
        private int DataAPImaxRetries;
        private int DataAPIretryDelay;
        private int DataBDmaxRetries;
        private int DataBDretryDelay;
        

        public SensorService(string connectionString, ILoggerService logger)
        {
            this.connectionString = connectionString;
            this.logger = logger;
            DataAPImaxRetries = int.Parse(ConfigurationManager.AppSettings["GetDataAPImaxRetries"]);
            DataAPIretryDelay = int.Parse(ConfigurationManager.AppSettings["GetDataAPIretryDelay"]);
            DataBDmaxRetries = int.Parse(ConfigurationManager.AppSettings["SetDataBDmaxRetries"]);
            DataBDretryDelay = int.Parse(ConfigurationManager.AppSettings["SetDataBDretryDelay"]);
        }

        public List<Sensor> GetRemoteSensors()
        {
            int maxRetries = DataAPImaxRetries;
            int retryDelay = DataAPIretryDelay;
            for (int retry = 0; retry < maxRetries; retry++)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        string json = client.GetStringAsync("http://aca-web.gencat.cat/sdim2/apirest/catalog").Result;
                        return JsonConvert.DeserializeObject<List<Sensor>>(json);
                    }
                }
                catch (Exception ex)
                {
                    logger.Warn($"Error getting remote sensors (attempt {retry + 1}): {ex.Message}");
                    if (retry < maxRetries - 1)
                    {
                        Thread.Sleep(retryDelay);
                        retryDelay *= 2; // Экспоненциальное увеличение задержки
                    }
                    else
                    {
                        logger.Error("Failed to get remote sensors after multiple retries.");
                        throw; // Пробрасываем исключение, если все попытки неудачны
                    }
                }
            }
            return null; // Этот код никогда не будет выполнен, но компилятор требует возвращаемое значение
        }

        public void UpdateSensors(List<Sensor> remoteSensors)
        {
            int maxRetries = DataBDmaxRetries;
            int retryDelay = DataBDretryDelay;
            for (int retry = 0; retry < maxRetries; retry++)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        foreach (Sensor sensor in remoteSensors)
                        {
                            string checkIfExistsQuery = "SELECT COUNT(*) FROM Sensors WHERE id = @Id";
                            using (SqlCommand checkCommand = new SqlCommand(checkIfExistsQuery, connection))
                            {
                                checkCommand.Parameters.AddWithValue("@Id", sensor.Id);
                                int count = (int)checkCommand.ExecuteScalar();

                                if (count == 0)
                                {
                                    string insertQuery = "INSERT INTO Sensors (id, name, description) VALUES (@Id, @Name, @Description)";
                                    using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                                    {
                                        insertCommand.Parameters.AddWithValue("@Id", sensor.Id);
                                        insertCommand.Parameters.AddWithValue("@Name", sensor.Name);
                                        insertCommand.Parameters.AddWithValue("@Description", sensor.Description);
                                        insertCommand.ExecuteNonQuery();
                                    }
                                }
                            }
                        }
                        return; // Успешное выполнение, выходим из метода
                    }
                }
                catch (Exception ex)
                {
                    logger.Warn($"Error updating sensors (attempt {retry + 1}): {ex.Message}");
                    if (retry < maxRetries - 1)
                    {
                        Thread.Sleep(retryDelay);
                        retryDelay *= 2; // Экспоненциальное увеличение задержки
                    }
                    else
                    {
                        logger.Error("Failed to update sensors after multiple retries.");
                        throw; // Пробрасываем исключение, если все попытки неудачны
                    }
                }
            }
        }

        public bool TableExistsAndHasRows()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string checkTableQuery = "IF OBJECT_ID('Sensors', 'U') IS NOT NULL BEGIN SELECT COUNT(*) FROM Sensors END ELSE SELECT 0";
                using (SqlCommand command = new SqlCommand(checkTableQuery, connection))
                {
                    int rowCount = (int)command.ExecuteScalar();
                    return rowCount > 0;
                }
            }
        }

        List<Sensor> ISensorService.GetRemoteSensors()
        {
            throw new NotImplementedException();
        }

        public void UpdateSensors(List<Sensor> remoteSensors)
        {
            throw new NotImplementedException();
        }

        List<Sensor> ISensorService.GetRemoteSensors()
        {
            throw new NotImplementedException();
        }

        void ISensorService.UpdateSensors(List<Sensor> remoteSensors)
        {
            throw new NotImplementedException();
        }

        bool ISensorService.TableExistsAndHasRows()
        {
            throw new NotImplementedException();
        }
    }

}
