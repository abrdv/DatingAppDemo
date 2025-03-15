using log4net.Config;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSSensorAguaData.Interfaces;

namespace WSSensorAguaData.Controllers
{
    public class Log4NetLoggerService : ILoggerService
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Log4NetLoggerService));

        public Log4NetLoggerService()
        {
            XmlConfigurator.Configure(new System.IO.FileInfo("log4net.config"));
        }

        public void Info(string message)
        {
            log.Info(message);
        }

        public void Warn(string message)
        {
            log.Warn(message);
        }

        public void Error(string message)
        {
            log.Error(message);
        }
    }
}
