using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Net.Http;
using System.ServiceProcess;
using System.Timers;
using Newtonsoft.Json;
using System.Collections.Generic;
using log4net; // Добавляем пространство имен log4net
using log4net.Config; // Добавляем пространство имен для конфигурации log4net
using System.Threading;
using WSSensorAguaData.Controllers;
using WSSensorAguaData.Interfaces;
using WSSensorAguaData.Entities;

namespace WSSensorAguaData
{
    public partial class Service1 : ServiceBase
    {
        private System.Timers.Timer timer;
        private ISensorService sensorService;
        private ILoggerService logger;
        private int interval;

        public Service1()
        {
            InitializeComponent();
            interval = int.Parse(ConfigurationManager.AppSettings["TimerInterval"]);
            logger = new Log4NetLoggerService();
            sensorService = new SensorService(ConfigurationManager.ConnectionStrings["YourConnectionString"].ConnectionString, logger);
        }

        protected override void OnStart(string[] args)
        {
            timer = new System.Timers.Timer();
            timer.Interval = interval;
            timer.Elapsed += new ElapsedEventHandler(OnTimer);
            timer.Start();
            logger.Info("Service started.");
        }

        protected override void OnStop()
        {
            timer.Stop();
            logger.Info("Service stopped.");
        }

        private void OnTimer(object sender, ElapsedEventArgs e)
        {
            ProcessSensorData();
        }

        private void ProcessSensorData()
        {
            try
            {
                if (sensorService.TableExistsAndHasRows())
                {
                    List<Sensor> remoteSensors = sensorService.GetRemoteSensors();
                    sensorService.UpdateSensors(remoteSensors);
                }
                else
                {
                    logger.Warn("Table Sensors does not exist or is empty.");
                }
            }
            catch (Exception ex)
            {
                logger.Error($"Error: {ex.Message}");
            }
        }
    }
}
    

