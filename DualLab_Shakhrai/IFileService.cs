namespace DualLab_Shakhrai
{
    public interface IFileService
    {
        Schedule LoadData(string filepath);
        void UnloadData(string filepath, Schedule schedule);
    }
}
