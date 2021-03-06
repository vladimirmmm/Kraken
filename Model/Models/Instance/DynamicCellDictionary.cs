﻿using BaseModel;
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

        //private Dictionary<string, string> _CellOfFact = new Dictionary<string, string>();
        //public Dictionary<string, string> CellOfFact
        //{
        //    get { return _CellOfFact; }
        //    set { _CellOfFact = value; }
        //}

        private Hierarchy<LayoutItem> _Extensions = null;
        public Hierarchy<LayoutItem> Extensions
        {
            get { return _Extensions; }
            set { _Extensions = value; }
        }

        public DynamicCellDictionary() 
        {

        }
        public DynamicCellDictionary(Instance instance)
        {
            this.Instance = instance;
        }
        public List<int> GetOpenAspects(Taxonomy taxonomy, int[] key) 
        {
            var result = new List<int>();
            foreach (var aspect in key) 
            {
                var domainid = taxonomy.GetDimensionDomainPart(aspect);
                if (domainid != -1 && domainid==aspect) 
                {
                    result.Add(aspect);
                }
            }
            return result;
        }
        public List<int> GetInstanceAspects(Instance instance,InstanceFact fact, List<int> domains) 
        {
            var result = new List<int>();
            var taxonomy = instance.Taxonomy;
            for (int i=0;i<fact.InstanceKey.Length;i++)
            {
                var taxaspect = fact.TaxonomyKey[i];
                var instaspect = fact.InstanceKey[i];
                var domainid = taxonomy.GetDimensionDomainPart(taxaspect);
                if (domains.Contains(domainid)) 
                {
                    result.Add(instaspect);
                }
            }
            return result;
        }
        public string GetString(List<int> aspects) 
        {
            var taxonomy = Instance.Taxonomy;
            var sb = new StringBuilder();
            foreach (var aspect in aspects) 
            {
                var aspectstring = "";
                if (Instance.CounterFactParts.ContainsKey(aspect))
                {
                    aspectstring = Instance.CounterFactParts[aspect] + ",";

                }
                else 
                {
                    aspectstring = taxonomy.CounterFactParts[aspect] + ",";

                }
                sb.Append(aspectstring);
            }
            return sb.ToString();
        }
        public Cell AddCells(Cell cell, InstanceFact fact, Table table)
        {
            var dynamiccell = new Cell();
            dynamiccell.Report = cell.Report;
            dynamiccell.Extension = cell.Extension;
            dynamiccell.Row = cell.Row;
            dynamiccell.Column = cell.Column;

            if (cell.Extension == Literals.DynamicCode)
            {
                var ext = table.Extensions.Children.FirstOrDefault();

                var opendimensions = ext.Item.Dimensions.Where(i=>i.MapID==i.DomMapID).Select(i => i.MapID).ToList();

                var instanceopendimensions = GetInstanceAspects(Instance, fact, opendimensions);
                var openfactstring = GetString(instanceopendimensions);

                if (!ExtDictionary.ContainsKey(openfactstring))
                {
                    var extnr = String.Format("{0}", ExtDictionary.Count + 1);
                    ExtDictionary.Add(openfactstring, extnr);
                }
                dynamiccell.Extension = ExtDictionary[openfactstring];


            }
            if (cell.Row == Literals.DynamicCode)
            {
                // var cell = table.Rows

                var row = table.Rows.FirstOrDefault(i => i.Item.LabelCode == cell.Row);

                var opendimensions = row.Item.Dimensions.Where(i => i.MapID == i.DomMapID).Select(i => i.MapID).ToList();

                var instanceopendimensions = GetInstanceAspects(Instance, fact, opendimensions);
                var openfactstring = GetString(instanceopendimensions);

                if (!RowDictionary.ContainsKey(openfactstring))
                {
                    var rownr = String.Format("{0}", RowDictionary.Count + 1);
                    RowDictionary.Add(openfactstring, rownr);
                }
                dynamiccell.Row = RowDictionary[openfactstring];

            }
            if (cell.Column == Literals.DynamicCode)
            {
                // var cell = table.Rows

                var col = table.Columns.FirstOrDefault(i => i.Item.LabelCode == cell.Row);

                var opendimensions = col.Item.Dimensions.Where(i => i.MapID == i.DomMapID).Select(i => i.MapID).ToList();

                var instanceopendimensions = GetInstanceAspects(Instance, fact, opendimensions);
                var openfactstring = GetString(instanceopendimensions);

                if (!ColDictionary.ContainsKey(openfactstring))
                {
                    var rownr = String.Format("{0}", RowDictionary.Count + 1);
                    ColDictionary.Add(openfactstring, rownr);
                }
                dynamiccell.Column = ColDictionary[openfactstring];

            }
            if (dynamiccell.CellID != cell.CellID)
            {
              
            }
            else
            {

            }
            fact.Cells.Add(dynamiccell.CellID);

            return dynamiccell;
        }

        public Cell AddCells_Old(Cell cell, InstanceFact fact, Table table)
        {
            var dynamiccell = new Cell();
            dynamiccell.Report = cell.Report;
            dynamiccell.Extension = cell.Extension;
            dynamiccell.Row = cell.Row;
            dynamiccell.Column = cell.Column;

            if (fact.Dimensions.Count == 0)
            {
                var factstring = Instance.GetFactStringKey(fact.InstanceKey);
                var tmpfact = new FactBase();
                tmpfact.SetFromString(factstring);
                fact.Dimensions.AddRange(tmpfact.Dimensions);
            }

            if (cell.Extension == Literals.DynamicCode)
            {
                var ext = table.Extensions.Children.FirstOrDefault();
                var opendimensions = ext.Item.Dimensions.Where(i => String.IsNullOrEmpty(i.DomainMember)).ToList();

                //var typeddimensions = ext.Item.Dimensions.Where(i => i.IsTyped).ToList();
                //var axistypeddimensions = Dimension.GetDimensions(fact.Dimensions, typeddimensions);

                var instanceopendimensions = Dimension.GetDimensions(fact.Dimensions, opendimensions);
                var openfactstring = GetFactString(instanceopendimensions);

                if (!ExtDictionary.ContainsKey(openfactstring))
                {
                    var extnr = String.Format("{0}", ExtDictionary.Count + 1);
                    ExtDictionary.Add(openfactstring, extnr);
                }
                dynamiccell.Extension = ExtDictionary[openfactstring];


            }
            if (cell.Row == Literals.DynamicCode)
            {
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
            if (cell.Column == Literals.DynamicCode)
            {
                // var cell = table.Rows

                var col = table.Columns.FirstOrDefault(i => i.Item.LabelCode == cell.Row);
                var typeddimensions = col.Item.Dimensions.Where(i => i.IsTyped).ToList();
                var axistypeddimensions = Dimension.GetDimensions(fact.Dimensions, typeddimensions);
                var typedfactstring = GetTypedFactString(axistypeddimensions);

                if (!ColDictionary.ContainsKey(typedfactstring))
                {
                    var rownr = String.Format("{0}", RowDictionary.Count + 1);
                    ColDictionary.Add(typedfactstring, rownr);
                }
                dynamiccell.Column = ColDictionary[typedfactstring];

            }
            //if (dynamiccell.CellID != cell.CellID)
            //{
            //    //var cellfactstring = fact.FactString;
            //    //if (CellOfFact.ContainsKey(fact.FactString))
            //    if (1 == 2)
            //    {
            //        //var existing = CellOfFact[fact.FactString];

            //        var existingfacts = TaxonomyEngine.CurrentEngine.CurrentInstance.FactDictionary.FactsByTaxonomyKey[fact.InstanceKey];
            //        //var existingfact = existingfacts.FirstOrDefault(i => i.FactString == fact.FactString);
            //        var ctid = typeof(InstanceFact).IsAssignableFrom(fact.GetType()) ? ((InstanceFact)fact).ContextID : "";
            //        var msg = String.Format("Fact {0} already exist >> {1}!", fact, ctid);
            //        Utilities.Logger.WriteLine(msg);
            //    }
            //    else
            //    {
            //        //var item = this.Instance.FactDictionary[fact.FactIntkeys];
            //        //CellOfFact.Add(fact.FactString, dynamiccell.CellID);
            //    }
            //}
            //else
            //{

            //}
            fact.Cells.Add(dynamiccell.CellID);

            return dynamiccell;
        }

        public Cell AddCells_Old_2(Cell cell,InstanceFact fact, Table table) 
        {
            var dynamiccell=new Cell();
            dynamiccell.Report = cell.Report;
            dynamiccell.Extension = cell.Extension;
            dynamiccell.Row = cell.Row;
            dynamiccell.Column = cell.Column;

            if (fact.Dimensions.Count == 0)
            {
                var factstring = Instance.GetFactStringKey(fact.InstanceKey);
                var tmpfact = new FactBase();
                tmpfact.SetFromString(factstring);
                fact.Dimensions.AddRange(tmpfact.Dimensions);
            }

            if (cell.Extension == Literals.DynamicCode) 
            {
                var ext = table.Extensions.Children.FirstOrDefault();
                var opendimensions = ext.Item.Dimensions.Where(i => String.IsNullOrEmpty(i.DomainMember)).ToList();

                //var typeddimensions = ext.Item.Dimensions.Where(i => i.IsTyped).ToList();
                //var axistypeddimensions = Dimension.GetDimensions(fact.Dimensions, typeddimensions);
            
                var instanceopendimensions = Dimension.GetDimensions(fact.Dimensions, opendimensions);
                var openfactstring = GetFactString(instanceopendimensions);

                if (!ExtDictionary.ContainsKey(openfactstring))
                {
                    var extnr = String.Format("{0}", ExtDictionary.Count + 1);
                    ExtDictionary.Add(openfactstring, extnr);
                }
                dynamiccell.Extension = ExtDictionary[openfactstring];

               
            }
            if (cell.Row == Literals.DynamicCode) { 
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
            if (cell.Column == Literals.DynamicCode)
            {
                // var cell = table.Rows

                var col = table.Columns.FirstOrDefault(i => i.Item.LabelCode == cell.Row);
                var typeddimensions = col.Item.Dimensions.Where(i => i.IsTyped).ToList();
                var axistypeddimensions = Dimension.GetDimensions(fact.Dimensions, typeddimensions);
                var typedfactstring = GetTypedFactString(axistypeddimensions);

                if (!ColDictionary.ContainsKey(typedfactstring))
                {
                    var rownr = String.Format("{0}", RowDictionary.Count + 1);
                    ColDictionary.Add(typedfactstring, rownr);
                }
                dynamiccell.Column = ColDictionary[typedfactstring];

            }
            //if (dynamiccell.CellID != cell.CellID)
            //{
            //    //var cellfactstring = fact.FactString;
            //    //if (CellOfFact.ContainsKey(fact.FactString))
            //    //if (1 == 2)
            //    //{
            //    //    //var existing = CellOfFact[fact.FactString];

            //    //    var existingfacts = TaxonomyEngine.CurrentEngine.CurrentInstance.FactDictionary.FactsByTaxonomyKey[fact.InstanceKey];
            //    //    //var existingfact = existingfacts.FirstOrDefault(i => i.FactString == fact.FactString);
            //    //    var ctid = typeof(InstanceFact).IsAssignableFrom(fact.GetType()) ? ((InstanceFact)fact).ContextID : "";
            //    //    var msg = String.Format("Fact {0} already exist >> {1}!", fact, ctid);
            //    //    Utilities.Logger.WriteLine(msg);
            //    //}
            //    //else
            //    //{
            //    //    //var item = this.Instance.FactDictionary[fact.FactIntkeys];
            //    //    //CellOfFact.Add(fact.FactString, dynamiccell.CellID);
            //    //}
            //}
            //else 
            //{

            //}
            fact.Cells.Add(dynamiccell.CellID);

            return dynamiccell;
        }

        public void Arrange() 
        {
           
        }

        public Hierarchy<LayoutItem> GetInstanceExtensions(Table table) 
        {
            if (table.ID.Contains("07")) 
            {

            }
            if (ExtDictionary.Count == 0)
            {
                return table.Extensions;
            }
            else
            {
                if (this.Extensions == null)
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
                                if (instancedim != null)
                                {
                                    dim.DomainMember = instancedim.DomainMember;
                                }
                            }
                            var fact = new FactBase();
                            fact.Dimensions = li_new.Dimensions;
                            fact.Concept = li_new.Concept;
                            fact.SetFactString();
                            li_new.FactString = fact.FactString;

                            Label extensionlabel = new Label();
                            var code = String.Format(Table.LabelCodeFormat, typedext.Value);
                            var content = String.Format(Table.ExtensionLableContentFormat, code);

                            extensionlabel.LabelID = code;
                            extensionlabel.Code = code;
                            extensionlabel.Content = content;
                            if (fact.Dimensions.Count == 1) 
                            {
                                extensionlabel = table.Taxonomy.GetLabelForDimensionDomainMember(fact.Dimensions.FirstOrDefault());
                                extensionlabel.Code = code;
                            }

                            if (fact.Dimensions.Count > 1)
                            {
                                extensionlabel.Content = "";
                                extensionlabel.Code = "";
                                foreach (var dim in fact.Dimensions) 
                                {
                                    extensionlabel.Code += dim.DomainMemberFullName + ",";
                                    extensionlabel.Content += table.Taxonomy.GetLabelForDimensionDomainMember(dim) + ",";
                                }
                            }
                            li_new.ID = extensionlabel.LabelID;
                            li_new.LabelCode = extensionlabel.Code;
                            li_new.LabelContent = extensionlabel.Content;
                   
                            var hli = new Hierarchy<LayoutItem>(li_new);
                            hlroot.Children.Add(hli);
                            hli.Parent = hlroot;

                            ix++;
                        }

                    }
                    Extensions = hlroot;
                }
                return Extensions;
            }
        }


        public string GetTypedFactString(List<Dimension> dimensions) 
        {
            var typedfact = new FactBase();
            typedfact.Dimensions = dimensions.Where(i => i.IsTyped).ToList();
            var typedfactstring = typedfact.FactString.Trim();
            return typedfactstring;
               
        }

        public string GetFactString(List<Dimension> dimensions)
        {
            var typedfact = new FactBase();
            typedfact.Dimensions = dimensions.ToList();
            var typedfactstring = typedfact.FactString.Trim();
            return typedfactstring;

        }
    }
}
