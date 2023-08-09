namespace PropProAssistant
{
    public interface IWorksheet
    {
        string[,] Structure { get; }
        string Path { get; }
        int ItemCol { get; }
        int BrandCol { get; }
        int ModelCol { get; }
        int UnitValueCol { get; }

        void Validate();
    }
}
