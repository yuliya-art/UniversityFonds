 namespace ClassRoom.Domain
{
    public static class ClassRoomTypeIdExtensions
    {
        public static string ToDescriptionString(this ClassRoomTypeId classRoomType) => classRoomType switch
        {
            ClassRoomTypeId.Unknown => "Не указано",
            ClassRoomTypeId.Gym => "Спортзал",
            ClassRoomTypeId.Lecture => "Лекционное",
            ClassRoomTypeId.Practice => "Для практических занятий",
            ClassRoomTypeId.Other => "Прочее",
            _ => "Неизвестный тип"
        };
    }
}
