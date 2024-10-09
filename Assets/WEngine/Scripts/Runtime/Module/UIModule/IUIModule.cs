
namespace WEngine
{
    public interface IUIModule
    {
        void Show<T>() where T : IUIBase, new();
        void Hide<T>() where T : IUIBase;
        void Close<T>() where T : IUIBase;
    }
}