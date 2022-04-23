using RunAndCatch;

namespace PoolSpawner
{
    internal class FootPrintPool : PoolSpawner<FootPrint>
    {
       protected override FootPrint CreateObject()
       {
            FootPrint footPrint = base.CreateObject();
            footPrint.FootPrintPool = this;           

            return footPrint;
        }
    }    
}
