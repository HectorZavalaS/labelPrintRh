using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace labelPrint.Class
{
    public class CModelInfo
    {
        private String PART_NUMBER;
        String REVISION;
        String ROUTE_NAME;
        int NB_CIRCUIT_PER_PANEL;
        String PN_DESC;
        String SN_VALIDATION_REGEX;
        String PRINT_LABEL_FOR_PANEL;

        public string FG_NAME { get => PART_NUMBER; set => PART_NUMBER = value; }
        public string REV { get => REVISION; set => REVISION = value; }
        public string ROUTE { get => ROUTE_NAME; set => ROUTE_NAME = value; }
        public int NUMBER_BOARDS { get => NB_CIRCUIT_PER_PANEL; set => NB_CIRCUIT_PER_PANEL = value; }
        public string DESCRIPTION { get => PN_DESC; set => PN_DESC = value; }
        public string POKAYOKE { get => SN_VALIDATION_REGEX; set => SN_VALIDATION_REGEX = value; }
        public string HAS_PANEL { get => PRINT_LABEL_FOR_PANEL; set => PRINT_LABEL_FOR_PANEL = value; }
    }
}