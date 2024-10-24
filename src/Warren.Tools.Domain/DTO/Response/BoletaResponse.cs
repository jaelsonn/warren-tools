namespace Warren.Tools.Domain.DTO.Response
{
    public class BoletaResponse
    {
        public int IdBoleta { get; set; }
        public int IdPreBoleta { get; set; }
        public int IdOperation { get; set; }
        public string MessageMetadata { get; set; }
        public string Command { get; set; }
        public string AssociationCommand { get; set; }
        public string BuyerSelicAccount { get; set; }
        public bool Debit { get; set; }
        public string Client { get; set; }
        public DateTime DateLiquidation { get; set; }
        public string UnitPrice { get; set; }
        public string BondQuantity { get; set; }
        public string Financial { get; set; }
        public string SellerSelicAccount { get; set; }
        public string Status { get; set; }
        public string ResultCommand { get; set; }
    }
}
