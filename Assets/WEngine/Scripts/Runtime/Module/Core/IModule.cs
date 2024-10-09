namespace WEngine
{
    public interface IModule
    {
        void Init();
        void Update();
        void Dispose();


        void OnInit();

        void OnUpdate();

        void OnDispose();

        void OnSubscribe();

        void UnSubscribe();
    }
}