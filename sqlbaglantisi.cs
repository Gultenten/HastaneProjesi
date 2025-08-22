using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace Proje_Hastane//proje adı
{
    class sqlbaglantisi // class adı     internal
    {

        public SqlConnection baglanti()  // /değer döndüren metot adı
        {
            SqlConnection baglan = new SqlConnection("Data Source=DESKTOP-CT9L3VP\\SQLEXPRESS;Initial Catalog=HastaneProje;Integrated Security=True;Encrypt=True;TrustServerCertificate=True");
            baglan.Open();
            return baglan;



        }
        
            

         


        }
        



    }


