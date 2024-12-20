using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace labelPrint.Class
{
    public class CModelPrint
    {
        String m_dj_group; 
        int m_num_labels;
        int m_num_panels;
        int m_id_flux; 
        int m_id_color; 
        int m_id_Volt;
        int m_id_flux1;
        int m_id_color1;
        int m_id_Volt1;
        int m_id_rev;
        int m_id_model;
        int m_id_side;
        String m_date_plan;

        public string Dj_group { get => m_dj_group; set => m_dj_group = value; }
        public int Num_labels { get => m_num_labels; set => m_num_labels = value; }
        public int Id_flux { get => m_id_flux; set => m_id_flux = value; }
        public int Id_color { get => m_id_color; set => m_id_color = value; }
        public int Id_Volt { get => m_id_Volt; set => m_id_Volt = value; }
        public int Id_rev { get => m_id_rev; set => m_id_rev = value; }
        public String Date_plan { get => m_date_plan; set => m_date_plan = value; }
        public int Id_model { get => m_id_model; set => m_id_model = value; }
        public int Id_side { get => m_id_side; set => m_id_side = value; }
        public int Id_flux1 { get => m_id_flux1; set => m_id_flux1 = value; }
        public int Id_color1 { get => m_id_color1; set => m_id_color1 = value; }
        public int Id_Volt1 { get => m_id_Volt1; set => m_id_Volt1 = value; }
        public int Num_panels { get => m_num_panels; set => m_num_panels = value; }
    }

}