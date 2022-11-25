using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCWEF.Models;

namespace MVCWEF.Controllers
{
    public class CrudController : Controller
    {
        // GET: Crud

        string connectionString = @"Data Source=MOHAMEDYAHEEYA;Initial Catalog=Mydb;Integrated Security=True";
        [HttpGet]
        public ActionResult Index()
        {
            DataTable dtblProduct = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM Crudtb", sqlCon);
                sqlDa.Fill(dtblProduct);
            }
            return View(dtblProduct);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View(new EmployeeModel());
        }

        //
        // POST: /Crud/Create
        [HttpPost]
        public ActionResult Create(EmployeeModel em)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "INSERT INTO Crudtb VALUES(@emp_Id,@emp_Name,@emp_Salary,@CreateOn)";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@emp_Id", em.emp_ID);
                sqlCmd.Parameters.AddWithValue("@emp_Name", em.emp_Name);
                sqlCmd.Parameters.AddWithValue("@emp_Salary", em.emp_Salary);
                sqlCmd.Parameters.AddWithValue("@CreateOn", em.CreateOn);
                sqlCmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");

        }
        // GET: /Product/Edit/5
        public ActionResult Edit(int id)
        {
            EmployeeModel productModel = new EmployeeModel();
            DataTable dtblProduct = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "SELECT * FROM Crudtb Where emp_Id = @emp_ID";
                SqlDataAdapter sqlDa = new SqlDataAdapter(query, sqlCon);
                sqlDa.SelectCommand.Parameters.AddWithValue("@emp_ID", id);
                sqlDa.Fill(dtblProduct);
            }
            if (dtblProduct.Rows.Count == 1)
            {
                productModel.emp_ID = Convert.ToInt32(dtblProduct.Rows[0][0].ToString());
                productModel.emp_Name = dtblProduct.Rows[0][1].ToString();
                productModel.emp_Salary =(dtblProduct.Rows[0][2].ToString());
                productModel.CreateOn = (dtblProduct.Rows[0][3].ToString());
                return View(productModel);
            }
            else
                return RedirectToAction("Index");
        }

        //
        // POST: /Crud/Edit/5
        [HttpPost]
        public ActionResult Edit(EmployeeModel em)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "UPDATE Crudtb SET emp_Name = @emp_Name , emp_Salary= @emp_Salary , CreateOn = @CreateOn WHere emp_Id = @emp_Id";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@emp_Id", em.emp_ID);
                sqlCmd.Parameters.AddWithValue("@emp_Name", em.emp_Name);
                sqlCmd.Parameters.AddWithValue("@emp_Salary", em.emp_Salary);
                sqlCmd.Parameters.AddWithValue("@CreateOn", em.CreateOn);
                sqlCmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        
        // GET: /Crude/Delete/5
        public ActionResult Delete(int id)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "DELETE FROM Crudtb WHere emp_ID = @emp_ID";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@emp_ID", id);
                sqlCmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }
    }
}

