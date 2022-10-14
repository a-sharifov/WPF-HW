using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WpfApp3.Model
{
    public static class DeserializeFilmSearchService
    {
        public static Rootobject? deserializeFilmInfo(string json)
        {
 
            return JsonSerializer.Deserialize<Rootobject?>(json);
        }
    }
}
