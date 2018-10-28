namespace SampleProject.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class MyConnection : DbContext
    {
        public MyConnection()
            : base("name=DefaultConnection")
        {
        }
    }
}
