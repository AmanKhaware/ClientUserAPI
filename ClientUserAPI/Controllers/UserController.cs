using ClientUserAPI.Model;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Reflection.Metadata.Ecma335;

namespace ClientUserAPI.Controllers
{
    [ApiController]
    public class UserController : Controller
    {
        [HttpGet]
        [Route("api/User/getUserList")]
        public List<User> getClientList()
        {
            List<User> user = new List<User>();
            SqlConnection con = new SqlConnection(@"Data Source=AMANKHA-LT;Initial Catalog=AdminSite;Integrated Security=True");
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from UserTable", con);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                user.Add(new User
                {
                    id = reader["id"].ToString(),
                    name = reader["name"].ToString(),
                    email = reader["email"].ToString(),
                    detail = reader["detail"].ToString(),
                    password = reader["password"].ToString(),
                    companyName = reader["companyName"].ToString(),
                    userName = reader["userName"].ToString(),
                });
            }
            return user;
        }
        [HttpGet]
        [Route("api/User/UserListById/{id}")]
        public User getUserListById(int id)
        {
            User user = new User();
            SqlConnection con = new SqlConnection(@"Data Source=AMANKHA-LT;Initial Catalog=AdminSite;Integrated Security=True");
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from UserTable where id=@id", con);
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                user.id = reader["id"].ToString();
                user.name = reader["name"].ToString();
                user.email = reader["email"].ToString();
                user.detail = reader["detail"].ToString();
                user.password = reader["password"].ToString();
                user.companyName = reader["companyName"].ToString();
                user.userName = reader["userName"].ToString();
            }
            return user;
        }

        [HttpPost]
        [Route("api/User/addNewUser")]
        public int addNewUser([FromBody] User usr) {
            
            SqlConnection con = new SqlConnection(@"Data Source=AMANKHA-LT;Initial Catalog=AdminSite;Integrated Security=True");
            con.Open();
            string sqlString = "insert into UserTable values(@id,@name,@detail,@email,@password,@userName,@companyName)";
            SqlCommand cmd = new SqlCommand(sqlString, con);
            cmd.Parameters.AddWithValue("@id", usr.id);
            cmd.Parameters.AddWithValue("@name", usr.name);
            cmd.Parameters.AddWithValue("@detail", usr.detail);
            cmd.Parameters.AddWithValue("@email", usr.email);
            cmd.Parameters.AddWithValue("@password", usr.password);
            cmd.Parameters.AddWithValue("@userName", usr.userName);
            cmd.Parameters.AddWithValue("@companyName", usr.companyName);
            int result = cmd.ExecuteNonQuery();
            return result;
        }

        [HttpDelete]
        [Route("api/User/deleteUser/{id}")]
        public int deleteUser(int id)
        {
            SqlConnection con = new SqlConnection(@"Data Source=AMANKHA-LT;Initial Catalog=AdminSite;Integrated Security=True");
            con.Open();
            string sqlStr = "delete from UserTable where id=@id";
            SqlCommand cmd = new SqlCommand(sqlStr, con);
            //var user = getUserListById(id);
            cmd.Parameters.AddWithValue("@id", id);
            int result = cmd.ExecuteNonQuery();
            return result;
        }
    }
}
