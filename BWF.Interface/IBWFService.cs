using BWF.Library;
using System;
using System.ServiceModel;

namespace BWF.Service.Interface
{
    [ServiceContract(Name = "BWFService", Namespace = "http://schemas.zso.ch/BrainWashFuck/server/BWFService")]
    public interface IBWFService
    {
        [OperationContract]
        FragenCol GetFrageCol();

        [OperationContract]
        FragenGruppeCol GetFragenGruppeCol();

        [OperationContract]
        Soldat GetSoldat(string SVNumber);

        [OperationContract]
        void SaveSoldatAntwortCol(SoldatAntwortCol soldatAntwortCol, Guid soldatId, Guid anlassId);
    }
}