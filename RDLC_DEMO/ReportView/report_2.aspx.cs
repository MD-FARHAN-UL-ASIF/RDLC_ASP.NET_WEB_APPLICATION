using Microsoft.Reporting.WebForms;
using RDLC_DEMO.ReportDataSet;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RDLC_DEMO.ReportView
{
    public partial class report_2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLoadReport_Click(object sender, EventArgs e)
        {
            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/OrderDetails.rdlc");
            Practices practices = GetData();
            ReportDataSource dataSource = new ReportDataSource("Practice", practices.Tables[0]);
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(dataSource);
        }

        private Practices GetData()
        {
            string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

            string query = @"SELECT customers.customer_name, 
                            customers.customer_id, 
                            sales.sale_id, 
                            sales.product_id, 
                            sales.customer_id, 
                            sales.sale_date, 
                            sales.quantity, 
                            sales.price, 
                            products.product_id, 
                            products.product_name, 
                            products.category_id, 
                            categories.category_id, 
                            categories.category_name 
                     FROM categories 
                     INNER JOIN products ON categories.category_id = products.category_id 
                     INNER JOIN sales ON products.product_id = sales.product_id 
                     INNER JOIN customers ON sales.customer_id = customers.customer_id 
                     ORDER BY customers.customer_id";

            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        using (Practices practices = new Practices())
                        {
                            try
                            {
                                da.Fill(practices, "DataTable1");

                                if (practices.Tables["DataTable1"].Rows.Count == 0)
                                {
                                    Console.WriteLine("No data retrieved from database.");
                                }
                            }
                            catch (ConstraintException ex)
                            {
                                // Log the exception details
                                Console.WriteLine("ConstraintException: " + ex.Message);
                            }

                            return practices;
                        }
                    }
                }
            }
        }


    }
}
