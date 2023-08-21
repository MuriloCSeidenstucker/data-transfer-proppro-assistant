namespace PropProAssistant
{
    public class PcpModel : ModelWorksheetAbs
    {
        public int ProcessNumberCol { get; }
        public int IDCol { get; }
        public int ItemNameCol { get; }
        public int AmountCol { get; }
        public int AnvisaRegCol { get; }
        public int DescriptionCol { get; }
        public int TotalValueCol { get; }

        public PcpModel(string path)
        {
            Path = path;
            ProcessNumberCol = 1;
            IDCol = 2;
            ItemCol = 3;
            ItemNameCol = 4;
            AmountCol = 5;
            ModelCol = 6;
            BrandCol = 7;
            AnvisaRegCol = 8;
            DescriptionCol = 9;
            UnitValueCol = 10;
            TotalValueCol = 11;

            Structure = new string[1, 11]
            {
                { "Número do Processo (Não edite)",
                    "ID (Não edite)",
                    "Item (Não edite)",
                    "Produto (Não edite)",
                    "Quantidade (Não edite)",
                    "Modelo (Insira as informações quando aplicável)",
                    "Marca/Fabricante (Insira as informações quando aplicável)",
                    "Código de Registro na ANVISA (Insira as informações quando aplicável)",
                    "Descrição detalhada do Item (Insira as informações)",
                    "Valor unitário (Insira as informações)",
                    "Valor total (Insira as informações)"
                }
            };
        }
    }
}
