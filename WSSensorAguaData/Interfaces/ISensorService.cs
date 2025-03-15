using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSSensorAguaData.Interfaces
{
    public interface ISensorService
    {
        List<Sensor> GetRemoteSensors();
        void UpdateSensors(List<Sensor> remoteSensors);
        bool TableExistsAndHasRows();
    }
}
