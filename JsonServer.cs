using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;

namespace pr5
{
    public class JsonServer
    {
        static private string file = "file.json";
        static public void WriteFile (ObservableCollection<BookingModel> bo)
        {
            if (bo == null)
            {
                return;
            }
            var optjson = new JsonSerializerOptions { WriteIndented = true };
            string jsonstr = JsonSerializer.Serialize(bo, optjson);
            File.WriteAllText(file, jsonstr);
        }

        static public ObservableCollection<BookingModel> ReadFile ()
        {
            if (!File.Exists(file))
            {
                return new ObservableCollection<BookingModel>();
            }
            string jsonstr = File.ReadAllText(file);
            ObservableCollection<BookingModel> by = JsonSerializer.Deserialize<ObservableCollection<BookingModel>>(jsonstr);
            return by;
        }
    }
}
