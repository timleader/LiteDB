
namespace LiteDB
{
    public partial class LiteEngine
    {
        /// <summary>
        /// </summary>
        public void Reset()
        {
            _disk.Reset();
            
            // initialize all services again
            this.InitializeServices();
        }
    }
}