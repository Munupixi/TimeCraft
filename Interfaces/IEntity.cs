namespace TimeCraft
{
    internal interface IEntity
    {
        void Delete();

        void Delete(int id);

        void Add();

        void Update();
    }
}