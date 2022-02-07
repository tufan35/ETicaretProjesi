using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ETicaretProjesi.Models;

namespace ETicaretProjesi.MVVM
{
    public class AnasayfaModel
    {
        public List<vw_aktif_urunler> SliderUrunler { get; set; }
        public List<vw_aktif_urunler> EnYeniUrunler { get; set; }
        public List<vw_aktif_urunler> OzelUrunler { get; set; }
        public List<vw_aktif_urunler> IndirimliUrunler { get; set; }
        public List<vw_aktif_urunler> OneCikanUrunler { get; set; }
        public List<vw_aktif_urunler> CokSatanlar { get; set; }
        public List<vw_aktif_urunler> SuperUrunler { get; set; }
        public vw_aktif_urunler GununUrunu { get; set; }
    }
}