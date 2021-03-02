using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DMS.Repository;
using DMS.Data;

namespace DMSWTestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            tblUserType utype=new tblUserType();
            utype.UserTypeId=6;
            utype.UserType="kushan";
            UnitOfWork unitOfWork = new UnitOfWork();
            IRepository<tblUserType> repository = unitOfWork.GetRepository<tblUserType>();
            repository.Insert(utype);
            unitOfWork.SaveChanges();
        }
    }
}
