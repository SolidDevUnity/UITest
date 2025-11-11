public interface IDataStorageService
{
    bool HasSaveData();
    SaveData Load();
    void Save(SaveData saveData);
}

