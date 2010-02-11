using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SpecExpress;
using SpecExpress.Quickstart.Domain.Entities;
using SpecExpress.Web;
using SpecExpress.Quickstart.Domain.Specifications;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int a = 1;
    }

    protected object btnValidate_GetObject()
    {
        var provider = new Provider();

        provider.FirstName = txtFirstName.Text;
        provider.LastName = txtLastName.Text;
        provider.MiddleInitial = txtMiddle.Text;
        provider.StartDate = Convert.ToDateTime(txtStartDate.Text);
        provider.Code = Convert.ToInt32(txtCode.Text);
        return provider;
    }

    /// <summary>
    /// Submit Button Clicked
    /// Executes after the object is valid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnValidate_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            lblSuccess.Visible = true;
            lblSuccess.Text = "Validation Successful";
        }
        else
        {
            lblSuccess.Visible = true;
            lblSuccess.Text = "Page Invalid and I don't know why!";
        }
       
    }

    /// <summary>
    ///  Executes when an Validation Error occurs
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnValidate_Invalid(object sender, ValidationNotificationEventArgs e)
    {
        lblSuccess.Visible = true;
        lblSuccess.Text = "Error, Will Robinson! " + e.ValidationNotification.Errors.Count + " errors found";



    }
}
