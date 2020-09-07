using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kurumsal.Models.Sınıflar
{
    public class MultipleModels
    {
        public Tuple<List<HizmetKategori>, List<AltKategori>> Tub { get; set; }
    }
}