using Microsoft.Reporting.WebForms;
using RDLC_DEMO.ReportDataSet;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RDLC_DEMO.ReportView
{
    public partial class report_1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnLoadReport_Click(object sender, EventArgs e)
        {
            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/CustomerDetails.rdlc");
            Customers customers = GetData();
            ReportDataSource dataSource = new ReportDataSource("Customer", customers.Tables[0]);
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(dataSource);
        }

        private Customers GetData()
        {
            string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            string query = "SELECT * FROM Customers WHERE country=@Country";

            SqlCommand cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@Country", txtCountry.Text);

            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlDataAdapter da = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    da.SelectCommand = cmd;
                    using (Customers customers = new Customers())
                    {
                        da.Fill(customers, "Cus");

                        // Debugging: Check if the DataTable has rows
                        if (customers.Tables["Cus"].Rows.Count == 0)
                        {
                            // Log or display a message indicating no data was retrieved
                            // For example:
                            Console.WriteLine("No data retrieved from database.");
                        }

                        return customers;
                    }
                }
            }
        }

    }
}