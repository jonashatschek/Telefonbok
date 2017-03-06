using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Xml.Linq;

namespace TelefonbokWCFService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface ITelefonbokService
    {

        [OperationContract]
        XElement HämtaAllaKontakter();

        [OperationContract]
        void LäggTillKontakt(XElement kontakt);

        [OperationContract]
        void TaBortKontakt(int id);

        [OperationContract]
        void ÄndraKontakt(XElement ändraKontakt);

        [OperationContract]
        XElement SökKontakter(XElement kriterier);

        /*
        string GetData(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);
        */
        // TODO: Add your service operations here



    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.

}
