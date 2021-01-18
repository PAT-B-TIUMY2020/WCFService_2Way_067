using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCFService_2Way_20180140067
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, InstanceContextMode = InstanceContextMode.Single)]
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class ServiceCallback : IServiceCallback
    {
        // Menyimpan data ketika user online
        Dictionary<IClientCallback, string> userList = new Dictionary<IClientCallback, string>();
        public void gabung(string username)
        {
            // Untuk menampung user ketika baru daftar / buat akun
            IClientCallback koneksiGabung = OperationContext.Current.GetCallbackChannel<IClientCallback>();
            userList[koneksiGabung] = username;
        }

//        public string GetData(int value)
//        {
//            return string.Format("You entered: {0}", value);
//        }

//        public CompositeType GetDataUsingDataContract(CompositeType composite)
//        {
//            if (composite == null)
//            {
//                throw new ArgumentNullException("composite");
//            }
//            if (composite.BoolValue)
//            {
//                composite.StringValue += "Suffix";
//            }
//            return composite;
//        }

        public void kirimPesan(string pesan)
        {
            // Mengirim data user dan pesan ke user lain
            IClientCallback koneksiPesan = OperationContext.Current.GetCallbackChannel<IClientCallback>();
            string user;
            if(!userList.TryGetValue(koneksiPesan, out user))
            {
                return;
            }
            foreach(IClientCallback other in userList.Keys)
            {
                other.pesanKirim(user, pesan);
            }
        }
    }
}
