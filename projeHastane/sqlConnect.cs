using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace projeHastane
{
    class sqlConnect
    {
        public SqlConnection baglanti()
        {
            SqlConnection baglan = new SqlConnection("Data Source=DESKTOP-SJORCEJ;Initial Catalog=hastaneProje;Integrated Security=True");
            baglan.Open();
            return baglan;
        }

    }
}
