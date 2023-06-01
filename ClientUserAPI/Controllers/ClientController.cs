using ClientUserAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace ClientUserAPI.Controllers
{
    [ApiController]
    public class ClientController : Controller
    {
        [HttpGet]
        [Route("api/Client/getClientList")]
        public List<Client> getClientList()
        {
            List<Client> clients = new List<Client>();
            SqlConnection con = new SqlConnection(@"Data Source=AMANKHA-LT;Initial Catalog=AdminSite;Integrated Security=True");
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from ClientTable", con);
            SqlDataReader reader = cmd.ExecuteReader();
            while(reader.Read())
            {
                clients.Add(new Client
                {
                    id = reader["id"].ToString(),
                    name = reader["name"].ToString(),
                    email = reader["email"].ToString(),
                    detail = reader["detail"].ToString(),
                    password = reader["password"].ToString(),
                    companyName = reader["companyName"].ToString(),
                    userName = reader["userName"].ToString(),
                }) ;
            }
            return clients;
        }
        [HttpGet]
        [Route("api/Client/ClientListById/{id}")]
        public Client getClientListById(int id)
        {
            Client client = new Client();
            SqlConnection con = new SqlConnection(@"Data Source=AMANKHA-LT;Initial Catalog=AdminSite;Integrated Security=True");
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from ClientTable where id=@id", con);
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataReader reader = cmd.ExecuteReader();
            if(reader.Read())
            {
                client.id = reader["id"].ToString();
                client.name = reader["name"].ToString();
                client.email = reader["email"].ToString();
                client.detail = reader["detail"].ToString();
                client.password = reader["password"].ToString();
                client.companyName = reader["companyName"].ToString();
                client.userName = reader["userName"].ToString();
            }
            return client;
        }

        [HttpDelete]
        [Route("api/Client/deleteClient/{id}")]
        public int deleteClient(int id)
        {
            SqlConnection con = new SqlConnection(@"Data Source=AMANKHA-LT;Initial Catalog=AdminSite;Integrated Security=True");
            con.Open();
            string sqlStr = "delete from UserTable where id=@id";
            SqlCommand cmd = new SqlCommand(sqlStr, con);
            var client = getClientListById(id);
            cmd.Parameters.AddWithValue("@id", client.id);
            int result = cmd.ExecuteNonQuery();
            return result;
        }
    }
}
