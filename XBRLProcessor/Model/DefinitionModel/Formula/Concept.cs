﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Model.DefinitionModel.Formula
{
    public class Concept
    {
        private QName _QName = new QName("");
        public QName QName { get { return _QName; } set { _QName = value; } }
    }
}
