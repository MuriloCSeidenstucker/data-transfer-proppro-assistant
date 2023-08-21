namespace PropProAssistant
{
    public class BncModel : ModelWorksheetAbs
    {
        public int BatchCol { get; }

        public BncModel(string path)
        {
            Path = path;
            BatchCol = 1;
            ItemCol = 2;
            UnitValueCol = 3;
            BrandCol = 4;
            ModelCol = 5;

            Structure = new string[1, 5]
            {
                { "Lote", "Item", "Valor Prop.", "Marca", "Modelo", }
            };
        }
    }
}
