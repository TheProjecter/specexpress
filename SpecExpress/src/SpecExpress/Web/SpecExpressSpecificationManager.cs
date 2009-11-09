using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

[assembly: TagPrefix("SpecExpress", "spec")]


namespace SpecExpress.Web
{
    [ToolboxData("<{0}:SpecExpressSpecificationManager runat='server'></{0}:SpecExpressSpecificationManager>")]    
    public class SpecExpressSpecificationManager : WebControl 
    {
        public string SpecificationType { get; set; }
    }
}
