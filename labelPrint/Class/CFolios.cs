using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace labelPrint.Class
{
    public class CFolios
    {
        private int m_init_folio;
        private int m_last_folio;
        private int m_curr_folio;
        private int m_printFolio;
        private String m_full_template;
        private String m_line_bot_template;
        private int m_side;

        public int Init_folio { get => m_init_folio; set => m_init_folio = value; }
        public int Last_folio { get => m_last_folio; set => m_last_folio = value; }
        public int Curr_folio { get => m_curr_folio; set => m_curr_folio = value; }
        public int PrintFolio { get => m_printFolio; set => m_printFolio = value; }
        public string Full_template { get => m_full_template; set => m_full_template = value; }
        public string Line_bot_template { get => m_line_bot_template; set => m_line_bot_template = value; }
        public int Side { get => m_side; set => m_side = value; }
    }
}