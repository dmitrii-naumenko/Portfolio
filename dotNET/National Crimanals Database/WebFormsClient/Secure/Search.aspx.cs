using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebFormsClient.NcSearchService;

namespace WebFormsClient
{
    public partial class Search : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                tbEmail.Text = Context.Profile["Email"].ToString();
        }

        protected void OnSubmit(object sender, EventArgs e)
        {
            if (IsValid)
            {
                Context.Profile["Email"] = tbEmail.Text;     

                var par = new SearchParameter
                {
                    FirstName = tbFirstName.Text,
                    LastName = tbLastName.Text,
                    Sex = tbSex.Text != "Any" ? tbSex.Text : null
                };

                //var par = new SearchParameter() {FirstName = "David"};

                using (var service = new NcSearchServiceClient())
                {
                    var result = service.Search(par, "user.testservice@yandex.ru", 100);
                    if (!result)
                        ScriptManager.RegisterStartupScript(this, GetType(), "error", "alert('requst faild!');", true);
                    else
                        ScriptManager.RegisterStartupScript(this, GetType(), "ok", "alert('your request is processing. Please see you email');", true);
                }
            }
        }
    }
}