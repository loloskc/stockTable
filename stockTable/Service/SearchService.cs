using stockTable.Models;

namespace stockTable.Service
{
    public class SearchService
    {
        public IEnumerable<Equipment> GetEquipment(IEnumerable<Equipment> equipments,string searchField)
        {

            if (!String.IsNullOrEmpty(searchField)&&equipments.Count()!=0)
            {
                var searchByInventoryNum = equipments.Where(c => c.InventoryNum!.Contains(searchField)).ToList();
                var searchByModel = equipments.Where(c=>c.Model!.Contains(searchField)).ToList();
                var searchByTypeEq = equipments.Where(c=>c.TypeEq!.Contains(searchField)).ToList();
                var searchByIp = equipments.Where(c=>c.IPAddress!.Contains(searchField)).ToList();
                var searchBySerialNum = equipments.Where(c=>c.SerialNum!.Contains(searchField)).ToList();
                var searchByResponsibly = equipments.Where(c=>c.Document!.Responsible!.Contains(searchField)).ToList();

                var result = searchByInventoryNum.Union(searchByModel).Union(searchByTypeEq).Union(searchByIp).Union(searchBySerialNum).Union(searchByResponsibly);
                return result;
            }
            else
            {
                return equipments;  
            }
        }
    }
}
