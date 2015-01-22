using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ASP.NET_Web_Forms_Game
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Application.Add("Key", null);
        }

        public string GetAnswer()
        {
            return ""; 
        }
    }
}