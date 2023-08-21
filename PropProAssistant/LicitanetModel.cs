namespace PropProAssistant
{
    public class LicitanetModel : ModelWorksheetAbs
    {
        public int ItemCodeCol { get; }
        public int DescriptionCol { get; }
        public int AmountCol { get; }

        public LicitanetModel(string path)
        {
            Path = path;
            ItemCol = 1;
            ItemCodeCol = 2;
            DescriptionCol = 3;
            AmountCol = 4;
            BrandCol = 5;
            ModelCol = 6;
            UnitValueCol = 7;

            Structure = new string[1, 7]
            {
                { "LOTE", "CÓDIGO DO ITEM", "DESCRIÇÃO DO ITEM", "QUANTIDADE DO ITEM", "MARCA", "MODELO", "VALOR UNITÁRIO" }
            };
        }
    }
}
