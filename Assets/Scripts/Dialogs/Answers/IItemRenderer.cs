namespace Dialogs.Answers
{
     public interface IItemRenderer<in TDataType>
     {
          void SetData(TDataType data,int index);
        
     }
}
