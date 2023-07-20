namespace PropProAssistant
{
    public static class PriceBidWorksheet
    {
        public static string[,] Structure { get; set; } = new string[1, 14]
            {
                { "ITEM", "DESCRIÇÃO", "UND", "QTD", "MARCA", "VALOR DE CUSTO", "PERCENTUAL DE ENTRADA",
                    "CUSTO + PERCENTUAL DE ENTRADA", "VALOR TOTAL", "PERCENTUAL MÍNIMO", "VALOR MÍNIMO",
                    "LANCE ATUAL", "POSIÇÃO", "VALOR TOTAL DO LANCE" }
            };
        public static int ItemCol { get; set; } = 1;
        public static int DescriptionCol { get; set; } = 2;
        public static int UnitCol { get; set; } = 3;
        public static int AmountCol { get; set; } = 4;
        public static int BrandCol { get; set; } = 5;
        public static int CostPriceCol { get; set; } = 6;
        public static int EntryPercentCol { get; set; } = 7;
        public static int UnitPriceCol { get; set; } = 8;
        public static int TotalPriceCol { get; set; } = 9;
        public static int MinPercentCol { get; set; } = 10;
        public static int MinPriceCol { get; set; } = 11;
        public static int CurrentBidCol { get; set; } = 12;
        public static int PositionCol { get; set; } = 13;
        public static int TotalBidCol { get; set; } = 14;
    }
}
