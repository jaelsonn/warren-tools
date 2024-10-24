using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Warren.Tools.Domain.Entities
{
    [Table("boleta")]
    public class BoletaEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdBoleta { get; set; }

        [Column("idpreboleta")]
        public int? IdPreBoleta { get; set; }

        [Column("idoperation")]
        public int? IdOperation { get; set; }

        [Column("idmessage")]
        public int? IdMessage { get; set; }

        [Column("message_metadata", TypeName = "varchar(45)")]
        public string? MessageMetadata { get; set; }

        [Column("command", TypeName = "varchar(6)")]
        public string? Command { get; set; }

        [Column("associationcommand", TypeName = "varchar(6)")]
        public string? AssociationCommand { get; set; }

        [Column("resultcommand", TypeName = "varchar(6)")]
        public string? ResultCommand { get; set; }

        [Column("sellerselicaccount", TypeName = "varchar(9)")]
        public string? SellerSelicAccount { get; set; }

        [Column("buyerselicaccount", TypeName = "varchar(9)")]
        public string? BuyerSelicAccount { get; set; }

        [Column("debit")]
        public bool? Debit { get; set; }

        [Column("cnpj_cpf", TypeName = "varchar(14)")]
        public string? CnpjCpf { get; set; }

        [Column("bond_code", TypeName = "varchar(6)")]
        public string? BondCode { get; set; }

        [Column("bond_maturity")]
        public DateTime? BondMaturity { get; set; }

        [Column("bond_quantity", TypeName = "varchar(13)")]
        public string? BondQuantity { get; set; }

        [Column("unit_price", TypeName = "varchar(21)")]
        public string? UnitPrice { get; set; }

        [Column("unit_price_intermediation", TypeName = "varchar(22)")]
        public string? UnitPriceIntermediation { get; set; }

        [Column("percentage_par", TypeName = "varchar(12)")]
        public string? PercentagePar { get; set; }

        [Column("percentage_par_intermediation", TypeName = "varchar(12)")]
        public string? PercentageParIntermediation { get; set; }

        [Column("date_liquidation")]
        public DateTime? DateLiquidation { get; set; }

        [Column("status", TypeName = "varchar(45)")]
        public string? Status { get; set; }

        [Column("verified")]
        public bool? Verified { get; set; }

        [Column("registered")]
        public bool? Registered { get; set; }

        [Column("liquidated")]
        public bool? Liquidated { get; set; }

        [Column("cancelled")]
        public bool? Cancelled { get; set; }

        [Column("date_creation")]
        public DateTime? DateCreation { get; set; }

        [Column("custodian", TypeName = "varchar(45)")]
        public string? Custodian { get; set; }

        [Column("client", TypeName = "varchar(100)")]
        public string? Client { get; set; }

        [Column("financial", TypeName = "varchar(22)")]
        public string? Financial { get; set; }

        [Column("version")]
        public int? Version { get; set; }

        [Column("status_time")]
        public DateTime? StatusTime { get; set; }

        [Column("idpassagem")]
        public int? IdPassagem { get; set; }

        [Column("automaticresendexp")]
        public bool? AutomaticResendExp { get; set; }

        [Column("unit_price_return", TypeName = "varchar(21)")]
        public string? UnitPriceReturn { get; set; }

        [Column("unit_price_intermediation_return", TypeName = "varchar(22)")]
        public string? UnitPriceIntermediationReturn { get; set; }

        [Column("financial_return", TypeName = "varchar(22)")]
        public string? FinancialReturn { get; set; }

        [Column("date_begin_commitment_liquidation")]
        public DateTime? DateBeginCommitmentLiquidation { get; set; }

        [Column("date_end_commitment_liquidation")]
        public DateTime? DateEndCommitmentLiquidation { get; set; }

        [Column("preference_level", TypeName = "varchar(1)")]
        public string? PreferenceLevel { get; set; }

        [Column("type_unilateral", TypeName = "varchar(1)")]
        public string? TypeUnilateral { get; set; }

        [Column("date_original_operation")]
        public DateTime? DateOriginalOperation { get; set; }

        [Column("auctionnumber", TypeName = "varchar(6)")]
        public string? AuctionNumber { get; set; }

        [Column("exported")]
        public bool? Exported { get; set; }

        [Column("crkcode", TypeName = "varchar(15)")]
        public string? CrkCode { get; set; }

        [Column("bond_release_date")]
        public DateTime? BondReleaseDate { get; set; }

        [Column("numopcrk", TypeName = "varchar(45)")]
        public string? NumOpCrk { get; set; }

        [Column("automaticRDC0008")]
        public bool? AutomaticRDC0008 { get; set; }

        [Column("proprietaryaccount")]
        public bool? ProprietaryAccount { get; set; }

        [Column("excludefrombondmovementreport")]
        public bool? ExcludeFromBondMovementReport { get; set; }

        [Column("processedexclusion")]
        public bool? ProcessedExclusion { get; set; }

        [Column("rate")]
        public double? Rate { get; set; }

        [Column("ratecrk")]
        public double? RateCrk { get; set; }

        [Column("theoreticalunitprice", TypeName = "varchar(21)")]
        public string? TheoreticalUnitPrice { get; set; }

        [Column("divergenceup")]
        public bool? DivergenceUP { get; set; }

        [Column("idcrkcode")]
        public int? IdCrkCode { get; set; }

        [Column("vconsent")]
        public bool? VconSent { get; set; }

        [Column("vconsenttime")]
        public DateTime? VconSentTime { get; set; }

        [Column("vcondata", TypeName = "varchar(200)")]
        public string? VconData { get; set; }

        [Column("allocdata")]
        public byte[]? AllocData { get; set; }

        [Column("numseqtrade", TypeName = "varchar(45)")]
        public string? NumSeqTrade { get; set; }

        [Column("identdpartcamr", TypeName = "varchar(45)")]
        public string? IdentDPartCamr { get; set; }

        [Column("icelinkstatuscode")]
        public int? IcelinkStatusCode { get; set; }

        [Column("tradeorder")]
        public int? TradeOrder { get; set; }

        [Column("updaterlogin", TypeName = "varchar(45)")]
        public string? UpdaterLogin { get; set; }

        [Column("converted")]
        public bool? Converted { get; set; }
    }
}

