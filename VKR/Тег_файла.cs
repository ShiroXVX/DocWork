//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VKR
{
    using System;
    using System.Collections.Generic;
    
    public partial class Тег_файла
    {
        public int Код_документа { get; set; }
        public int Код_тега { get; set; }
        public string Доп_описание { get; set; }
    
        public virtual Документ Документ { get; set; }
        public virtual Список_тегов Список_тегов { get; set; }
    }
}
