using BaseModel;
using LogicalModel.Base;
using LogicalModel.Dimensions;
using LogicalModel.Validation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace LogicalModel
{

    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class Concept : QualifiedName
    {
        [JsonProperty]
        public QualifiedName Domain { get; set; }
        
        private String _HierarchyRole = "";
        [JsonProperty]
        public String HierarchyRole { get { return _HierarchyRole; } set { _HierarchyRole = value; } }

        [JsonProperty]
        public string ItemType { get; set; }

    }

    public class ConceptSetting : ItemTypeSetting
    {
        public string ConceptID { get; set; }

 
    }

    public class ItemTypeSetting 
    {
        public string ItemType { get; set; }
        public TypeEnum Type { get; set; }

        public int? Decimals { get; set; }
        public String UnitID { get; set; }

        public void Set(string itemtype, Taxonomy taxonomy)
        {
            //var concepttosearch = concept.ToLower();
            //var taxconcept = taxonomy.Concepts.FirstOrDefault(i => i.Content.ToLower() == concepttosearch);
            //if (taxconcept == null) 
            //{
            ItemType = itemtype;
            if (ItemType.In("integerItemType"))
            {
                Type = TypeEnum.Integer;
            }
            if (ItemType.In("monetaryItemType", "percentItemType"))
            {
                Type = TypeEnum.Numeric;
            }
            if (ItemType.In("stringItemType", "QNameItemType"))
            {
                Type = TypeEnum.String;

            }
            if (ItemType.In("booleanItemType"))
            {
                Type = TypeEnum.Boolean;

            }
            if (ItemType.In("dateItemType"))
            {
                Type = TypeEnum.Date;

            }

            if (Type == TypeEnum.Numeric)
            {
                Decimals = 0;
                if (ItemType == "percentItemType")
                {
                    Decimals = 4;
                }
            }


            // }
        }
    }
}
