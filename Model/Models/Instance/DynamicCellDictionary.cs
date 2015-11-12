using BaseModel;
using LogicalModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalModel.Models
{
    public class DynamicCellDictionary
    {
        public Instance Instance = null;
        public String ReportID  = "";

        private Dictionary<string, string> _ExtDictionary = new Dictionary<string, string>();
        public Dictionary<string, string> ExtDictionary {
            get { return _ExtDictionary; }
            set { _ExtDictionary = value; }
        }
        private Dictionary<string, string> _RowDictionary = new Dictionary<string, string>();
        public Dictionary<string, string> RowDictionary
        {
            get { return _RowDictionary; }
            set { _RowDictionary = value; }
        }
        private Dictionary<string, string> _ColDictionary = new Dictionary<string, string>();
        public Dictionary<string, string> ColDictionary
        {
            get { return _ColDictionary; }
            set { _ColDictionary = value; }
        }

        private Dictionary<string, string> _CellOfFact = new Dictionary<string, string>();
        public Dictionary<string, string> CellOfFact
        {
            get { return _CellOfFact; }
            set { _CellOfFact = value; }
        }

        public DynamicCellDictionary() 
        {

        }

        public Cell AddCells(Cell cell,FactBase fact, Table table) 
        {
            var dynamiccell=new Cell();
            dynamiccell.Report = cell.Report;
            dynamiccell.Extension = cell.Extension;
            dynamiccell.Row = cell.Row;
            dynamiccell.Column = cell.Column;
            if (cell.Extension == "_") 
            {
                var ext = table.Extensions.Children.FirstOrDefault();
                var typeddimensions = ext.Item.Dimensions.Where(i => i.IsTyped).ToList();
                var axistypeddimensions = Dimension.GetDimensions(fact.Dimensions, typeddimensions);
                var typedfactstring = GetTypedFactString(axistypeddimensions);

                if (!ExtDictionary.ContainsKey(typedfactstring))
                {
                    var extnr = String.Format("{0}", ExtDictionary.Count + 1);
                    ExtDictionary.Add(typedfactstring, extnr);
                }
                dynamiccell.Extension = ExtDictionary[typedfactstring];

               
            }
            if (string.IsNullOrEmpty(cell.Row)) { 
               // var cell = table.Rows

                var row = table.Rows.FirstOrDefault(i => i.Item.LabelCode == cell.Row);
                var typeddimensions = row.Item.Dimensions.Where(i => i.IsTyped).ToList();
                var axistypeddimensions = Dimension.GetDimensions(fact.Dimensions, typeddimensions);
                var typedfactstring = GetTypedFactString(axistypeddimensions);

                if (!RowDictionary.ContainsKey(typedfactstring))
                {
                    var rownr = String.Format("{0}", RowDictionary.Count + 1);
                    RowDictionary.Add(typedfactstring, rownr);
                }
                dynamiccell.Row = RowDictionary[typedfactstring];

            }
            if (dynamiccell.CellID != cell.CellID)
            {
                //var cellfactstring = fact.FactString;
                CellOfFact.Add(fact.FactString, dynamiccell.CellID);
            }
            return dynamiccell;
        }

        public void Arrange() 
        {
           
        }

        public Hierarchy<LayoutItem> GetInstanceExtensions(Table table) 
        {
            if (ExtDictionary.Count == 0)
            {
                return table.Extensions;
            }
            else
            {
                var hlroot = new Hierarchy<LayoutItem>(table.Extensions.Item);
                var ix = 1;
                foreach (var typedext in ExtDictionary)
                {
                    var factstring = typedext.Key;
                    var typedfact = new FactBase();
                    typedfact.SetFromString(factstring);
                    typedfact.SetTyped();

                    foreach (var ext in table.Extensions.Children)
                    {
                        var li_original = ext.Item;
                        var li_new = LayoutItem.Copy(li_original);
                        foreach (var dim in li_new.Dimensions)
                        {
                            var instancedim = typedfact.Dimensions.FirstOrDefault(i => i.DimensionItem == dim.DimensionItem && i.Domain == dim.Domain);
                            dim.DomainMember = instancedim.DomainMember;
                        }
                        var fact = new FactBase();
                        fact.Dimensions = li_new.Dimensions;
                        fact.Concept = li_new.Concept;
                        fact.SetFactString();
                        li_new.FactString = fact.FactString;
                        var code = String.Format(Table.LabelCodeFormat, ix);
                        var content = String.Format(Table.ExtensionLableContentFormat, code);
                        li_new.LabelCode = code;
                        li_new.LabelContent = content;
                        var hli = new Hierarchy<LayoutItem>(li_new);
                        hlroot.Children.Add(hli);
                        hli.Parent = hlroot;

                        ix++;
                    }

                }
                return hlroot;
            }
        }


        public string GetTypedFactString(List<Dimension> dimensions) 
        {
            var typedfact = new FactBase();
            typedfact.Dimensions = dimensions.Where(i => i.IsTyped).ToList();
            var typedfactstring = typedfact.FactString.Trim();
            return typedfactstring;
               
        }
    }
}
